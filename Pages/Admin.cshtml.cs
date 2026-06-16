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

        [BindProperty] public int EditId { get; set; }          // 0 = adding, >0 = editing
        [BindProperty] public string Title { get; set; } = "";
        [BindProperty] public string Artist { get; set; } = "";
        [BindProperty] public string Museum { get; set; } = "";
        [BindProperty] public string ImageUrl { get; set; } = "";
        [BindProperty] public string Description { get; set; } = "";
        [BindProperty] public int Year { get; set; }

        public string Message { get; set; } = "";
        public bool MessageIsSuccess { get; set; } = false;

        private bool IsAdmin() => HttpContext.Session.GetInt32("IsAdmin") == 1;

        public IActionResult OnGet()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue) return RedirectToPage("/Login");
            if (!IsAdmin()) return RedirectToPage("/Index");
            return Page();
        }

        // Loads an artwork into the form for editing (called from the Explore Edit link)
        public IActionResult OnGetEdit(int id)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue) return RedirectToPage("/Login");
            if (!IsAdmin()) return RedirectToPage("/Index");

            var artwork = _artworkService.GetArtworkById(id);
            if (artwork != null)
            {
                EditId = artwork.Id;
                Title = artwork.Title;
                Artist = artwork.Artist;
                Museum = artwork.Museum;
                ImageUrl = artwork.ImageUrl;
                Description = artwork.Description;
                Year = artwork.Year;
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue) return RedirectToPage("/Login");
            if (!IsAdmin()) return RedirectToPage("/Index");

            if (string.IsNullOrWhiteSpace(Title) || string.IsNullOrWhiteSpace(Artist))
            {
                Message = "Title and Artist are required.";
                return Page();
            }

            var artwork = new Artwork
            {
                Id = EditId,
                Title = Title,
                Artist = Artist,
                Museum = Museum,
                ImageUrl = ImageUrl,
                Description = Description,
                Year = Year
            };

            if (EditId > 0)
            {
                _artworkService.UpdateArtwork(artwork);
                Message = "Artwork updated successfully.";
            }
            else
            {
                _artworkService.CreateArtwork(artwork);
                Message = "Artwork added successfully.";
            }

            MessageIsSuccess = true;
            EditId = 0;
            Title = Artist = Museum = ImageUrl = Description = "";
            Year = 0;
            return Page();
        }
    }
}