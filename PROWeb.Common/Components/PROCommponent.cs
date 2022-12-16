using Humanizer;
using Microsoft.AspNetCore.Components;

namespace PROWeb.Common.Components
{
    public abstract class PROComponent : ComponentBase
    {
        [Inject]
        private NavigationManager _navigationManager { get; set; } = null!;

        [CascadingParameter]
        public PROContentLayout? Layout { get; set; }

        [CascadingParameter]
        public PROLayout? MainLayout { get; set; }

        protected virtual string? PageTitle { get; private set; }

        protected override void OnInitialized()
        {
            if (IsRouted() && 
                MainLayout is { } layout)
            {
                PageTitle ??= _navigationManager.Uri.Split('/').Last().Humanize(LetterCasing.Title);

                MainLayout.SetPageTitle(PageTitle);
            }
        }

        private bool IsRouted()
        {
            var attributes = GetType().GetCustomAttributes(inherit: true);

            return attributes.OfType<RouteAttribute>().Any();
        }
    }
}
