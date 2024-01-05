using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROWeb.Data.Models
{
    public class ConstituencyBoundary
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ConstituencyId { get; set; }

        public string? ConstituencyName { get; set; }

        public string? Geometry { get; set; }

        public double CenterLongitude { get; set; }

        public double CenterLatitude { get; set; }
    }
}
