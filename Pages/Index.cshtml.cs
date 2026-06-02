using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ARTNEST.BLL;
using ARTNEST.Models;

namespace ARTNEST.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ArtworkService _artworkService;
        private readonly WishlistService _wishlistService;

        public List<Artwork> Artworks { get; set; } = new();

        public IndexModel(ArtworkService artworkService, WishlistService wishlistService)
        {
            _artworkService = artworkService;
            _wishlistService = wishlistService;
        }

        public void OnGet()
        {
            Artworks = _artworkService.GetAllArtworks();
        }

        public IActionResult OnPostSave(int artworkId)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToPage("/Login");
            _wishlistService.SaveArtwork(userId.Value, artworkId);
            return RedirectToPage();
        }
    }
}
