#nullable enable

using Microsoft.AspNetCore.Components;
using PROWeb.Components.Extensions;
using PROWeb.Components.Person;
using PROWeb.Data.Models;
using PROWeb.Data.Services.Voters;
using PROWeb.Office.ViewModels.Voters;

namespace PROWeb.Office.Components.Voters
{
    public class VoterDocumentsView : PersonDocumentsView<ListVoterViewModel, DocumentViewModel>
    {
        [Inject]
        private IVotersServiceFactory _voterServiceFactory { get; set; } = null!;

        protected override async Task<int> AddDocumentAsync(DocumentViewModel vmDocument, MemoryStream stream)
        {
            Document document = vmDocument.MapTo<Document>(Mapper);

            document.PersonId = Context.VoterId;
            document.DocumentDate = DateTime.Now;
            document.RegistryYear = Context.RegistryYear;
            document.Content = stream?.ToArray();

            using (var service = _voterServiceFactory.CreateService())
            {
                await service.AddDocumentAsync(document);
            }

            return document.DocumentId;
        }

        protected override async Task DeleteDocumentAsync(DocumentViewModel vmDocument)
        {
            using (var service = _voterServiceFactory.CreateService())
            {
                await service.DeleteDocumentAsync(vmDocument.DocumentId);
            }
        }

        protected override async Task<DocumentViewModel?> DownloadDocumentAsync(int documetId)
        {
            byte[]? content;
            Document? document;

            using (var service = _voterServiceFactory.CreateService())
            {
                document = await service.GetVoterDocumentAsync(documetId);
                content = await service.GetVoterDocumentContentAsync(documetId);
            }

            if(document == null)
            {
                return null;
            }

            DocumentViewModel vmDocument = document.MapTo<DocumentViewModel>(Mapper);
            vmDocument.Content = content;

            return vmDocument;
        }
    }
}
