namespace PROWeb.Components.ViewModels.Voter
{
    public interface IVoterContactInfoViewModel : IVoterViewModel
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
