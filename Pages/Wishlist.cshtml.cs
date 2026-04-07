using Microsoft.AspNetCore.Mvc.RazorPages;
using ARTNEST.Models;
using ARTNEST.Data;

namespace ARTNEST.Pages
{
    public class WishlistModel : PageModel
    {
        public List<WishlistItem> SavedArtworks { get; set; } = new();

        public void OnGet()
        {
            SavedArtworks = new List<WishlistItem>
            {
                new WishlistItem { Id = 1, ArtworkId = 1, SavedDate = new DateTime(2026, 2, 28), Artwork = ArtworkData.GetById(1) },
                new WishlistItem { Id = 2, ArtworkId = 3, SavedDate = new DateTime(2026, 2, 25), Artwork = ArtworkData.GetById(3) },
                new WishlistItem { Id = 3, ArtworkId = 2, SavedDate = new DateTime(2026, 2, 20), Artwork = ArtworkData.GetById(2) }
            };
        }
    }
}