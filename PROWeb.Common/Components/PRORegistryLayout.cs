using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROWeb.Common.Components
{
    public abstract class PRORegistryLayout<TFilter, TItem> : PROComponent where TFilter: class, new() 
    {
        protected PROFilterComponent<TFilter>? FilterRef { get; set; }
        protected PROListComponent<TFilter,TItem>? ListRef { get; set; }

        public async Task OnFilterAsync(TFilter filter)
        {
            if (ListRef == null)
            {
                return;
            }

            await ListRef.FilterAsync(filter);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (FilterRef == null)
            {
                return;    
            }

            FilterRef.SetFilterCallBack(OnFilterAsync);

            await base.OnAfterRenderAsync(firstRender);
        }
    }
}
