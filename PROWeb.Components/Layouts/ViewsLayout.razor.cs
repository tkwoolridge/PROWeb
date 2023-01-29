using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using PROWeb.Common.Components;
using PROWeb.Components.Common;
using Telerik.Blazor;

namespace PROWeb.Components.Layouts
{
    public abstract partial class ViewsLayout<TViewModel> : PROContentLayout where TViewModel : class
    {
        [CascadingParameter]
        public DialogFactory Dialogs { get; set; } = null!;

        private IList<PROView<TViewModel>> _views = new List<PROView<TViewModel>>();

        [CascadingParameter]
        public TViewModel? Model { get; set; }

        [Parameter]
        public RenderFragment? Views { get; set; }

        [Parameter]
        public bool Editable { get; set; }

        public bool CanSave { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        internal void OnFieldChanged(object? sender, FieldChangedEventArgs e)
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
            if (Validate() is { } errors)
            {
                await Dialogs.AlertAsync(string.Join('\n', errors.Select((e, i) => $"{i+1}. {e}").ToList()).TrimEnd('\n'), "Validation Error");

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

        public List<string>? Validate()
        {
            List<string>? errors = new List<string>();

            foreach (var view in _views)
            {
                if (view.OnValidate() is { } viewErrors)
                {
                    errors?.AddRange(viewErrors);
                }
            }

            return errors.Any() ? errors : null;
        }
    }
}
