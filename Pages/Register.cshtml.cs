using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ARTNEST.BLL;
using System.Text.RegularExpressions;

namespace ARTNEST.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly UserService _userService;

        public RegisterModel(UserService userService)
        {
            _userService = userService;
        }

        [BindProperty] public string FullName { get; set; } = "";
        [BindProperty] public string Email { get; set; } = "";
        [BindProperty] public string Password { get; set; } = "";
        [BindProperty] public string ConfirmPassword { get; set; } = "";
        public string Message { get; set; } = "";

        public void OnGet() { }

        public IActionResult OnPost()
        {
            if (string.IsNullOrWhiteSpace(FullName) || FullName.Trim().Length < 2)
            {
                Message = "Please enter your full name (at least 2 characters).";
                return Page();
            }
            if (string.IsNullOrWhiteSpace(Email) || !Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                Message = "Please enter a valid email address.";
                return Page();
            }
            if (string.IsNullOrWhiteSpace(Password) || Password.Length < 6)
            {
                Message = "Password must be at least 6 characters.";
                return Page();
            }
            if (Password != ConfirmPassword)
            {
                Message = "Passwords do not match.";
                return Page();
            }

            bool success = _userService.RegisterUser(FullName.Trim(), Email.Trim(), Password);
            if (success)
                return RedirectToPage("/Login", new { registered = "1" });

            Message = "An account with that email already exists. Try logging in instead.";
            return Page();
        }
    }
}
