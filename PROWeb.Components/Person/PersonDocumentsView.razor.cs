using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using PROWeb.Common.Components;
using PROWeb.Components.Extensions;
using PROWeb.Components.ViewModels.Person;
using PROWeb.Components.ViewModels.Voter;
using PROWeb.Data.Models;
using PROWeb.Data.Services.Voters;
using Telerik.Blazor.Components;
using Telerik.Blazor.Components.FileSelect;

namespace PROWeb.Components.Person
{
    public class PersonDocumentsViewBase<TPersonDocumentsViewModel, TDocumentViewModel> : PROView<TPersonDocumentsViewModel>
        where TPersonDocumentsViewModel : class, IPersonDocumentsViewModel<TDocumentViewModel>
        where TDocumentViewModel : class, IDocumentViewModel
    {
        [Inject]
        private IVotersServiceFactory _voterServiceFactory { get; set; } = null!;

        [Inject]
        private IJSRuntime _js { get; set; } = null!;

        public IList<TDocumentViewModel>? Documents { get; set; }

        public VoterDocumentViewModel NewDocument { get; set; } = new VoterDocumentViewModel();

        protected bool ShowUploadDialog { get; set; }

        protected bool AllowSubmitDocument { get; set; }

        protected EditContext? DocumentEditContext { get; set; }

        private CancellationTokenSource? _newDocUploadToken { get; set; }

        private MemoryStream? _newDocStream { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            Documents = Context?.Documents;
        }

        protected void AddDocument()
        {
            OnShowNewDocumentWindow();
        }

        protected virtual void OnSubmitNewDocument()
        {
            OnCloseNewDocumentWindow();
        }

        protected virtual void OnDeleteDocument(int personId, int documetId)
        {

        }

        protected async Task OnSelectDocumentHandler(FileSelectEventArgs args)
        {
            await ReadFileAsync(args.Files.First());
        }

        protected void OnRemoveDocumentHandler(FileSelectEventArgs args)
        {
            DocumentStreamDispose();
            DocumentEditContext?.Validate();
            NewDocument.DocumentName = null;
            NewDocument.ExportFormat = null;
        }

        protected void OnShowNewDocumentWindow()
        {
            DocumentStreamDispose();
            NewDocument = new VoterDocumentViewModel();
            DocumentEditContext = new EditContext(NewDocument);
            DocumentEditContext.OnValidationStateChanged += OnNewDocumentValidationStateChanged;
            ShowUploadDialog = true;
        }

        protected void OnCloseNewDocumentWindow()
        {
            if (DocumentEditContext is { } context)
            {
                context.OnValidationStateChanged -= OnNewDocumentValidationStateChanged;
                context = null;
            }

            ShowUploadDialog = false;
            AllowSubmitDocument = false;
        }

        protected async Task DownloadDocumentAsync(int voterId, int documetId)
        {
            byte[]? content;
            Document? document;

            using (var service = _voterServiceFactory.CreateService())
            {
                document = await service.GetVoterDocumentAsync(documetId);
                content = await service.GetVoterDocumentContentAsync(documetId);
            }

            if (document == null || content == null)
            {
                return;
            }

            await DownloadFileFromStreamAsync(document.MapTo<VoterDocumentViewModel>(Mapper), new MemoryStream(content));
        }

        private async Task ReadFileAsync(FileSelectFileInfo file)
        {
            DocumentStreamDispose();

            if (file.InvalidExtension || file.InvalidMaxFileSize)
            {
                return;
            }

            var byteArray = new byte[file.Size];
            _newDocUploadToken = new CancellationTokenSource();
            await using MemoryStream fs = new MemoryStream(byteArray);
            await file.Stream.CopyToAsync(fs, _newDocUploadToken.Token);
            _newDocStream = fs;
            NewDocument.DocumentName = Path.GetFileNameWithoutExtension(file.Name);
            NewDocument.ExportFormat = file.Extension;
            DocumentEditContext?.Validate();
        }

        private void DocumentStreamDispose()
        {
            _newDocUploadToken?.Cancel();
            _newDocStream?.Dispose();

            _newDocUploadToken = null;
            _newDocStream = null;
        }

        private void OnNewDocumentValidationStateChanged(object? sender, ValidationStateChangedEventArgs e)
        {
            ValidateNewDocument();
        }

        private void ValidateNewDocument()
        {
            AllowSubmitDocument = DocumentEditContext?.GetValidationMessages().Any() == false && _newDocStream != null;
        }

        private async Task DownloadFileFromStreamAsync(VoterDocumentViewModel document, Stream stream)
        {
            var fileName = $"{document.DocumentName}.{document.ExportFormat}";

            using var streamRef = new DotNetStreamReference(stream: stream);

            await _js.InvokeVoidAsync("downloadFileFromStream", fileName, streamRef);
        }
    }
}
