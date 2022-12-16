using AutoMapper.QueryableExtensions;
using PROWeb.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROWeb.Components.Mapping
{
    public static class ViewModelExtensions
    {
        public static TViewModel Project<TViewModel>(this object source) where TViewModel : ViewModelBase
        {
            return MapperProfile.Mapper.Map<TViewModel>(source);
        }
    }
}
