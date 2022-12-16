using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROWeb.Components.ViewModels.Voter
{
    public interface IVoterAssessmentViewModel : IVoterViewModel
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
