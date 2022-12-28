using PROWeb.Components.Person;
using PROWeb.Components.ViewModels.Voter;
using PROWeb.Office.ViewModels.Voter;

namespace PROWeb.Office.Components.Voter
{
    public class VoterDocumentsView : PersonDocumentsView<ListVoterViewModel, VoterDocumentViewModel>
    {
        protected override void OnDeleteDocument(int personId, int documetId)
        {
            base.OnDeleteDocument(personId, documetId);
        }

        protected override void OnSubmitNewDocument()
        {
            base.OnSubmitNewDocument();
        }
    }
}
