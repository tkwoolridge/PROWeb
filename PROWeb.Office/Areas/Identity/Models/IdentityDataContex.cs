using Microsoft.EntityFrameworkCore;
using PROWeb.Authentication.Models;

namespace PROWeb.Office.Areas.Identity.Models
{
    public class IdentityDataContext : PROIdentityDbContext<PROUser>
    {
        public IdentityDataContext(IConfiguration configuration)
           : base(configuration)
        {
        }
    }
}
