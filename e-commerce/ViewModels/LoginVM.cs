using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Email adresinizi daxil etməlisiniz."), EmailAddress(ErrorMessage = "Zəhmət olmasa, düzgün elektron poçt daxil edin.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Şifrənizi daxil etməlisiniz."), DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
