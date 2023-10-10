using Microsoft.AspNetCore.Components;

namespace PROWeb.Common.Components;

public abstract class PROLayout : LayoutComponentBase
{
    protected string? Title { get; set; }

    public void SetPageTitle(string title)
    {
        this.Title = title;
    }
}
