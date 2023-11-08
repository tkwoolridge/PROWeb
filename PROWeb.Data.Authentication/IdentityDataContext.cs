using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PROWeb.Data.Authentication.Models;

namespace PROWeb.Data.Authentication
{
    public class IdentityDataContext : IdentityDbContext<PROUser>
    {
        private readonly IConfiguration _configuration;

        public IdentityDataContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("PROWebIdentityConnection"),
                options =>
            {
                // See https://go.microsoft.com/fwlink/?linkid=2134277, https://github.com/dotnet/efcore/issues/22580 for more information.

                options.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery);
            });
        }
    }
}
