using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ARTNEST.Pages
{
    public class IndexModel : PageModel
    {
        public List<Artwork> Artworks { get; set; } = new();

        public void OnGet()
        {
            Artworks = new List<Artwork>
            {
                new Artwork
                {
                    Id = 1,
                    Title = "The Grand Canal",
                    Artist = "Claude Monet",
                    Museum = "Museum of Fine Arts, Boston",
                    Image = "https://images.unsplash.com/photo-1763491905859-d013a8430f21?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&ixid=M3w3Nzg4Nzd8MHwxfHNlYXJjaHwxfHxmYW1vdXMlMjBvaWwlMjBwYWludGluZyUyMGFydHdvcmt8ZW58MXx8fHwxNzcyNDQ4NTkxfDA&ixlib=rb-4.1.0&q=80&w=1080&utm_source=figma&utm_medium=referral"
                },
                new Artwork
                {
                    Id = 2,
                    Title = "Portrait of a Lady",
                    Artist = "Johannes Vermeer",
                    Museum = "The Louvre, Paris",
                    Image = "https://images.unsplash.com/photo-1763070605733-e420828395ea?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&ixid=M3w3Nzg4Nzd8MHwxfHNlYXJjaHwxfHxwb3J0cmFpdCUyMHBhaW50aW5nJTIwbXVzZXVtJTIwYXJ0fGVufDF8fHx8MTc3MjQ0ODU5MXww&ixlib=rb-4.1.0&q=80&w=1080&utm_source=figma&utm_medium=referral"
                },
                new Artwork
                {
                    Id = 3,
                    Title = "Water Lilies",
                    Artist = "Claude Monet",
                    Museum = "Musée de l'Orangerie, Paris",
                    Image = "https://images.unsplash.com/photo-1699391202798-bec3f1c894bc?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&ixid=M3w3Nzg4Nzd8MHwxfHNlYXJjaHwxfHxpbXByZXNzaW9uaXN0JTIwcGFpbnRpbmclMjBjb2xvcmZ1bHxlbnwxfHx8fDE3NzI0NDg1OTF8MA&ixlib=rb-4.1.0&q=80&w=1080&utm_source=figma&utm_medium=referral"
                },
                new Artwork
                {
                    Id = 4,
                    Title = "Classical Gallery",
                    Artist = "Various Artists",
                    Museum = "The Metropolitan Museum of Art",
                    Image = "https://images.unsplash.com/photo-1770713522187-d9c16e16a15b?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&ixid=M3w3Nzg4Nzd8MHwxfHNlYXJjaHwxfHxjbGFzc2ljYWwlMjBhcnQlMjBzY3VscHR1cmUlMjBtdXNldW18ZW58MXx8fHwxNzcyNDQ4NTkzfDA&ixlib=rb-4.1.0&q=80&w=1080&utm_source=figma&utm_medium=referral"
                }
            };
        }
    }

    public class Artwork
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Artist { get; set; } = "";
        public string Museum { get; set; } = "";
        public string Image { get; set; } = "";
    }
}