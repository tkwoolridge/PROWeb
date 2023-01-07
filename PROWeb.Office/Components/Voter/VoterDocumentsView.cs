using PROWeb.Components.Person;
using PROWeb.Components.ViewModels.Voter;
using PROWeb.Office.ViewModels.Voter;

namespace PROWeb.Office.Components.Voter
{
    public class VoterDocumentsView : PersonDocumentsView<ListVoterViewModel, DocumentViewModel>
    {
        protected override async Task OnDeleteDocumentAsync(int personId, int documetId)
        {
            await base.OnDeleteDocumentAsync(personId, documetId);
        }

        protected override async Task OnSubmitNewDocumentAsync()
        {
            await base.OnSubmitNewDocumentAsync();
        }
    }
}
