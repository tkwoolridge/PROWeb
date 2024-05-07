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
                .Map(d => d.AssessmentLatitude, s => s.Assessment.Latitude)
                .Map(d => d.AssessmentLongitude, s => s.Assessment.Longitude)
                .Map(d => d.IsAssessmentBogus, s => s.Assessment.IsBogus)
                .Map(d => d.OldAssessmentNo, s => s.OldAssessment.AssessmentNo)
                .Map(d => d.OldHouseNo, s => s.OldAssessment.HouseNo)
                .Map(d => d.OldAddress1, s => s.OldAssessment.Address1)
                .Map(d => d.OldAddress1, s => s.OldAssessment.Address1)
                .Map(d => d.OldAddress2, s => s.OldAssessment.Address2)
                .Map(d => d.OldPostalCode, s => s.OldAssessment.PostalCode)
                .Map(d => d.OldParishName, s => s.OldAssessment.Parish.ParishName)
                .Map(d => d.OldConstituencyNo, s => s.OldAssessment.ConstituencyNo)
                .Map(d => d.OldConstituencyName, s => s.OldAssessment.Constituency.ConstituencyName)
                .Map(d => d.OldAssessmentLatitude, s => s.OldAssessment.Latitude)
                .Map(d => d.OldAssessmentLongitude, s => s.OldAssessment.Longitude)
                .Map(d => d.OldIsBogus, s => s.OldAssessment.IsBogus);

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
                .Map(d => d.OldAssessmentLatitude, s => s.AssessmentLatitude)
                .Map(d => d.OldAssessmentLongitude, s => s.AssessmentLongitude)
                .Map(d => d.OldIsBogus, s => s.IsAssessmentBogus);

            config.NewConfig<RegistrationViewModel, VoterViewModel>()
               .Map(d => d.AssessmentNo, s => s.AssessmentNo)
               .Map(d => d.HouseNo, s => s.HouseNo)
               .Map(d => d.Address1, s => s.Address1)
               .Map(d => d.Address1, s => s.Address1)
               .Map(d => d.Address2, s => s.Address2)
               .Map(d => d.PostalCode, s => s.PostalCode)
               .Map(d => d.ParishName, s => s.ParishName)
               .Map(d => d.ConstituencyNo, s => s.ConstituencyNo)
               .Map(d => d.ConstituencyName, s => s.ConstituencyName)
               .Map(d => d.AssessmentLatitude, s => s.AssessmentLatitude)
               .Map(d => d.AssessmentLongitude, s => s.AssessmentLongitude)
               .Map(d => d.IsAssessmentBogus, s => s.IsAssessmentBogus);
        }
    }
}
