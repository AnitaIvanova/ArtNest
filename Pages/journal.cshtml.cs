using Microsoft.AspNetCore.Mvc.RazorPages;
using ARTNEST.Models;
using ARTNEST.Data;

namespace ARTNEST.Pages
{
    public class JournalModel : PageModel
    {
        public List<JournalEntry> Entries { get; set; } = new();

        public void OnGet()
        {
            Entries = new List<JournalEntry>
            {
                new JournalEntry
                {
                    Id = 1,
                    ArtworkId = 1,
                    Reflection = "Standing in front of this painting, I was struck by how Monet captured the light dancing on the water. The brushstrokes are so alive.",
                    Date = new DateTime(2026, 3, 1),
                    Artwork = ArtworkData.GetById(1)
                },
                new JournalEntry
                {
                    Id = 2,
                    ArtworkId = 2,
                    Reflection = "The way Vermeer captures light is magical. There's such intimacy in this portrait.",
                    Date = new DateTime(2026, 2, 15),
                    Artwork = ArtworkData.GetById(2)
                }
            };
        }
    }
}