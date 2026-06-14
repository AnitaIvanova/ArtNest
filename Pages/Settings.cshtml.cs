using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ARTNEST.BLL;

namespace ARTNEST.Pages
{
    public class SettingsModel : PageModel
    {
        private readonly UserService _userService;
        private readonly WishlistService _wishlistService;
        private readonly JournalService _journalService;
        private readonly VisitedService _visitedService;

        public SettingsModel(UserService userService, WishlistService wishlistService,
                             JournalService journalService, VisitedService visitedService)
        {
            _userService = userService;
            _wishlistService = wishlistService;
            _journalService = journalService;
            _visitedService = visitedService;
        }

        [BindProperty] public string Name { get; set; } = "";
        [BindProperty] public string Email { get; set; } = "";
        [BindProperty] public string NewPassword { get; set; } = "";
        [BindProperty] public string ConfirmPassword { get; set; } = "";

        public int SavedArtworksCount { get; set; }
        public int VisitedArtworksCount { get; set; }
        public string Message { get; set; } = "";
        public bool MessageIsSuccess { get; set; } = false;

        public IActionResult OnGet()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue) return RedirectToPage("/Login");
            var user = _userService.GetUserById(userId.Value);
            if (user == null) return RedirectToPage("/Login");
            LoadPageData(userId.Value);
            return Page();
        }

        public IActionResult OnPost()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue) return RedirectToPage("/Login");

            if (string.IsNullOrWhiteSpace(Name))
            {
                Message = "Name cannot be empty.";
                LoadPageData(userId.Value);
                return Page();
            }
            if (string.IsNullOrWhiteSpace(Email) || !Email.Contains('@'))
            {
                Message = "Please enter a valid email address.";
                LoadPageData(userId.Value);
                return Page();
            }
            if (!string.IsNullOrWhiteSpace(NewPassword))
            {
                if (NewPassword.Length < 6)
                {
                    Message = "New password must be at least 6 characters.";
                    LoadPageData(userId.Value);
                    return Page();
                }
                if (NewPassword != ConfirmPassword)
                {
                    Message = "Passwords do not match.";
                    LoadPageData(userId.Value);
                    return Page();
                }
            }

            string? passwordToSave = string.IsNullOrWhiteSpace(NewPassword) ? null : NewPassword;
            bool success = _userService.UpdateUserSettings(userId.Value, Name, Email, passwordToSave);

            if (!success)
            {
                Message = "Could not update settings. That email may already be in use.";
                LoadPageData(userId.Value);
                return Page();
            }

            HttpContext.Session.SetString("UserName", Name);
            HttpContext.Session.SetString("UserEmail", Email);
            Message = "Settings updated successfully.";
            MessageIsSuccess = true;
            LoadPageData(userId.Value);
            return Page();
        }

        private void LoadPageData(int userId)
        {
            var user = _userService.GetUserById(userId);
            if (user != null)
            {
                Name = user.Name;
                Email = user.Email;
            }
            SavedArtworksCount = _wishlistService.GetWishlistCount(userId);
            VisitedArtworksCount = _visitedService.GetVisitedCount(userId);
        }
    }
}
