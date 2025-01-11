using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using PROWeb.Data.Authentication.Models;
using System.Security.Claims;

namespace PROWeb.Authentication.DependencyInjection
{
    public static class AuthenticationEndpointsExtensions
    {
        public static IEndpointConventionBuilder MapPROWebAuthenticationEndpoints(this IEndpointRouteBuilder endpoints)
        {
            ArgumentNullException.ThrowIfNull(endpoints);

            var accountGroup = endpoints.MapGroup("/Account");

            accountGroup.MapGet("/Logout", async (
                ClaimsPrincipal user,
                UserManager<PROUser> userManager,
                SignInManager<PROUser> signInManager,
                [FromQuery] string returnUrl) =>
            {
                await signInManager.SignOutAsync();
                await signInManager.ForgetTwoFactorClientAsync();

                if (user.Identity?.Name is { } userName &&
                    await userManager.FindByNameAsync(userName) is { } proUser)
                {
                    await userManager.UpdateSecurityStampAsync(proUser);
                }

                return TypedResults.LocalRedirect($"{returnUrl}");
            });

            return accountGroup;
        }
    }
}
