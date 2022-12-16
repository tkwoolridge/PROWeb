using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PROWeb.Common.ViewModels;
using PROWeb.Components.Mapping;

namespace PROWeb.Components.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<TViewModel> ProjectToQueryable<TViewModel>(this IQueryable source, IDictionary<string, object>? parameters = null) where TViewModel : ViewModelBase
        {
            return source.ProjectTo<TViewModel>(MapperProfile.Configuration, parameters);
        }

        public static IList<TViewModel> ProjectToList<TViewModel>(this IQueryable source, IDictionary<string, object>? parameters = null) where TViewModel : ViewModelBase
        {
            return source.ProjectTo<TViewModel>(MapperProfile.Configuration, parameters).ToList();
        }

        public static async Task<IList<TViewModel>> ProjectToListAsync<TViewModel>(this IQueryable source, IDictionary<string, object>? parameters = null) where TViewModel : ViewModelBase
        {
            return await source.ProjectTo<TViewModel>(MapperProfile.Configuration, parameters).ToListAsync();
        }
    }
}
