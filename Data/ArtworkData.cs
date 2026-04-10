using ARTNEST.Models;

namespace ARTNEST.Data
{
    public static class ArtworkData
    {
        public static List<Artwork> GetAll()
        {
            return new List<Artwork>
            {
                new Artwork
                {
                    Id = 1,
                    Title = "The Grand Canal",
                    Artist = "Claude Monet",
                    Museum = "Museum of Fine Arts, Boston",
                    Year = 1908,
                    Description = "A stunning impressionist view of Venice's Grand Canal, capturing the shimmering light on the water.",
                    ImageUrl = "https://images.unsplash.com/photo-1763491905859-d013a8430f21?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&ixlib=rb-4.1.0&q=80&w=1080"
                },
                new Artwork
                {
                    Id = 2,
                    Title = "Portrait of a Lady",
                    Artist = "Johannes Vermeer",
                    Museum = "The Louvre, Paris",
                    Year = 1665,
                    Description = "An intimate and luminous portrait demonstrating Vermeer's mastery of light and texture.",
                    ImageUrl = "https://images.unsplash.com/photo-1763070605733-e420828395ea?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&ixlib=rb-4.1.0&q=80&w=1080"
                },
                new Artwork
                {
                    Id = 3,
                    Title = "Water Lilies",
                    Artist = "Claude Monet",
                    Year = 1919,
                    Description = "Part of Monet's famous series painted at his garden in Giverny, a meditative study of nature.",
                    ImageUrl = "https://images.unsplash.com/photo-1699391202798-bec3f1c894bc?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&ixlib=rb-4.1.0&q=80&w=1080"
                },
                new Artwork
                {
                    Id = 4,
                    Title = "Modern Abstraction",
                    Artist = "Various Artists",
                    Museum = "The Metropolitan Museum of Art",
                    Year = 2024,
                    Description = "A contemporary exploration of form, color, and movement in abstract expression.",
                    ImageUrl = "https://images.unsplash.com/photo-1703936205356-11814e31bfda?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&ixlib=rb-4.1.0&q=80&w=1080"
                },
                new Artwork
                {
                    Id = 5,
                    Title = "Serene Landscape",
                    Artist = "Vincent van Gogh",
                    Museum = "Museum of Modern Art, NYC",
                    Year = 1889,
                    Description = "A breathtaking landscape showcasing van Gogh's expressive brushwork and emotional depth.",
                    ImageUrl = "https://images.unsplash.com/photo-1688589935455-7793f9f52a2d?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&ixlib=rb-4.1.0&q=80&w=1080"
                },
                new Artwork
                {
                    Id = 6,
                    Title = "Renaissance Masterpiece",
                    Artist = "Gustav Klimt",
                    Museum = "Belvedere Palace, Vienna",
                    Year = 1908,
                    Description = "A richly decorative work blending symbolism and gold leaf in Klimt's distinctive style.",
                    ImageUrl = "https://images.unsplash.com/photo-1763792334906-70a24d373140?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&ixlib=rb-4.1.0&q=80&w=1080"
                }
            };
        }

        public static Artwork? GetById(int id)
        {
            return GetAll().FirstOrDefault(a => a.Id == id);
        }
    }
}