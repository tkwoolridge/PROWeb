using PROWeb.Common.ViewModels;
using PROWeb.Components.ViewModels.Person;
using System.ComponentModel.DataAnnotations;

namespace PROWeb.Components.ViewModels.Voter
{
    public class DocumentViewModel : ViewModelBase, IDocumentViewModel
    {
        public int DocumentId { get; set; }

        public int PersonId { get; set; }

        public int RegistryYear { get; set; }

        public DateTime DocumentDate { get; set; }

        [Required(ErrorMessage = "Document name is required!")]
        [StringLength(50, ErrorMessage = "Maximum name size is {0} characters!")]
        public string? DocumentName { get; set; }

        public string? DocumentDescription { get; set; }

        public string? ExportFormat { get; set; }
    }
}
