namespace PROWeb.Components.ViewModels.Person
{
    public interface IPersonDocumentsViewModel<TDocumentViewModel>
         where TDocumentViewModel : class, IDocumentViewModel
    {
        List<TDocumentViewModel> Documents { get; set; }
    }
}
