using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROWeb.Components.ViewModels.Person
{
    public interface IDocumentViewModel
    {
        int PersonId { get; set; }

        int DocumentId { get; set; }

        DateTime DocumentDate { get; set; }

        string? DocumentName { get; set; }

        string? DocumentDescription { get; set; }

        string? ExportFormat { get; set; }
    }
}
