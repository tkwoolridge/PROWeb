using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROWeb.Common.ViewModels
{
    public abstract class BindableContext<TViewModel> : SlimViewModelBase where TViewModel : SlimViewModelBase
    {
        public TViewModel? Model { get; set; }

        public abstract void UnBind();
    }
}
