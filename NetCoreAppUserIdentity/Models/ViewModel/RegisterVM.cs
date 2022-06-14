using System.ComponentModel.DataAnnotations;

namespace NetCoreAppUserIdentity.Models.ViewModel
{
    public class RegisterVM
    {
        [Required(ErrorMessage ="Kullanıcı Adı boş geçilemez!")]
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }
        [EmailAddress(ErrorMessage = "Kullanıcı Adı boş geçilemez!")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Şifre boş geçilemez!")]
        [Display(Name = "Şifre")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Şifre Tekrar boş geçilemez!")]
        [Compare("Password", ErrorMessage ="Şifreler aynı olmalıdır!")]
        [Display(Name ="Şifre Tekrar")]
        public string ConfirmPassword { get; set; }

    }
}
