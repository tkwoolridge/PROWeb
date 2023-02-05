using PROWeb.Components.Mapping;
using PROWeb.Data.Models;
using PROWeb.Office.ViewModels.EligiblePoll;
using PROWeb.Office.ViewModels.Registration;
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
        }
    }
}
