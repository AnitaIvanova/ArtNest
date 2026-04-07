using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ARTNEST.Pages
{
    public class ExploreModel : PageModel
    {
        public List<ExploreArtwork> Artworks { get; set; } = new();

        public void OnGet()
        {
            Artworks = new List<ExploreArtwork>
            {
                new ExploreArtwork
                {
                    Id = 1,
                    Title = "The Grand Canal",
                    Artist = "Claude Monet",
                    Museum = "Museum of Fine Arts, Boston",
                    Year = "1908",
                    Image = "https://images.unsplash.com/photo-1763491905859-d013a8430f21?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&ixid=M3w3Nzg4Nzd8MHwxfHNlYXJjaHwxfHxmYW1vdXMlMjBvaWwlMjBwYWludGluZyUyMGFydHdvcmt8ZW58MXx8fHwxNzcyNDQ4NTkxfDA&ixlib=rb-4.1.0&q=80&w=1080&utm_source=figma&utm_medium=referral"
                },
                new ExploreArtwork
                {
                    Id = 2,
                    Title = "Portrait of a Lady",
                    Artist = "Johannes Vermeer",
                    Museum = "The Louvre, Paris",
                    Year = "1665",
                    Image = "https://images.unsplash.com/photo-1763070605733-e420828395ea?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&ixid=M3w3Nzg4Nzd8MHwxfHNlYXJjaHwxfHxwb3J0cmFpdCUyMHBhaW50aW5nJTIwbXVzZXVtJTIwYXJ0fGVufDF8fHx8MTc3MjQ0ODU5MXww&ixlib=rb-4.1.0&q=80&w=1080&utm_source=figma&utm_medium=referral"
                },
                new ExploreArtwork
                {
                    Id = 3,
                    Title = "Water Lilies",
                    Artist = "Claude Monet",
                    Museum = "Musée de l'Orangerie, Paris",
                    Year = "1919",
                    Image = "https://images.unsplash.com/photo-1699391202798-bec3f1c894bc?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&ixid=M3w3Nzg4Nzd8MHwxfHNlYXJjaHwxfHxpbXByZXNzaW9uaXN0JTIwcGFpbnRpbmclMjBjb2xvcmZ1bHxlbnwxfHx8fDE3NzI0NDg1OTF8MA&ixlib=rb-4.1.0&q=80&w=1080&utm_source=figma&utm_medium=referral"
                },
                new ExploreArtwork
                {
                    Id = 4,
                    Title = "Modern Abstraction",
                    Artist = "Various Artists",
                    Museum = "The Metropolitan Museum of Art",
                    Year = "2024",
                    Image = "https://images.unsplash.com/photo-1703936205356-11814e31bfda?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&ixid=M3w3Nzg4Nzd8MHwxfHNlYXJjaHwxfHxhYnN0cmFjdCUyMG1vZGVybiUyMGFydCUyMHBhaW50aW5nfGVufDF8fHx8MTc3MjM1NTg3OXww&ixlib=rb-4.1.0&q=80&w=1080&utm_source=figma&utm_medium=referral"
                },
                new ExploreArtwork
                {
                    Id = 5,
                    Title = "Serene Landscape",
                    Artist = "Vincent van Gogh",
                    Museum = "Museum of Modern Art, NYC",
                    Year = "1889",
                    Image = "https://images.unsplash.com/photo-1688589935455-7793f9f52a2d?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&ixid=M3w3Nzg4Nzd8MHwxfHNlYXJjaHwxfHxsYW5kc2NhcGUlMjBwYWludGluZyUyMG5hdHVyZSUyMGFydHxlbnwxfHx8fDE3NzI0NDg1OTJ8MA&ixlib=rb-4.1.0&q=80&w=1080&utm_source=figma&utm_medium=referral"
                },
                new ExploreArtwork
                {
                    Id = 6,
                    Title = "Renaissance Masterpiece",
                    Artist = "Gustav Klimt",
                    Museum = "Belvedere Palace, Vienna",
                    Year = "1908",
                    Image = "https://images.unsplash.com/photo-1763792334906-70a24d373140?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&ixid=M3w3Nzg4Nzd8MHwxfHNlYXJjaHwxfHxyZW5haXNzYW5jZSUyMHBhaW50aW5nJTIwbWFzdGVycGllY2V8ZW58MXx8fHwxNzcyNDQyNzAwfDA&ixlib=rb-4.1.0&q=80&w=1080&utm_source=figma&utm_medium=referral"
                }
            };
        }
    }

    public class ExploreArtwork
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Artist { get; set; } = "";
        public string Museum { get; set; } = "";
        public string Year { get; set; } = "";
        public string Image { get; set; } = "";
    }
}