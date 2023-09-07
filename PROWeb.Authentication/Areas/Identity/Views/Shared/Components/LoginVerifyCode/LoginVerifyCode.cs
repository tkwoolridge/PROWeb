using Microsoft.AspNetCore.Mvc;
using PROWeb.Authentication.ViewModels.Account;
using PROWeb.Common.Attributes;


namespace PROWeb.Authentication.Areas.Identity.Views.Shared.Components.LoginVerifyCode
{
    [ViewComponent]
    public class LoginVerifyCode : ViewComponent
    {
        [RestoreModelStateFromTempData]
        public IViewComponentResult Invoke(string name, LoginVerificationCodeViewModel? model)
        {
            model ??= new LoginVerificationCodeViewModel();

            return View("LoginVerifyCode", model);
        }
    }
}
