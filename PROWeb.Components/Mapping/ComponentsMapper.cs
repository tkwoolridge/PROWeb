using Mapster;
using PROWeb.Components.Assessments.ViewModels;
using PROWeb.Components.EligiblePolls.ViewModels;
using PROWeb.Components.Navigation.ViewModels;
using PROWeb.Components.Voters.ViewModels;
using PROWeb.Data.Models;
using PROWeb.Data.Models.Navigation;

namespace PROWeb.Components.Mapping
{
    public class ComponentsMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<MenuItem, MenuItemViewModel>()
                .Ignore(d => d.Level);

            config.NewConfig<ConstituencyBoundary, ConstituencyMarkerViewModel>()
                .Map(d => d.Center, s => new[] { s.CenterLatitude, s.CenterLongitude});

            config.NewConfig<Assessment, AssessmentViewModel>()
                .IgnoreIf((s, d) => d.Constituency != null, d => d.Constituency!)
                .Map(d => d.ConstituencyName, s => s.Constituency.ConstituencyName)
                .Map(d => d.ParishName, s => s.Parish.ParishName);

            config.NewConfig<EligibleViewModel, AssessmentViewModel>()
                .IgnoreIf((s, d) => d.Constituency != null, d => d.Constituency!)
                .Map(d => d.AssessmentNo, s => s.DriverLicenseAssessmentNo)
                .Map(d => d.HouseNo, s => s.DriverLicenseHouseNo)
                .Map(d => d.Address1, s => s.DriverLicenseAddress1)
                .Map(d => d.Address2, s => s.DriverLicenseAddress2)
                .Map(d => d.ParishName, s => s.DriverLicenseParishName)
                .Map(d => d.PostalCode, s => s.DriverLicensePostalCode)
                .Map(d => d.ConstituencyNo, s => s.DriverLicenseConstituencyNo)
                .Map(d => d.ConstituencyName, s => s.DriverLicenseConstituencyName)
                .Map(d => d.Longitude, s => s.DriverLicenseAssessmentLongitude)
                .Map(d => d.Latitude, s => s.DriverLicenseAssessmentLatitude);

            config.NewConfig<Voter, VoterViewModel>()
                .Map(d => d.HouseNo, s => s.Assessment.HouseNo)
                .Map(d => d.Address1, s => s.Assessment.Address1)
                .Map(d => d.Address2, s => s.Assessment.Address2)
                .Map(d => d.PostalCode, s => s.Assessment.PostalCode)
                .Map(d => d.ParishName, s => s.Assessment.Parish.ParishName)
                .Map(d => d.ConstituencyNo, s => s.Assessment.ConstituencyNo)
                .Map(d => d.ConstituencyName, s => s.Assessment.Constituency.ConstituencyName)
                .Map(d => d.AssessmentLatitude, s => s.Assessment.Latitude)
                .Map(d => d.AssessmentLongitude, s => s.Assessment.Longitude)
                .Map(d => d.IsAssessmentBogus, s => s.Assessment.IsBogus)
                .Map(d => d.CountryName, s => s.Country.CountryName);
        }
    }
}
