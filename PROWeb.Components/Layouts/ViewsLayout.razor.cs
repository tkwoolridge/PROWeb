using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using PROWeb.Common.Components;
using PROWeb.Components.Common;

namespace PROWeb.Components.Layouts
{
    public abstract partial class ViewsLayout<TViewModel> : PROContentLayout where TViewModel : class
    {
        private IList<PROView<TViewModel>> _views = new List<PROView<TViewModel>>();

        [CascadingParameter]
        public TViewModel? Model { get; set; }

        [Parameter]
        public RenderFragment? Views { get; set; }

        public bool Editable { get; set; }

        public bool CanSave { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        private void OnFieldChanged(object? sender, FieldChangedEventArgs e)
        {
            CanSave = true;

            StateHasChanged();
        }

        internal void AddView(PROView<TViewModel> view)
        {
            _views.Add(view);
        }

        public virtual async Task<bool> OnSaveAsync()
        {
            if (!Validate())
            {
                return false;
            }

            foreach (var view in _views)
            {
                view.OnSave();
            }

            await SaveAsync();

            return true;
        }

        public abstract Task SaveAsync();

        public bool Validate()
        {
            List<string>? errors = new List<string>();

            foreach (var view in _views)
            {
                if (view.OnValidate() is { } viewErrors)
                {
                    errors?.AddRange(viewErrors);
                }
            }

            return errors != null;
        }
    }
}
