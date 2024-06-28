using System.ComponentModel.DataAnnotations;

namespace CompanyInfo.MVCUI.Models.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage ="Email alani Zorunludur")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage ="Şifre girilmesi Zorunludur")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
