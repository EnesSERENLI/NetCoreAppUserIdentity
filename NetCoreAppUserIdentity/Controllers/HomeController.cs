using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NetCoreAppUserIdentity.Models.Entity;
using NetCoreAppUserIdentity.Models.ViewModel;
using System.Threading.Tasks;

namespace NetCoreAppUserIdentity.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public HomeController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser();
                user.Email = registerVM.Email;
                user.UserName = registerVM.UserName;

                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    return View();
                }
                else
                {
                    return View();
                }
            }
            return View();
        }
    }
}
