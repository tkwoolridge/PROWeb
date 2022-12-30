using AutoMapper;
using Microsoft.AspNetCore.Components;

namespace PROWeb.Common.Components;

public abstract class PROLayout : LayoutComponentBase
{
    [Inject]
    protected IMapper Mapper { get; set; } = null!;

    protected string? Title { get; set; }

    public void SetPageTitle(string title)
    {
        this.Title = title;
    }
}
