using AutoMapper;
using PROWeb.Components.ViewModels.Assessment;
using PROWeb.Components.ViewModels.Navigation;
using PROWeb.Components.ViewModels.Voter;
using PROWeb.Data.Models;
using PROWeb.Data.Models.Navigation;

namespace PROWeb.Components.Mapping
{
    public class MapperProfile : Profile
    {
        public readonly static MapperConfiguration Configuration = new MapperConfiguration(cfg => cfg.AddProfile(new MapperProfile()));
        public readonly static IMapper Mapper = Configuration.CreateMapper();

        private MapperProfile()
        {
            CreateMap<MenuItem, MenuItemViewModel>()
            .ForMember(vm => vm.Level, options => options.Ignore());
            CreateMap<Menu, MenuViewModel>();

            CreateMap<Constituency, ConstituencyViewModel>();
            CreateMap<Parish, ParishViewModel>();
            CreateMap<AssessmentFlag, AssessmentFlagViewModel>();
            CreateMap<Assessment, AssessmentViewModel>()
            .ForMember(vm => vm.Constituency, options => options.Ignore())
            .ForMember(vm => vm.ConstituencyName, options => options.MapFrom(a => a.Constituency.ConstituencyName))
            .ForMember(vm => vm.ParishName, options => options.MapFrom(a => a.Parish.ParishName));

            CreateMap<VoterFlag, VoterFlagViewModel>();
            CreateMap<VoterDocument, VoterDocumentViewModel>();
            CreateMap<Constituency, VoterViewModel>();
            CreateMap<Parish, VoterViewModel>();
            CreateMap<Country, VoterViewModel>();
            CreateMap<Assessment, VoterViewModel>()
            .IncludeMembers(a => a.Constituency, a => a.Parish);
            CreateMap<Voter, VoterViewModel>()
            .IncludeMembers(v => v.Assessment, v => v.Country)
            .ForMember(vm => vm.BogusConstituencyNo, options => options.MapFrom(c => c.BogusConstituency != null ? c.BogusConstituency.ConstituencyNo : (int?)null))
            .ForMember(vm => vm.BogusConstituencyName, options => options.MapFrom(c => c.BogusConstituency != null ? c.BogusConstituency.ConstituencyName : null));
        }
    }
}
