using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ARTNEST.Models;
using ARTNEST.Repositories;

namespace ARTNEST.Pages
{
    public class WishlistModel : PageModel
    {
        private readonly WishlistRepository _wishlistRepository;

        public List<Artwork> SavedArtworks { get; set; } = new List<Artwork>();

        public WishlistModel(WishlistRepository wishlistRepository)
        {
            _wishlistRepository = wishlistRepository;
        }

        public IActionResult OnGet()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToPage("/Login");
            }

            SavedArtworks = _wishlistRepository.GetWishlistByUserId(userId.Value);
            return Page();
        }

        public IActionResult OnPostRemove(int artworkId)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToPage("/Login");
            }

            _wishlistRepository.RemoveFromWishlist(userId.Value, artworkId);
            return RedirectToPage();
        }
    }
}