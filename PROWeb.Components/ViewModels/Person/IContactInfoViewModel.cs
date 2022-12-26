namespace PROWeb.Components.ViewModels.Person
{
    public interface IContactInfoViewModel : IPersonDocumentsViewModel
    {
        string? Email { get; set; }

        string? ContactPhone { get; set; }

        string? PhoneHome { get; set; }

        string? PhoneWork { get; set; }

        string? PhoneMobile { get; set; }

        string? DriverLicense { get; set; }

        string? Comment { get; set; }
    }
}
