using System.ComponentModel.DataAnnotations;

namespace NetCoreAppUserIdentity.Models.ViewModel
{
    public class LoginVm
    {
        [EmailAddress(ErrorMessage = "Lütfen email formatında bir adres girin")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Şifre boş geçilemez!")]
        public string Password { get; set; }
    }
}
