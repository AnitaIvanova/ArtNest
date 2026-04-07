using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ARTNEST.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public string FullName { get; set; } = "";

        [BindProperty]
        public string Email { get; set; } = "";

        [BindProperty]
        public string Password { get; set; } = "";

        [BindProperty]
        public string ConfirmPassword { get; set; } = "";

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            // Registration logic will go here later
            return Page();
        }
    }
}