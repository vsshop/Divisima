using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using WebApplication48.Models;
using WebApplication48.Services;
using Microsoft.AspNetCore.Authorization;
using WebApplication48.Extensions;

namespace WebApplication48.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ProductServices _productServices;
        private readonly AuthorizationService _authorizationService;
        private readonly UserService _userService;
        

		public HomeController(ILogger<HomeController> logger, 
            ProductServices productServices, 
            AuthorizationService authorizationService,
            UserService userService)
        {
            _logger = logger;
            _productServices = productServices;
			_authorizationService = authorizationService;
            _userService = userService;

        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Checkout()
        {
            return View();
        }

        public async Task<IActionResult> Product(Guid product)
        {
            if(product != default)
            {
                var productModel = await _productServices.GetProductById(product);
                return View(productModel);
            }
            return View("404");
        }

        public async Task<IActionResult> SignIn(AuthorizationModel model)
        {
            if (ModelState.IsValid)
            {
				var response = await _authorizationService.SignIn(model);
                if(response.Status == Models.StatusCode.Ok)
                {
                    await this.SesionAdd(response.Data);
                    return RedirectToAction("Index");
				}
			}

			return RedirectToAction("Index");
		}
        
        public async Task<IActionResult> SignUp(RegistrationModel model)
        {
            if (ModelState.IsValid)
            {
				var response = await _authorizationService.SignUp(model);
                if(response.Status == Models.StatusCode.Ok)
                {
                    await this.SesionAdd(response.Data);
                    return RedirectToAction("Index");
				}
			}

			return RedirectToAction("Index");
		}

        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<IActionResult> Payment()
        {
            var login = User.Identity?.Name;
            var responce =  await _userService.CreatePayment(login);
            if (responce.Status == Models.StatusCode.Ok)
            {
                return Redirect(responce.Data);
            }
            return BadRequest();
        }

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}