namespace PROWeb.Components.ViewModels.Person
{
    public interface IPersonDetailsViewModel
    {
        int VoterId { get; set; }

        string? FirstName { get; set; }

        string? LastName { get; set; }

        string? MaidenName { get; set; }

        string? MiddleName { get; set; }

        string? Title { get; set; }

        char Gender { get; set; }

        DateTime DateOfBirth { get; set; }
    }
}
