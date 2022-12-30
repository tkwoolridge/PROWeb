using PROWeb.Components.Mapping;
using PROWeb.Components.ViewModels.Voter;
using PROWeb.Data.Models;
using PROWeb.Office.ViewModels.Voter;

namespace PROWeb.Office.Mapper
{
    public class OfficeMapperProfile : MapperProfile
    {
        public OfficeMapperProfile() : base()
        {
            CreateMap<Voter, ListVoterViewModel>().IncludeBase<Voter, VoterViewModel>();
        }
    }
}
