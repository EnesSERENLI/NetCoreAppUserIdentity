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
                AppUser newUser = new AppUser();
                newUser.Email = registerVM.Email;
                newUser.UserName = registerVM.UserName;

                var result = await _userManager.CreateAsync(newUser,registerVM.Password); //Kullanıcıyı ve şifreyi ver. Passwordhash olduğu için burada veriyoruz.
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(registerVM.Email);
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);//Token oluştur..
                    return RedirectToAction("ConfirmEmail", "Home", new { id = user.Id, token = token });//Onaylamak için gönderilece action.. Burada mail atılıp kullanıcı tıkladığında da action'a yönlendirilebilir... 
                }
                else
                {
                    return View();
                }
            }
            return View();
        }

        public async Task<IActionResult> ConfirmEmail(string id, string token) //Kullanıcı kaydından sonra Guid bir id gelecek ve token gelecek.
        {
            if (id != null && token != null)
            {
                var user = await _userManager.FindByIdAsync(id); // kullanıcıyı veritabanında komtrol et..
                var confirm = await _userManager.ConfirmEmailAsync(user, token); // Kullanıcının emailini onayla.
                if (confirm.Succeeded)
                {
                    return RedirectToAction("SignIn"); // giriş sayfasına yönlendir.
                }
            }
            return View();
        }


        public IActionResult SignIn() // giriş sayfası
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(LoginVm loginVm)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(loginVm.Email); //Email'den db de kullanıcıyı kontrol et..
                if (user != null)
                {
                    var result = _signInManager.PasswordSignInAsync(user, loginVm.Password, false, false);
                    if (result.Result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("errLogin", "Hatalı giriş!!..");//bilgiler eksikse veya yanlışsa tekrar modelstate e gitsin.
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
