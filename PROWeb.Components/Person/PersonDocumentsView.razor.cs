using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using PROWeb.Common.Components;
using PROWeb.Components.Properties;
using PROWeb.Components.ViewModels.Person;
using System.Diagnostics;
using Telerik.Blazor;
using Telerik.Blazor.Components;
using Telerik.Blazor.Components.FileSelect;

namespace PROWeb.Components.Person
{
    public abstract partial class PersonDocumentsView<TPersonDocumentsViewModel, TDocumentViewModel> : PersonDocumentsViewBase<TPersonDocumentsViewModel, TDocumentViewModel>
        where TPersonDocumentsViewModel : class, IPersonDocumentsViewModel<TDocumentViewModel>
        where TDocumentViewModel : class, IDocumentViewModel, new()
    {
    }

    public abstract class PersonDocumentsViewBase<TPersonDocumentsViewModel, TDocumentViewModel> : PROView<TPersonDocumentsViewModel>
        where TPersonDocumentsViewModel : class, IPersonDocumentsViewModel<TDocumentViewModel>
        where TDocumentViewModel : class, IDocumentViewModel, new()
    {
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

        protected async Task OnSubmitNewDocumentAsync()
        {
            OnCloseNewDocumentWindow();

            if(_newDocStream is not { } stream)
            {
                return;
            }

            int documentId = await AddDocumentAsync(NewDocument, stream);

            Context.Documents.Add(NewDocument);
            NewDocument.DocumentId = documentId;

            DocumentsGridRef?.Rebind();
        }

        protected abstract Task<int> AddDocumentAsync(TDocumentViewModel vmDocument, MemoryStream stream);

        protected async Task OnDeleteDocumentAsync(int documetId)
        {
            if (Context.Documents.FirstOrDefault(d => d.DocumentId == documetId) is not { } document)
            {
                return;
            }

            bool isConfirmed = await Dialogs.ConfirmAsync(string.Format(Messages.DeleteDocumentMessage, document.DocumentName, Context.FullName), "Delete Document.");

            if (!isConfirmed)
            {
                return;
            }

            await DeleteDocumentAsync(document);
            Context.Documents.Remove(document);

            DocumentsGridRef?.Rebind();
        }

        protected abstract Task DeleteDocumentAsync(TDocumentViewModel vmDocument);

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

        protected async Task OnDownloadDocumentAsync(int documetId)
        {
            TDocumentViewModel? document = await DownloadDocumentAsync(documetId);

            if (document?.Content is null)
            {
                return;
            }

            await DownloadFileFromStreamAsync(document);
        }

        protected abstract Task<TDocumentViewModel?> DownloadDocumentAsync(int documetId);


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

        private async Task DownloadFileFromStreamAsync(TDocumentViewModel document)
        {
            var fileName = $"{document.DocumentName}.{document.ExportFormat}";

            Debug.Assert(document.Content != null);

            using var streamRef = new DotNetStreamReference(stream: new MemoryStream(document.Content));

            await _js.InvokeVoidAsync("downloadFileFromStream", fileName, streamRef);
        }
    }
}
