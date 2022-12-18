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

namespace PROWeb.Authentication.Areas.Identity.Pages.Shared.Components.Login
{
    [ViewComponent]
    public class Login : ViewComponent
    {
        [RestoreModelStateFromTempData]
        public IViewComponentResult Invoke(string name)
        {
            LoginViewModel model = new LoginViewModel();

            //if(TempData["ModelState"] is string jsonState)
            //{
            //    var errors = JsonConvert.DeserializeObject<SerializableError>(jsonState);

            //    if (errors != null)
            //    {
            //        ModelStateDictionary dictionary = new ModelStateDictionary();

            //        foreach (var error in errors)
            //        {
            //            dictionary.AddModelError(error.Key, error.Value.ToString());
            //        }

            //        ModelState.Merge(dictionary);
            //    }
            //}


            return View("Login", model);
        }
    }
}
