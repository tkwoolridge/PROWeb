using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PROWeb.Data.Models;

namespace PROWeb.Data
{
    public class IdentityDataContext : IdentityDbContext<PROUser>
    {

        public IdentityDataContext(DbContextOptions<IdentityDataContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(options =>
            {
                // See https://go.microsoft.com/fwlink/?linkid=2134277, https://github.com/dotnet/efcore/issues/22580 for more information.
                options.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery);
            });
        }
    }
}
