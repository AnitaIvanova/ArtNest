using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ARTNEST.BLL;
using ARTNEST.Models;

namespace ARTNEST.Pages
{
    public class JournalModel : PageModel
    {
        private readonly JournalService _journalService;
        private readonly ArtworkService _artworkService;

        public List<JournalEntry> Entries { get; set; } = new();
        public List<Artwork> Artworks { get; set; } = new();
        public string Message { get; set; } = "";
        public bool MessageIsSuccess { get; set; } = false;

        [BindProperty] public int NewArtworkId { get; set; }
        [BindProperty] public string NewReflection { get; set; } = "";
        [BindProperty] public int EditEntryId { get; set; }
        [BindProperty] public string EditReflection { get; set; } = "";

        public JournalModel(JournalService journalService, ArtworkService artworkService)
        {
            _journalService = journalService;
            _artworkService = artworkService;
        }

        public IActionResult OnGet()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue) return RedirectToPage("/Login");
            LoadData(userId.Value);
            return Page();
        }

        public IActionResult OnPostAdd()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue) return RedirectToPage("/Login");

            string? error = _journalService.AddEntry(userId.Value, NewArtworkId, NewReflection);
            if (error != null)
            {
                Message = error;
                LoadData(userId.Value);
                return Page();
            }

            TempData["SuccessMessage"] = "Journal entry saved!";
            return RedirectToPage();
        }

        public IActionResult OnPostEdit()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue) return RedirectToPage("/Login");

            string? error = _journalService.UpdateEntry(EditEntryId, userId.Value, EditReflection);
            if (error != null)
            {
                Message = error;
                LoadData(userId.Value);
                return Page();
            }

            TempData["SuccessMessage"] = "Entry updated successfully.";
            return RedirectToPage();
        }

        public IActionResult OnPostDelete(int entryId)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue) return RedirectToPage("/Login");
            _journalService.DeleteEntry(entryId, userId.Value);
            TempData["SuccessMessage"] = "Entry deleted.";
            return RedirectToPage();
        }

        private void LoadData(int userId)
        {
            Entries = _journalService.GetEntries(userId);
            Artworks = _artworkService.GetAllArtworks();
            if (TempData["SuccessMessage"] is string msg)
            {
                Message = msg;
                MessageIsSuccess = true;
            }
        }
    }
}
