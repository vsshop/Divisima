using Microsoft.AspNetCore.Mvc;
using WebApplication48.Models;
using WebApplication48.Services;

namespace WebApplication48.Controllers
{
    public class ApiProductController : Controller
    {
        ProductServices _productServices;
        public ApiProductController(ProductServices productServices)
        {
            _productServices = productServices;
        }
        public async Task<IActionResult> GetProducts()
        {
            var list = await _productServices.GetProducts();
            return Json(list);
        }
    }
}
