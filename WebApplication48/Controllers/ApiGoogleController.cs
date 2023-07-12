using Microsoft.AspNetCore.Mvc;
using WebApplication48.Extensions;
using WebApplication48.Models;
using WebApplication48.Services;

namespace WebApplication48.Controllers
{
    public class ApiGoogleController : Controller
    {
        GoogleService _googleService;
        AuthorizationService _authorizationService;
        public ApiGoogleController(GoogleService google, AuthorizationService authorizationService)
        {
            _googleService = google;
            _authorizationService = authorizationService;

        }
        public IActionResult RedirectToGoogle()
        {
            string url = _googleService.CreateRedirectGoogle();
            return Redirect(url);
        }

        public async Task<IActionResult> GetGoogleCode(string code)
        {
            var tokenResult = await _googleService.GetGoogleToken(code);
            if (tokenResult.Status == Models.StatusCode.Ok)
            {
                await GetGooglePerson(tokenResult.Data);
            }
            return RedirectToAction("Index", "Home");
        }

        private async Task<IActionResult> GetGooglePerson(GoogleToken token)
        {
            var personResult = await _googleService.GetGooglePerson(token);
            if (personResult.Status == Models.StatusCode.Ok)
            {
                var userResult = await _authorizationService.SignUpGoogle(personResult.Data);
                if(userResult.Status == Models.StatusCode.Ok)
                {
                    await this.SesionAdd(userResult.Data);
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
