using ARTNEST.Models;
using ARTNEST.DAL;

namespace ArtNest.Tests.Mocks
{
     public class MockArtworkRepository : IArtworkRepository
    {
        private readonly List<Artwork> _artworks;

        public MockArtworkRepository()
        {
            _artworks = new List<Artwork>
            {
                new Artwork { Id = 1, Title = "Starry Night",      Artist = "Vincent van Gogh", Museum = "MoMA",               ImageUrl = "/img/sample/starry.jpg",  Description = "A swirling night sky over a quiet village.", Year = 1889 },
                new Artwork { Id = 2, Title = "The Persistence of Memory", Artist = "Salvador Dalí", Museum = "MoMA",        ImageUrl = "/img/sample/dali.jpg",    Description = "Melting clocks in a dreamlike landscape.",   Year = 1931 },
                new Artwork { Id = 3, Title = "Girl with a Pearl Earring", Artist = "Johannes Vermeer", Museum = "Mauritshuis", ImageUrl = "/img/sample/pearl.jpg",  Description = "A quiet portrait of an unknown young woman.",Year = 1665 }
            };
        }

        public List<Artwork> GetAllArtworks() => _artworks.ToList();

        public Artwork? GetById(int id) => _artworks.FirstOrDefault(a => a.Id == id);

        public List<Artwork> SearchArtworks(string? searchQuery, string? filterArtist, string? filterMuseum, int? filterYear)
        {
            IEnumerable<Artwork> results = _artworks;

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                string q = searchQuery.ToLower();
                results = results.Where(a =>
                    a.Title.ToLower().Contains(q) ||
                    a.Artist.ToLower().Contains(q) ||
                    a.Museum.ToLower().Contains(q));
            }

            if (!string.IsNullOrWhiteSpace(filterArtist))
                results = results.Where(a => a.Artist == filterArtist);

            if (!string.IsNullOrWhiteSpace(filterMuseum))
                results = results.Where(a => a.Museum == filterMuseum);

            if (filterYear.HasValue)
                results = results.Where(a => a.Year == filterYear.Value);

            return results.ToList();
        }

        public List<string> GetDistinctArtists() =>
            _artworks.Select(a => a.Artist).Distinct().OrderBy(x => x).ToList();

        public List<string> GetDistinctMuseums() =>
            _artworks.Select(a => a.Museum).Distinct().OrderBy(x => x).ToList();
    public void Insert(Artwork artwork)
        {
            int nextId = _artworks.Count == 0 ? 1 : _artworks.Max(a => a.Id) + 1;
            artwork.Id = nextId;
            _artworks.Add(artwork);
        }
        public void Update(Artwork artwork)
{
    var existing = _artworks.FirstOrDefault(a => a.Id == artwork.Id);
    if (existing != null)
    {
        existing.Title = artwork.Title;
        existing.Artist = artwork.Artist;
        existing.Museum = artwork.Museum;
        existing.ImageUrl = artwork.ImageUrl;
        existing.Description = artwork.Description;
        existing.Year = artwork.Year;
    }
}
    }
}
