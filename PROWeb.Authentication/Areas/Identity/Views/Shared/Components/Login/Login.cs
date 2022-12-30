using Microsoft.AspNetCore.Mvc;
using PROWeb.Authentication.ViewModels.Account;
using PROWeb.Common.Attributes;

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
