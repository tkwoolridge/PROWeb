using AutoMapper;
using PROWeb.Components.ViewModels.Assessment;
using PROWeb.Components.ViewModels.Navigation;
using PROWeb.Data.Models;
using PROWeb.Data.Models.Navigation;

namespace PROWeb.Components.Mapping
{
    public abstract class MapperProfile : Profile
    {
        protected MapperProfile()
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
        }
    }
}
