using Microsoft.AspNetCore.Components.Forms;

namespace PROWeb.Common.Components
{

    public class PROViewContext<TViewModel> where TViewModel : class
    {
        public TViewModel Model { get; }

        public EditContext? EditContext { get; }

        public bool IsEditable { get; }

        public readonly IList<PROView<TViewModel>> Views = new List<PROView<TViewModel>>();

        internal void AddView(PROView<TViewModel> view)
        {
            Views.Add(view);
        }

        public PROViewContext(TViewModel model, EditContext? context = null, bool isEditable = false)
        {
            Model = model;
            EditContext = context;
            IsEditable = EditContext != null;
        }
    }
}
