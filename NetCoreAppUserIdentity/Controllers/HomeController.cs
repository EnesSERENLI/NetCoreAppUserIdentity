using Microsoft.AspNetCore.Mvc;

namespace NetCoreAppUserIdentity.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
