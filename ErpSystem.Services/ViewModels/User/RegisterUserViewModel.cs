using System;
using System.ComponentModel.DataAnnotations;

namespace ErpSystem.Services.ViewModels.User
{
    public class RegisterUserViewModel
    {
        //[Required(ErrorMessage = "First name is required")]
        //[MinLength(3, ErrorMessage = "Name length should be more than 3 characters.")]
        //[MaxLength(20, ErrorMessage = "Name length should be less than 20 characters.")]
        //public string InputFirstName { get; set; }

        //[Required(ErrorMessage = "Last name is required")]
        //[MinLength(3, ErrorMessage = "Name length should be more than 3 characters.")]
        //[MaxLength(20, ErrorMessage = "Name length should be less than 20 characters.")]
        //public string InputLastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please input valid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password length should be at least than 6 characters.")]
        public string Password { get; set; }

        //[Required(ErrorMessage = "Confirm password is required")]
        //[Compare("InputPassword", ErrorMessage = "Password and Confirm Password do not match.")]
        //public string ConfirmPassword { get; set; }
    }
}
