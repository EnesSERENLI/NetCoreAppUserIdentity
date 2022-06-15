using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NetCoreAppUserIdentity.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles ="admin",Policy ="")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
