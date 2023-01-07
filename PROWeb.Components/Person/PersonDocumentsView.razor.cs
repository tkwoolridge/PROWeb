using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using PROWeb.Common.Components;
using PROWeb.Components.Extensions;
using PROWeb.Components.Properties;
using PROWeb.Components.ViewModels.Person;
using PROWeb.Components.ViewModels.Voter;
using PROWeb.Data.Models;
using PROWeb.Data.Services.Voters;
using Telerik.Blazor;
using Telerik.Blazor.Components;
using Telerik.Blazor.Components.FileSelect;

namespace PROWeb.Components.Person
{
    public class PersonDocumentsViewBase<TPersonDocumentsViewModel, TDocumentViewModel> : PROView<TPersonDocumentsViewModel>
        where TPersonDocumentsViewModel : class, IPersonDocumentsViewModel<TDocumentViewModel>
        where TDocumentViewModel : class, IDocumentViewModel, new()
    {
        [Inject]
        private IVotersServiceFactory _voterServiceFactory { get; set; } = null!;

        [Inject]
        private IJSRuntime _js { get; set; } = null!;

        [CascadingParameter]
        public DialogFactory Dialogs { get; set; } = null!;

        public IList<TDocumentViewModel>? Documents { get; set; }

        public TDocumentViewModel NewDocument { get; set; } = new TDocumentViewModel();

        protected bool ShowUploadDialog { get; set; }

        protected bool AllowSubmitDocument { get; set; }

        protected EditContext? DocumentEditContext { get; set; }

        protected TelerikGrid<TDocumentViewModel>? DocumentsGridRef { get; set; }

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

        protected virtual async Task OnSubmitNewDocumentAsync()
        {
            OnCloseNewDocumentWindow();

            Document document = NewDocument.MapTo<Document>(Mapper);

            document.PersonId = Context.PersonId;
            document.DocumentDate = DateTime.Now;
            document.RegistryYear= Context.RegistryYear;
            document.Content = _newDocStream?.ToArray();

            using (var service = _voterServiceFactory.CreateService())
            {
                await service.AddDocumentAsync(document);
                Context.Documents.Add(NewDocument);
                NewDocument.DocumentId = document.DocumentId;
            }

            DocumentsGridRef?.Rebind();
        }

        protected virtual async Task OnDeleteDocumentAsync(int personId, int documetId)
        {
            if (Context.Documents.FirstOrDefault(d => d.DocumentId == documetId) is not { } document)
            {
                return;
            }

            bool isConfirmed = await Dialogs.ConfirmAsync(string.Format(Messages.DeleteDocumentMessage, document.DocumentName, Context.FullName), "Delete Document.");

            if(!isConfirmed)
            {
                return;
            }

            using (var service = _voterServiceFactory.CreateService())
            {
                await service.DeleteDocumentAsync(documetId);
                Context.Documents.Remove(document);
            }

            DocumentsGridRef?.Rebind();
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
            NewDocument = new TDocumentViewModel();
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

            await DownloadFileFromStreamAsync(document.MapTo<DocumentViewModel>(Mapper), new MemoryStream(content));
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

        private async Task DownloadFileFromStreamAsync(DocumentViewModel document, Stream stream)
        {
            var fileName = $"{document.DocumentName}.{document.ExportFormat}";

            using var streamRef = new DotNetStreamReference(stream: stream);

            await _js.InvokeVoidAsync("downloadFileFromStream", fileName, streamRef);
        }
    }
}
