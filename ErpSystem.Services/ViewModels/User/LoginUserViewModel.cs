using System;
using System.ComponentModel.DataAnnotations;

namespace ErpSystem.Services.ViewModels.User
{
    public class LoginUserViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Please enter valid email address")]
        public string Email { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Password length should be at least 6 characters.")]
        public string Password { get; set; }
    }
}
