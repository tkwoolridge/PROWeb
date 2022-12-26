using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PROWeb.Common.ViewModels;

namespace PROWeb.Components.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<TViewModel> ProjectToQueryable<TViewModel>(this IQueryable source, IMapper mapper, IDictionary<string, object>? parameters = null) where TViewModel : ViewModelBase
        {
            return source.ProjectTo<TViewModel>(mapper.ConfigurationProvider, parameters);
        }

        public static TViewModel MapTo<TViewModel>(this object source, IMapper mapper) where TViewModel : ViewModelBase
        {
            return mapper.Map<TViewModel>(source);
        }

        public static IList<TViewModel> ProjectToList<TViewModel>(this IQueryable source, IMapper mapper, IDictionary<string, object>? parameters = null) where TViewModel : ViewModelBase
        {
            return source.ProjectToQueryable<TViewModel>(mapper, parameters).ToList();
        }

        public static async Task<IList<TViewModel>> ProjectToListAsync<TViewModel>(this IQueryable source, IMapper mapper, IDictionary<string, object>? parameters = null) where TViewModel : ViewModelBase
        {
            return await source.ProjectToQueryable<TViewModel>(mapper, parameters).ToListAsync();
        }
    }
}
