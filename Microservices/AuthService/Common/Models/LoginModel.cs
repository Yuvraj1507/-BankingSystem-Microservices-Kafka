using System;
using System.ComponentModel.DataAnnotations;

namespace AuthService.Models
{
    public class LoginModel
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
