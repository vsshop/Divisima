using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication48.Models;

namespace WebApplication48.Extensions
{
    public static class AuthoriazationControllerExtension
    {

        public static async Task SesionAdd(this Controller controller, RegistrationModel model)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, model.Login),
            };

            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);
            await controller.HttpContext.SignInAsync(principal);
        }
    }
}
