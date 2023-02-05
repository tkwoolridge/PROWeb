using Microsoft.AspNetCore.Components;
using PROWeb.Common.Components;

namespace PROWeb.Components.Person
{
    public abstract partial class PhotosView : PROComponent
    {
        [Parameter]
        public bool ShowTCDPhoto { get; set; } = true;
        [Parameter]
        public bool ShowPROPhoto { get; set; } = true;

        [Parameter]
        public string? TCDPhotoTitle { get; set; } = "TCD";

        [Parameter]
        public string? PROPhotoTitle { get; set; } = "PRO";
    }
}
