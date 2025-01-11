using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PROWeb.Data.Authentication.Models;

namespace PROWeb.Data.Authentication
{
    public class IdentityDataContext : IdentityDbContext<PROUser, PRORole, int>
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            SeedPRORoles(builder);
        }

        private void SeedPRORoles(ModelBuilder builder)
        {
            builder.Entity<PRORole>().HasData(
                new PRORole() { Id = 1, Name = "Administrator", ConcurrencyStamp = "1", NormalizedName = "Administrator", Description = "Administrator" },
                new PRORole() { Id = 2, Name = "FrontOfficer", ConcurrencyStamp = "2", NormalizedName = "Front Officer", Description = "Front Officer" }
                );
        }
    }
}
