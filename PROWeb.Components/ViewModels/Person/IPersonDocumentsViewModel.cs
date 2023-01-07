namespace PROWeb.Components.ViewModels.Person
{
    public interface IPersonDocumentsViewModel<TDocumentViewModel>
         where TDocumentViewModel : class, IDocumentViewModel
    {
        int PersonId { get; }

        string FullName { get; }

        int RegistryYear { get; }

        List<TDocumentViewModel> Documents { get; set; }
    }
}
