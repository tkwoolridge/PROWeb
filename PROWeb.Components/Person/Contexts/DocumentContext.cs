using PROWeb.Common.Extensions;
using PROWeb.Common.StrongBindings.Enums;
using PROWeb.Common.StrongBindings.Extensions;
using PROWeb.Common.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace PROWeb.Components.Person.Contexts
{
    internal class DocumentContext<TDocumentViewModel> : BindableContext<TDocumentViewModel> where TDocumentViewModel : SlimViewModelBase, new()
    {
        private int _documentId;

        public int DocumentId
        {
            get => _documentId;
            set => RaiseAndSetIfChanged(ref _documentId, value);
        }

        private DateTime _documentDate;

        public DateTime DocumentDate
        {
            get => _documentDate;
            set => RaiseAndSetIfChanged(ref _documentDate, value);
        }

        private string? _documentName;

        [Required(ErrorMessage = "Document name is required!")]
        [StringLength(50, ErrorMessage = "Maximum name size is 50 characters!")]
        public string? DocumentName
        {
            get => _documentName;
            set => RaiseAndSetIfChanged(ref _documentName, value.ToNullIfWhiteSpace());
        }

        private string? _documentDescription;

        public string? DocumentDescription
        {
            get => _documentDescription;
            set => RaiseAndSetIfChanged(ref _documentDescription, value.ToNullIfWhiteSpace());
        }

        private string? _exportFormat;

        public string? ExportFormat
        {
            get => _exportFormat;
            set => RaiseAndSetIfChanged(ref _exportFormat, value.ToNullIfWhiteSpace());
        }

        private byte[]? _content;

        public byte[]? Content
        {
            get => _content;
            set => RaiseAndSetIfChanged(ref _content, value);
        }

        private IDisposable? _documentIdBinding;
        private IDisposable? _documentDateBinding;
        private IDisposable? _documentNameBinding;
        private IDisposable? _documentDescriptionBinding;
        private IDisposable? _exportFormatBinding;
        private IDisposable? _contentBinding;

        public DocumentContext(
            TDocumentViewModel? model = null,
            Expression<Func<TDocumentViewModel, int>>? documentIdPath = null,
            Expression<Func<TDocumentViewModel, DateTime>>? documentDatePath = null,
            Expression<Func<TDocumentViewModel, string?>>? documentNamePath = null,
            Expression<Func<TDocumentViewModel, string?>>? documentDescriptionPath = null,
            Expression<Func<TDocumentViewModel, string?>>? exportFormatPath = null,
            Expression<Func<TDocumentViewModel, byte[]?>>? contentPath = null
            )
        {
            Model = model ?? new TDocumentViewModel();
            
            _documentIdBinding = Model?.Bind(this, documentIdPath, c => c.DocumentId, StrongBindingMode.TwoWay);
            _documentDateBinding = Model?.Bind(this, documentDatePath, c => c.DocumentDate, StrongBindingMode.TwoWay);
            _documentNameBinding = Model?.Bind(this, documentNamePath, c => c.DocumentName, StrongBindingMode.TwoWay);
            _documentDescriptionBinding = Model?.Bind(this, documentDescriptionPath, c => c.DocumentDescription, StrongBindingMode.TwoWay);
            _exportFormatBinding = Model?.Bind(this, exportFormatPath, c => c.ExportFormat, StrongBindingMode.TwoWay);
            _contentBinding = Model?.Bind(this, contentPath, c => c.Content, StrongBindingMode.TwoWay);
        }

        public override void UnBind()
        {
            _documentIdBinding?.Dispose();
            _documentDateBinding?.Dispose();
            _documentNameBinding?.Dispose();
            _documentDescriptionBinding?.Dispose();
            _exportFormatBinding?.Dispose();
            _contentBinding?.Dispose();
        }
    }
}
