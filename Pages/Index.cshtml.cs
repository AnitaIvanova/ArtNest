using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ARTNEST.Models;
using ARTNEST.Repositories;

namespace ARTNEST.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ArtworkRepository _artworkRepository;
        private readonly WishlistRepository _wishlistRepository;

        public List<Artwork> Artworks { get; set; } = new List<Artwork>();

        public IndexModel(ArtworkRepository artworkRepository, WishlistRepository wishlistRepository)
        {
            _artworkRepository = artworkRepository;
            _wishlistRepository = wishlistRepository;
        }

        public void OnGet()
        {
            Artworks = _artworkRepository.GetAllArtworks();
        }

        public IActionResult OnPostSave(int artworkId)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToPage("/Login");
            }

            _wishlistRepository.SaveToWishlist(userId.Value, artworkId);
            return RedirectToPage();
        }
    }
}