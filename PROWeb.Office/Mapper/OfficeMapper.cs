using Mapster;
using PROWeb.Components.Voters.ViewModels;
using PROWeb.Data.Models;
using PROWeb.Office.Shared.Registrations.ViewModels;
using PROWeb.Office.Shared.Registry.ViewModels;

namespace PROWeb.Office.Mapper
{
    public class OfficeMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            //config.NewConfig<Voter, VoterViewModel>()
            //    .Map(d => d.BogusConstituencyNo, s => s.BogusConstituency != null ? s.BogusConstituency.ConstituencyNo : (int?)null)
            //    .Map(d => d.BogusConstituencyName, s => s.BogusConstituency != null ? s.BogusConstituency.ConstituencyName : null)
            //    .Inherits<Voter, ListVoterViewModel>();

            config.NewConfig<Voter, ListVoterViewModel>()
                .Inherits<Voter, VoterViewModel>();

            config.NewConfig<Registration, RegistrationViewModel>()
                .Map(d => d.OldAssessmentNo, s => s.AssessmentNo)
                .Map(d => d.OldHouseNo, s => s.OldAssessment.HouseNo)
                .Map(d => d.OldAddress1, s => s.OldAssessment.Address1)
                .Map(d => d.OldAddress1, s => s.OldAssessment.Address1)
                .Map(d => d.OldAddress2, s => s.OldAssessment.Address2)
                .Map(d => d.OldPostalCode, s => s.OldAssessment.PostalCode)
                .Map(d => d.OldParishName, s => s.OldAssessment.Parish.ParishName)
                .Map(d => d.OldConstituencyNo, s => s.OldAssessment.ConstituencyNo)
                .Map(d => d.OldConstituencyName, s => s.OldAssessment.Constituency.ConstituencyName);
                
            config.NewConfig<Registration, ListRegistrationViewModel>()
                .Inherits<Registration, RegistrationViewModel>();

            config.NewConfig<VoterViewModel, RegistrationViewModel>()
                .Map(d => d.OldAssessmentNo, s => s.AssessmentNo)
                .Map(d => d.OldHouseNo, s => s.HouseNo)
                .Map(d => d.OldAddress1, s => s.Address1)
                .Map(d => d.OldAddress1, s => s.Address1)
                .Map(d => d.OldAddress2, s => s.Address2)
                .Map(d => d.OldPostalCode, s => s.PostalCode)
                .Map(d => d.OldParishName, s => s.ParishName)
                .Map(d => d.OldConstituencyNo, s => s.ConstituencyNo)
                .Map(d => d.OldConstituencyName, s => s.ConstituencyName)
                .Map(d => d.OldIsBogusNo, s => s.IsBogusNo)
                .Map(d => d.OldBogusNo, s => s.BogusNo)
                .Map(d => d.OldBogusConstituencyName, s => s.BogusConstituencyNo);
        }
    }
}
