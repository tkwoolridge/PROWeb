using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PROWeb.Data.Authentication.Models
{
    public class PRORole : IdentityRole<int>
    {
        [StringLength(50)]
        public string Description { get; set; } = null!;
    }
}
