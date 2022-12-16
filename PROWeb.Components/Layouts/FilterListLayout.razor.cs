using Microsoft.AspNetCore.Components;
using PROWeb.Common.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROWeb.Components.Layouts
{
    public partial class FilterListLayout : PROContentLayout
    {
        [Parameter]
        public RenderFragment? Filter { get; set; }

        [Parameter]
        public RenderFragment? List { get; set; }

        [Parameter]
        public RenderFragment? Footer { get; set; }
    }
}
