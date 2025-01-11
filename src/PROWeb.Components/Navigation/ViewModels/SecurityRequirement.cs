namespace PROWeb.Components.Navigation.ViewModels
{
    public enum SecurityRequirement
    {
        None,

        CanUseAdministration,

        CanChangeFormStatus,

        CanSearchRegistrySnapshots,

        CanRunReports,

        CanUpdateVoterRegistration,

        CanUpdateIDs
    }
}
