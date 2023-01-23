using PROWeb.Common.StrongBindings.Enums;
using PROWeb.Common.StrongBindings.Extensions;
using PROWeb.Common.ViewModels;
using System.Linq.Expressions;

namespace PROWeb.Components.Person.Contexts
{
    internal class DocumentsContext<TDocumentsViewModel, TDocumentViewModel> : BindableContext<TDocumentsViewModel> 
        where TDocumentsViewModel : SlimViewModelBase
        where TDocumentViewModel : SlimViewModelBase, new()
    {
        private int _registryYear;

        public int RegistryYear
        {
            get => _registryYear;
            set => RaiseAndSetIfChanged(ref _registryYear, value);
        }

        private string? _fullName;

        public string? FullName
        {
            get => _fullName;
            set => RaiseAndSetIfChanged(ref _fullName, value);
        }

        private IList<DocumentContext<TDocumentViewModel>> _contextDocuments = new List<DocumentContext<TDocumentViewModel>>();

        public IList<DocumentContext<TDocumentViewModel>> ContextDocuments
        {
            get => _contextDocuments;
            set => RaiseAndSetIfChanged(ref _contextDocuments, value);
        }


        private IList<TDocumentViewModel>? _documents;

        public IList<TDocumentViewModel>? Documents
        {
            get => _documents;
            set => RaiseAndSetIfChanged(ref _documents, value);
        }

        private IDisposable? _documentsBinding;
        private IDisposable? _registryYearBinding;
        private IDisposable? _fullNameBinding;

        public void Bind(
            TDocumentsViewModel model,
            Expression<Func<TDocumentsViewModel, IEnumerable<TDocumentViewModel>?>>? documentsPath = null,
            Expression<Func<TDocumentsViewModel, int>>? registryYearPath = null,
            Expression<Func<TDocumentsViewModel, string?>>? fullNamePath = null,
            Expression<Func<TDocumentViewModel, int>>? documentIdPath = null,
            Expression<Func<TDocumentViewModel, DateTime>>? documentDatePath = null,
            Expression<Func<TDocumentViewModel, string?>>? documentNamePath = null,
            Expression<Func<TDocumentViewModel, string?>>? documentDescriptionPath = null,
            Expression<Func<TDocumentViewModel, string?>>? exportFormatPath = null,
            Expression<Func<TDocumentViewModel, byte[]?>>? contentPath = null
            )
        {
            Model = model;

            _documentsBinding = Model?.Bind(this, documentsPath, c => c.Documents, StrongBindingMode.TwoWay);
            _registryYearBinding = Model?.Bind(this, registryYearPath, c => c.RegistryYear, StrongBindingMode.OneWay);
            _fullNameBinding = Model?.Bind(this, fullNamePath, c => c.FullName, StrongBindingMode.OneWay);

            if(_documents is not { } models)
            {
                return;
            }

            foreach (var dModel in models)
            {
                ContextDocuments.Add(new DocumentContext<TDocumentViewModel>(
                    dModel,
                    documentIdPath,
                    documentDatePath,
                    documentNamePath,
                    documentDescriptionPath,
                    exportFormatPath,
                    contentPath));
            }
        }

        public override void UnBind()
        {
            _documentsBinding?.Dispose();
            _registryYearBinding?.Dispose();
            _fullNameBinding?.Dispose();

            foreach (var document in ContextDocuments)
            {
                document.UnBind();
            }
        }
    }
}
