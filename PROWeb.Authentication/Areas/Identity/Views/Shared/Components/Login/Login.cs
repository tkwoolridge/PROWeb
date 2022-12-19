using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PROWeb.Authentication.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PROWeb.Common.Attributes;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace PROWeb.Authentication.Areas.Identity.Shared.Components.Login
{
    [ViewComponent]
    public class Login : ViewComponent
    {
        [RestoreModelStateFromTempData]
        public IViewComponentResult Invoke(string name, LoginViewModel? model)
        {
            model ??= new LoginViewModel();

            return View("Login", model);
        }
    }
}
