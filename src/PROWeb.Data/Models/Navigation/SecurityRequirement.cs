namespace PROWeb.Data.Models.Navigation
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
