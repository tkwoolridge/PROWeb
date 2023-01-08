namespace PROWeb.Components.ViewModels.Person
{
    public interface IPersonDocumentsViewModel<TDocumentViewModel>
         where TDocumentViewModel : class, IDocumentViewModel
    {
        string FullName { get; }

        int RegistryYear { get; }

        List<TDocumentViewModel> Documents { get; set; }
    }
}
