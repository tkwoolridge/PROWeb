using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROWeb.Authentication.Models
{
    public abstract class PROIdentityDbContext<TUser> : IdentityDbContext<TUser>
        where TUser : IdentityUser, IPROUser
    {
        private readonly IConfiguration _configuration;

        public PROIdentityDbContext(IConfiguration configuration)
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
