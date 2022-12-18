using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROWeb.Components.ViewModels.Voter
{
    public interface IVoterFlagsViewModel
    {
        bool? CommonwealthCitizen { get; set; }

        DateTime? BermudianStatusGranted { get; set; }

        bool? RegisteredAsElector { get; set; }

        bool? IsBermudianStatusGranted { get; set; }

        ICollection<VoterFlagViewModel> VoterFlags { get; set; }
    }
}
