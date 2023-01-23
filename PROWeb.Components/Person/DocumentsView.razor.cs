using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using PROWeb.Common.ViewModels;
using PROWeb.Components.Common;
using PROWeb.Components.Person.Contexts;
using PROWeb.Components.Properties;
using System.Diagnostics;
using System.Linq.Expressions;
using Telerik.Blazor;
using Telerik.Blazor.Components;
using Telerik.Blazor.Components.FileSelect;

namespace PROWeb.Components.Person
{
    public abstract class DocumentsViewBase<TDocumentsViewModel, TDocumentViewModel> : PROView<TDocumentsViewModel>
        where TDocumentsViewModel : SlimViewModelBase
        where TDocumentViewModel : SlimViewModelBase
    {
    }

    public abstract partial class DocumentsView<TDocumentsViewModel, TDocumentViewModel> : DocumentsViewBase<TDocumentsViewModel, TDocumentViewModel>
        where TDocumentsViewModel : SlimViewModelBase
        where TDocumentViewModel : SlimViewModelBase, new()
    {
        private readonly Expression<Func<TDocumentsViewModel, IEnumerable<TDocumentViewModel>?>>? _modelsPath;
        private readonly Expression<Func<TDocumentsViewModel, int>>? _registryYearPath;
        private readonly Expression<Func<TDocumentsViewModel, string?>>? _fullNamePath;
        private readonly Expression<Func<TDocumentViewModel, int>>? _documentIdPath;
        private readonly Expression<Func<TDocumentViewModel, DateTime>>? _documentDatePath;
        private readonly Expression<Func<TDocumentViewModel, string?>>? _documentNamePath;
        private readonly Expression<Func<TDocumentViewModel, string?>>? _documentDescriptionPath;
        private readonly Expression<Func<TDocumentViewModel, string?>>? _exportFormatPath;
        private readonly Expression<Func<TDocumentViewModel, byte[]?>>? _contentPath;

        [Inject]
        private IJSRuntime _js { get; set; } = null!;

        [CascadingParameter]
        public DialogFactory Dialogs { get; set; } = null!;

        public DocumentsView(
            Expression<Func<TDocumentsViewModel, IEnumerable<TDocumentViewModel>?>>? modelsPath,
            Expression<Func<TDocumentsViewModel, int>>? registryYearPath = null,
            Expression<Func<TDocumentsViewModel, string?>>? fullNamePath = null,
            Expression<Func<TDocumentViewModel, int>>? documentIdPath = null,
            Expression<Func<TDocumentViewModel, DateTime>>? documentDatePath = null,
            Expression<Func<TDocumentViewModel, string?>>? documentNamePath = null,
            Expression<Func<TDocumentViewModel, string?>>? documentDescriptionPath = null,
            Expression<Func<TDocumentViewModel, string?>>? exportFormatPath = null,
            Expression<Func<TDocumentViewModel, byte[]?>>? contentPath = null)
        {
            _modelsPath = modelsPath;
            _registryYearPath = registryYearPath;
            _fullNamePath = fullNamePath;
            _documentIdPath = documentIdPath;
            _documentDatePath = documentDatePath;
            _documentNamePath = documentNamePath;
            _documentDescriptionPath = documentDescriptionPath;
            _exportFormatPath = exportFormatPath;
            _contentPath = contentPath;

            NewDocument = CreateNewDocument();
        }

        internal DocumentsContext<TDocumentsViewModel, TDocumentViewModel> Context { get; } = new DocumentsContext<TDocumentsViewModel, TDocumentViewModel>();

        internal IList<DocumentContext<TDocumentViewModel>>? Documents { get; set; }

        internal DocumentContext<TDocumentViewModel> NewDocument { get; set; }

        protected bool ShowUploadDialog { get; set; }

        protected bool AllowSubmitDocument { get; set; }

        protected EditContext? DocumentEditContext { get; set; }
        internal TelerikGrid<DocumentContext<TDocumentViewModel>>? DocumentsGridRef { get; set; }

        private CancellationTokenSource? _newDocUploadToken { get; set; }

        private MemoryStream? _newDocStream { get; set; }

        protected override void OnModelUpdate()
        {
            base.OnModelUpdate();

            Context.UnBind();
            if (Model is { } model)
            {
                Context.Bind
                (
                model,
                _modelsPath,
                _registryYearPath,
                _fullNamePath
                );
            }
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            Documents = Context.Documents;
        }

        protected void AddDocument()
        {
            OnShowNewDocumentWindow();
        }

        protected async Task OnSubmitNewDocumentAsync()
        {
            OnCloseNewDocumentWindow();

            if (_newDocStream is not { } stream || NewDocument.Model is not { } model)
            {
                return;
            }

            int documentId = await AddDocumentAsync(model, stream);

            Context.Documents?.Add(NewDocument);
            NewDocument.DocumentId = documentId;

            DocumentsGridRef?.Rebind();
        }

        protected abstract Task<int> AddDocumentAsync(TDocumentViewModel vmDocument, MemoryStream stream);

        protected async Task OnDeleteDocumentAsync(int documetId)
        {
            if (Context.Documents?.FirstOrDefault(d => d.DocumentId == documetId) is not { } document || document.Model is not { } model)
            {
                return;
            }

            bool isConfirmed = await Dialogs.ConfirmAsync(string.Format(Messages.DeleteDocumentMessage, document.DocumentName, Context.FullName), "Delete Document.");

            if (!isConfirmed)
            {
                return;
            }

            await DeleteDocumentAsync(model);
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

        private DocumentContext<TDocumentViewModel> CreateNewDocument(TDocumentViewModel? model = null)
        {
            return new DocumentContext<TDocumentViewModel>(
                model,
                _documentIdPath,
                _documentDatePath,
                _documentNamePath,
                _documentDescriptionPath,
                _exportFormatPath,
                _contentPath);
        }

        protected void OnShowNewDocumentWindow()
        {
            DocumentStreamDispose();
            NewDocument = CreateNewDocument();
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
            TDocumentViewModel? model = await DownloadDocumentAsync(documetId);

            DocumentContext<TDocumentViewModel> document = CreateNewDocument(model);

            if (document.Content is null)
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

        private async Task DownloadFileFromStreamAsync(DocumentContext<TDocumentViewModel> document)
        {
            var fileName = $"{document.DocumentName}.{document.ExportFormat}";

            Debug.Assert(document.Content != null);

            using var streamRef = new DotNetStreamReference(stream: new MemoryStream(document.Content));

            await _js.InvokeVoidAsync("downloadFileFromStream", fileName, streamRef);
        }
    }
}
