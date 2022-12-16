namespace PROWeb.Components.ViewModels.Navigation
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
