using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROWeb.Common.Components;

public abstract class PROLayout : LayoutComponentBase
{
    protected string? Title { get; set; }

    public void SetPageTitle(string title)
    {
        this.Title = title;
    }
}
