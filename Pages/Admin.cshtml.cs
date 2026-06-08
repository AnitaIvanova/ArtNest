using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ARTNEST.BLL;
using ARTNEST.Models;

namespace ARTNEST.Pages
{
    public class AdminModel : PageModel
    {
        private readonly ArtworkService _artworkService;

        public AdminModel(ArtworkService artworkService)
        {
            _artworkService = artworkService;
        }

        [BindProperty] public string Title { get; set; } = "";
        [BindProperty] public string Artist { get; set; } = "";
        [BindProperty] public string Museum { get; set; } = "";
        [BindProperty] public string ImageUrl { get; set; } = "";
        [BindProperty] public string Description { get; set; } = "";
        [BindProperty] public int Year { get; set; }

        public string Message { get; set; } = "";
        public bool MessageIsSuccess { get; set; } = false;

        public IActionResult OnGet()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue) return RedirectToPage("/Login");
            if (HttpContext.Session.GetInt32("IsAdmin") != 1) return RedirectToPage("/Index");

            return Page();
        }

        public IActionResult OnPost()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue) return RedirectToPage("/Login");
            if (HttpContext.Session.GetInt32("IsAdmin") != 1) return RedirectToPage("/Index");

            if (string.IsNullOrWhiteSpace(Title) || string.IsNullOrWhiteSpace(Artist))
            {
                Message = "Title and Artist are required.";
                return Page();
            }

            var artwork = new Artwork
            {
                Title = Title,
                Artist = Artist,
                Museum = Museum,
                ImageUrl = ImageUrl,
                Description = Description,
                Year = Year
            };

            _artworkService.CreateArtwork(artwork);

            Message = "Artwork added successfully.";
            MessageIsSuccess = true;

            Title = Artist = Museum = ImageUrl = Description = "";
            Year = 0;

            return Page();
        }
    }
}