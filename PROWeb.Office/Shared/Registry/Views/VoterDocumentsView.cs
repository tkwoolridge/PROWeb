using Microsoft.AspNetCore.Components;
using PROWeb.Components.Extensions;
using PROWeb.Components.Person;
using PROWeb.Components.Voters.ViewModels;
using PROWeb.Data.Models;
using PROWeb.Data.Services.Voters;
using PROWeb.Office.Shared.Registry.ViewModels;

namespace PROWeb.Office.Shared.Registry.Views
{
    public class VoterDocumentsView : DocumentsView<ListVoterViewModel, DocumentViewModel>
    {
        public VoterDocumentsView() :
            base(v => v.Documents,
                v => v.RegistryYear,
                v => v.FullName,
                d => d.DocumentId,
                d => d.DocumentDate,
                d => d.DocumentName,
                d => d.DocumentDescription,
                d => d.ExportFormat,
                d => d.Content)
        {
        }

        [Inject]
        private IVotersServiceFactory _voterServiceFactory { get; set; } = null!;

        protected override async Task<int> AddDocumentAsync(DocumentViewModel vmDocument, MemoryStream stream)
        {
            if (Model?.VoterId is not { } voterId)
            {
                return -1;
            }

            Document document = vmDocument.MapTo<Document>(Mapper);

            document.PersonId = voterId;
            document.DocumentDate = DateTime.Now;
            document.RegistryYear = Model.RegistryYear;
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

            if (document == null)
            {
                return null;
            }

            DocumentViewModel vmDocument = document.MapTo<DocumentViewModel>(Mapper);
            vmDocument.Content = content;

            return vmDocument;
        }
    }
}
