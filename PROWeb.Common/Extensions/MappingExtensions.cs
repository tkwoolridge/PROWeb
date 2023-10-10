// Ignore Spelling: queryable

using Mapster;
using Microsoft.EntityFrameworkCore;

namespace PROWeb.Common.Extensions
{
    public static class MappingExtensions
    {
        public static async Task<IList<TView>> ProjectToListAsync<TView> (this IQueryable queryable)
        {
            return await queryable.ProjectToType<TView>().ToListAsync();
        }
    }
}
