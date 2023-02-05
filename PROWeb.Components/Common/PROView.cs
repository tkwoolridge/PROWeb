using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Metadata;
using PROWeb.Common.Components;
using PROWeb.Common.ViewModels;
using PROWeb.Components.Layouts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROWeb.Components.Common
{
    public abstract class PROView<TViewModel> : PROComponent where TViewModel : SlimViewModelBase
    {
        private TViewModel? _model;

        [Parameter]
        public string? Title { get; set; }

        [CascadingParameter]
        public ViewsLayout<TViewModel>? ParentLayout { get; set; }

        [CascadingParameter]
        public TViewModel? Model
        {
            get => _model;
            set
            {
                if (_model != value)
                {
                    _model = value;
                    OnModelUpdate();
                }
            }
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (ParentLayout is { } layout)
            {
                layout.AddView(this);
            }
        }

        protected virtual void OnModelUpdate()
        {
        }
    }
}
