using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using WebApplication48.Models;
using WebApplication48.Services;

namespace WebApplication48.Controllers
{
	[Authorize]
	public class ApiUserController : Controller
    {
        UserService _userService;

		public ApiUserController(UserService userService)
        {
			_userService = userService;

		}

        public async Task<IActionResult> AppendProductToCart([FromBody] ProductModel model)
        {
            if (ModelState.IsValid)
            {
                var user = User.Identity?.Name;
				var cart = await _userService.AppendProductToCart(user, model);
                if(cart.Status == Models.StatusCode.Ok)
                {
					return Json(cart.Data);
				}
			}

            return BadRequest();
        }

        public async Task<IActionResult> GetCart()
        {
			var user = User.Identity?.Name;
			var cart = await _userService.GetCart(user);
			if (cart.Status == Models.StatusCode.Ok)
			{
				return Json(cart.Data);
			}
			return BadRequest();
		}
    }
}
