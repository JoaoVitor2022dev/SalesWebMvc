using Microsoft.AspNetCore.Mvc;

namespace SalesWebMvcProject.Controllers
{
    public class SellersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
