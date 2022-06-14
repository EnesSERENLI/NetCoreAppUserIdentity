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
        private readonly SignInManager<AppUser> _signInManager;

        public HomeController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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

                var result = await _userManager.CreateAsync(user,registerVM.Password);
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

        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(LoginVm loginVm)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(loginVm.Email); //Email'den db de var böyle bil kullanıcı olup olmadığını bakacak.
                if (user != null)
                {
                    var result = _signInManager.PasswordSignInAsync(user, loginVm.Password, false, false);
                    if (result.Result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("errLogin", "Hatalı giriş!!..");
                    }
                }
                return View();
            }
            else
            {
                return View();
            }
        }
    }
}
