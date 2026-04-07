using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ARTNEST.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Email { get; set; } = "";

        [BindProperty]
        public string Password { get; set; } = "";

        [BindProperty]
        public bool RememberMe { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            // Authentication logic will go here later
            return Page();
        }
    }
}