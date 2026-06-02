using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ARTNEST.BLL;
using ARTNEST.Models;

namespace ARTNEST.Pages
{
    public class LoginModel : PageModel
    {
        private readonly UserService _userService;

        public LoginModel(UserService userService)
        {
            _userService = userService;
        }

        [BindProperty] public string Email { get; set; } = "";
        [BindProperty] public string Password { get; set; } = "";
        public string Message { get; set; } = "";
        public bool MessageIsSuccess { get; set; } = false;

        public void OnGet(string? registered)
        {
            if (registered == "1")
            {
                Message = "Account created successfully! You can now sign in.";
                MessageIsSuccess = true;
            }
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                Message = "Please enter your email and password.";
                return Page();
            }

            User? user = _userService.LoginUser(Email, Password);
            if (user == null)
            {
                Message = "Invalid email or password. Please try again.";
                return Page();
            }

            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("UserName", user.Name);
            HttpContext.Session.SetString("UserEmail", user.Email);
            return RedirectToPage("/Index");
        }
    }
}
