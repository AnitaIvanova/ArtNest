using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ARTNEST.BLL;
using ARTNEST.Models;

namespace ARTNEST.Pages
{
    
    public class ArtworkDetailModel : PageModel
    {
        private readonly ArtworkService _artworkService;
        private readonly WishlistService _wishlistService;
        private readonly VisitedService _visitedService;

        public Artwork? Artwork { get; set; }
        public bool IsVisited { get; set; }
        public string Message { get; set; } = "";

        public ArtworkDetailModel(ArtworkService artworkService,
                                   WishlistService wishlistService,
                                   VisitedService visitedService)
        {
            _artworkService = artworkService;
            _wishlistService = wishlistService;
            _visitedService = visitedService;
        }

        public void OnGet(int id)
        {
            Artwork = _artworkService.GetArtworkById(id);
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId.HasValue && Artwork != null)
                IsVisited = _visitedService.IsVisited(userId.Value, Artwork.Id);
        }

        public IActionResult OnPostSave(int artworkId)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue) return RedirectToPage("/Login");

            _wishlistService.SaveArtwork(userId.Value, artworkId);
            Artwork = _artworkService.GetArtworkById(artworkId);
            IsVisited = _visitedService.IsVisited(userId.Value, artworkId);
            Message = "Saved to your wishlist!";
            return Page();
        }

        public IActionResult OnPostToggleVisited(int artworkId)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue) return RedirectToPage("/Login");

            bool nowVisited = _visitedService.ToggleVisited(userId.Value, artworkId);
            Artwork = _artworkService.GetArtworkById(artworkId);
            IsVisited = nowVisited;
            Message = nowVisited ? "Marked as visited!" : "Marked as unvisited.";
            return Page();
        }
    }
}
