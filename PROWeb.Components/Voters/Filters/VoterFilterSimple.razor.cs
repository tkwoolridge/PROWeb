using PROWeb.Common.Components;
using PROWeb.Components.ViewModels.Voter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROWeb.Components.Voters.Filters
{
    public partial class VoterFilterSimple : PROFilterComponent<VoterFilterViewModel>
    {
        protected override void OnInitialized()
        {
            base.OnInitialized();

            ResetFilter();
        }

        protected override void ResetFilter()
        {       
            base.ResetFilter();
            Filter.RegistryYear = 2022;
        }
    }
}
