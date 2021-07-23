using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Ad familyanı daxil etməlisiniz!")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "İstifadəçi adınızı daxil etməlisiniz!")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Email adresinizi daxil etməlisiniz!"), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Şifrənizi daxil etməlisiniz!"), DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Təkrar şifrənizi düzgün daxil etməlisiniz!"), DataType(DataType.Password), Compare(nameof(Password),ErrorMessage ="Təkrar şifrəniz yanlışdır.")]
        public string ConfirmPassword { get; set; }
    }
}
