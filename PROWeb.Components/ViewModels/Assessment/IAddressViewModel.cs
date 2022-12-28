using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PROWeb.Components.ViewModels.Person;

namespace PROWeb.Components.ViewModels.Assessment
{
    public interface IAddressViewModel 
    {
        int AssessmentNo { get; set; }

        string? Address1 { get; set; }

        string? HouseNo { get; set; }

        string? Address2 { get; set; }

        string? PostalCode { get; set; }

        string? ParishName { get; set; }

        int ConstituencyNo { get; set; }

        string? ConstituencyName { get; set; }

        bool? IsBogusNo { get; set; }

        string? BogusNo { get; set; }

        int? BogusConstituencyNo { get; set; }

        string? BogusConstituencyName { get; set; }
    }
}
