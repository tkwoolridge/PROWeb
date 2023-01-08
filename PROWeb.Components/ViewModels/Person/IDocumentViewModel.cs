namespace PROWeb.Components.ViewModels.Person
{
    public interface IDocumentViewModel
    {
        int DocumentId { get; set; }

        DateTime DocumentDate { get; set; }

        string? DocumentName { get; set; }

        string? DocumentDescription { get; set; }

        string? ExportFormat { get; set; }

        byte[]? Content { get; set; }
    }
}
