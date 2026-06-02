using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ARTNEST.BLL;
using ARTNEST.Models;

namespace ARTNEST.Pages
{
    public class WishlistModel : PageModel
    {
        private readonly WishlistService _wishlistService;
        private readonly VisitedService _visitedService;

        public List<Artwork> SavedArtworks { get; set; } = new();
        public HashSet<int> VisitedIds { get; set; } = new();
        public string Message { get; set; } = "";

        public WishlistModel(WishlistService wishlistService, VisitedService visitedService)
        {
            _wishlistService = wishlistService;
            _visitedService = visitedService;
        }

        public IActionResult OnGet()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue) return RedirectToPage("/Login");

            SavedArtworks = _wishlistService.GetWishlist(userId.Value);
            VisitedIds = _visitedService.GetVisitedIds(userId.Value);

            if (TempData["WishlistMessage"] is string msg)
                Message = msg;

            return Page();
        }

        public IActionResult OnPostRemove(int artworkId)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue) return RedirectToPage("/Login");

            _wishlistService.RemoveArtwork(userId.Value, artworkId);
            TempData["WishlistMessage"] = "Artwork removed from wishlist.";
            return RedirectToPage();
        }
    }
}
