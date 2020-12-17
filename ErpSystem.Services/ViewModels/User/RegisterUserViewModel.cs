namespace ErpSystem.Services.ViewModels.User
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterUserViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please input valid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password length should be at least than 6 characters.")]
        public string Password { get; set; }
    }
}
