using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.DTOs.UserDTO
{
    public class RegisterUserDTO
    {
        [Required]
        public string? UserName { get; set; }
        [Required, EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Passwords dont match")]
        public string? ConfirmPassword { get; set; }

    }
}
