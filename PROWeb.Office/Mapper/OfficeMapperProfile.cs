using PROWeb.Components.Mapping;
using PROWeb.Components.ViewModels.Voters;
using PROWeb.Data.Models;
using PROWeb.Office.ViewModels.EligiblePolls;
using PROWeb.Office.ViewModels.Forms;
using PROWeb.Office.ViewModels.Registrations;
using PROWeb.Office.ViewModels.Voters;

namespace PROWeb.Office.Mapper
{
    public class OfficeMapperProfile : MapperProfile
    {
        public OfficeMapperProfile() : base()
        {
            CreateMap<VoterFlag, VoterFlagViewModel>();
            CreateMap<Document, DocumentViewModel>();

            CreateMap<Constituency, VoterViewModel>();
            CreateMap<Parish, VoterViewModel>();
            CreateMap<Country, VoterViewModel>();
            
            CreateMap<Assessment, VoterViewModel>()
            .IncludeMembers(a => a.Constituency, a => a.Parish);

            CreateMap<Voter, VoterViewModel>()
            .IncludeMembers(v => v.Assessment, v => v.Country)
            
            .ForMember(vm => vm.BogusConstituencyNo, options => options.MapFrom(c => c.BogusConstituency != null ? c.BogusConstituency.ConstituencyNo : (int?)null))
            .ForMember(vm => vm.BogusConstituencyName, options => options.MapFrom(c => c.BogusConstituency != null ? c.BogusConstituency.ConstituencyName : null));

            CreateMap<Voter, ListVoterViewModel>().IncludeBase<Voter, VoterViewModel>();

            CreateMap<VoterFlagViewModel, VoterFlag>();
            CreateMap<DocumentViewModel, Document>();

            CreateMap<VoterViewModel, Voter>();
            CreateMap<Eligible, EligibleViewModel>();

            CreateMap<FormType, FormTypeViewModel>();

            CreateMap<Constituency, RegistrationViewModel>();
            CreateMap<Parish, RegistrationViewModel>();
            CreateMap<Country, RegistrationViewModel>();

            CreateMap<Assessment, RegistrationViewModel>()
            .IncludeMembers(a => a.Constituency, a => a.Parish);

            CreateMap<Registration, RegistrationViewModel>()
            .IncludeMembers(r => r.OldAssessment, r => r.Assessment, r => r.Country)
            .ForMember(vm => vm.OldAssessmentNo, options => options.MapFrom(v => v.AssessmentNo))
            .ForMember(vm => vm.OldHouseNo, options => options.MapFrom(r => r.OldAssessment.HouseNo))
            .ForMember(vm => vm.OldAddress1, options => options.MapFrom(r => r.OldAssessment.Address1))
            .ForMember(vm => vm.OldAddress1, options => options.MapFrom(r => r.OldAssessment.Address1))
            .ForMember(vm => vm.OldAddress2, options => options.MapFrom(r => r.OldAssessment.Address2))
            .ForMember(vm => vm.OldPostalCode, options => options.MapFrom(r => r.OldAssessment.PostalCode))
            .ForMember(vm => vm.OldParishName, options => options.MapFrom(r => r.OldAssessment.Parish.ParishName))
            .ForMember(vm => vm.OldConstituencyNo, options => options.MapFrom(r => r.OldAssessment.ConstituencyNo))
            .ForMember(vm => vm.OldConstituencyName, options => options.MapFrom(r => r.OldAssessment.Constituency.ConstituencyName));

            CreateMap<VoterViewModel, RegistrationViewModel>()
            .ForMember(vm => vm.OldAssessmentNo, options => options.MapFrom(v => v.AssessmentNo))
            .ForMember(vm => vm.OldHouseNo, options => options.MapFrom(v => v.HouseNo))
            .ForMember(vm => vm.OldAddress1, options => options.MapFrom(v => v.Address1))
            .ForMember(vm => vm.OldAddress1, options => options.MapFrom(v => v.Address1))
            .ForMember(vm => vm.OldAddress2, options => options.MapFrom(v => v.Address2))
            .ForMember(vm => vm.OldPostalCode, options => options.MapFrom(v => v.PostalCode))
            .ForMember(vm => vm.OldParishName, options => options.MapFrom(v => v.ParishName))
            .ForMember(vm => vm.OldConstituencyNo, options => options.MapFrom(v => v.ConstituencyNo))
            .ForMember(vm => vm.OldConstituencyName, options => options.MapFrom(v => v.ConstituencyName))
            .ForMember(vm => vm.OldIsBogusNo, options => options.MapFrom(v => v.IsBogusNo))
            .ForMember(vm => vm.OldBogusNo, options => options.MapFrom(v => v.BogusNo))
            .ForMember(vm => vm.OldBogusConstituencyName, options => options.MapFrom(v => v.BogusConstituencyNo));

            CreateMap<EligibleViewModel, RegistrationViewModel>();

            CreateMap<Registration, ListRegistrationViewModel>().IncludeBase<Registration, RegistrationViewModel>();
        }
    }
}
