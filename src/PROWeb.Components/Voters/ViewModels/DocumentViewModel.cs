using PROWeb.Common.ViewModels;

namespace PROWeb.Components.Voters.ViewModels
{
    public class DocumentViewModel : SlimViewModelBase
    {
        private int _documentId;

        public int DocumentId
        {
            get => _documentId;
            set => RaiseAndSetIfChanged(ref _documentId, value);
        }

        private int _personId;

        public int PersonId
        {
            get => _personId;
            set => RaiseAndSetIfChanged(ref _personId, value);
        }

        private int _registryYear;

        public int RegistryYear
        {
            get => _registryYear;
            set => RaiseAndSetIfChanged(ref _registryYear, value);
        }

        private DateTime _documentDate;

        public DateTime DocumentDate
        {
            get => _documentDate;
            set => RaiseAndSetIfChanged(ref _documentDate, value);
        }

        private string? _documentName;

        public string? DocumentName
        {
            get => _documentName;
            set => RaiseAndSetIfChanged(ref _documentName, value);
        }

        private string? _documentDescription;

        public string? DocumentDescription
        {
            get => _documentDescription;
            set => RaiseAndSetIfChanged(ref _documentDescription, value);
        }

        private string? _exportFormat;

        public string? ExportFormat
        {
            get => _exportFormat;
            set => RaiseAndSetIfChanged(ref _exportFormat, value);
        }

        private byte[]? _content;

        public byte[]? Content
        {
            get => _content;
            set => RaiseAndSetIfChanged(ref _content, value);
        }
    }
}
