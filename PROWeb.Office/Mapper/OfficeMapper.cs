using Mapster;
using PROWeb.Components.Voters.ViewModels;
using PROWeb.Data.Models;
using PROWeb.Office.Shared.Registrations.ViewModels;

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

            config.NewConfig<Registration, RegistrationViewModel>()
                .Map(d => d.AssessmentNo, s => s.Assessment.AssessmentNo)
                .Map(d => d.HouseNo, s => s.Assessment.HouseNo)
                .Map(d => d.Address1, s => s.Assessment.Address1)
                .Map(d => d.Address1, s => s.Assessment.Address1)
                .Map(d => d.Address2, s => s.Assessment.Address2)
                .Map(d => d.PostalCode, s => s.Assessment.PostalCode)
                .Map(d => d.ParishName, s => s.Assessment.Parish.ParishName)
                .Map(d => d.ConstituencyNo, s => s.Assessment.ConstituencyNo)
                .Map(d => d.ConstituencyName, s => s.Assessment.Constituency.ConstituencyName)
                .Map(d => d.OldAssessmentNo, s => s.OldAssessment.AssessmentNo)
                .Map(d => d.OldHouseNo, s => s.OldAssessment.HouseNo)
                .Map(d => d.OldAddress1, s => s.OldAssessment.Address1)
                .Map(d => d.OldAddress1, s => s.OldAssessment.Address1)
                .Map(d => d.OldAddress2, s => s.OldAssessment.Address2)
                .Map(d => d.OldPostalCode, s => s.OldAssessment.PostalCode)
                .Map(d => d.OldParishName, s => s.OldAssessment.Parish.ParishName)
                .Map(d => d.OldConstituencyNo, s => s.OldAssessment.ConstituencyNo)
                .Map(d => d.OldConstituencyName, s => s.OldAssessment.Constituency.ConstituencyName);
                
            //config.NewConfig<Registration, RegistrationViewModel>()
                //.Inherits<Registration, RegistrationViewModel>();

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
