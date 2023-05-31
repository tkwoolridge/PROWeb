using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROWeb.Common.Components
{
    public class PROComponentBase : ComponentBase
    {
        [Parameter]
        public string? Class { get; set; }
    }
}
