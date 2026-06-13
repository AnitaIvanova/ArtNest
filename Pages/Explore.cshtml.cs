using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ARTNEST.BLL;
using ARTNEST.Models;

namespace ARTNEST.Pages
{
    public class ExploreModel : PageModel
    {
        private readonly ArtworkService _artworkService;
        private readonly WishlistService _wishlistService;
        private readonly VisitedService _visitedService;

        public List<Artwork> Artworks { get; set; } = new();
        public List<string> AllArtists { get; set; } = new();
        public List<string> AllMuseums { get; set; } = new();
        public HashSet<int> VisitedIds { get; set; } = new();

        [BindProperty(SupportsGet = true)] public string? SearchQuery { get; set; }
        [BindProperty(SupportsGet = true)] public string? FilterArtist { get; set; }
        [BindProperty(SupportsGet = true)] public string? FilterMuseum { get; set; }
       
        public ExploreModel(ArtworkService artworkService, WishlistService wishlistService,
                            VisitedService visitedService)
        {
            _artworkService = artworkService;
            _wishlistService = wishlistService;
            _visitedService = visitedService;
        }

        public void OnGet() => LoadData();

        public IActionResult OnPostSave(int artworkId)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue) return RedirectToPage("/Login");
            _wishlistService.SaveArtwork(userId.Value, artworkId);
            return RedirectToPage(new { SearchQuery, FilterArtist, FilterMuseum });
        }

        public IActionResult OnPostToggleVisited(int artworkId)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue) return RedirectToPage("/Login");
            _visitedService.ToggleVisited(userId.Value, artworkId);
            return RedirectToPage(new { SearchQuery, FilterArtist, FilterMuseum });
        }

        private void LoadData()
        {
            Artworks = _artworkService.SearchAndFilter(SearchQuery, FilterArtist, FilterMuseum);
            AllArtists = _artworkService.GetAllArtists();
            AllMuseums = _artworkService.GetAllMuseums();

            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId.HasValue)
                VisitedIds = _visitedService.GetVisitedIds(userId.Value);
        }
    }
}
