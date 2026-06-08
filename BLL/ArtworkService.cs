using ARTNEST.DAL;
using ARTNEST.Models;

namespace ARTNEST.BLL
{
   
    public class ArtworkService
    {
        private readonly IArtworkRepository _artworkRepository;

        public ArtworkService(IArtworkRepository artworkRepository)
        {
            _artworkRepository = artworkRepository;
        }

        public List<Artwork> GetAllArtworks()
        {
            return _artworkRepository.GetAllArtworks();
        }

        public Artwork? GetArtworkById(int id)
        {
            return _artworkRepository.GetById(id);
        }
        
        public void CreateArtwork(Artwork artwork)
        {
            _artworkRepository.Insert(artwork);
        }
      
        public List<Artwork> SearchAndFilter(string? query, string? artist, string? museum, string? sortBy)
        {
            var artworks = _artworkRepository.SearchArtworks(query, artist, museum, null);

            return sortBy switch
            {
                "year_asc"  => artworks.OrderBy(a => a.Year).ToList(),
                "year_desc" => artworks.OrderByDescending(a => a.Year).ToList(),
                "artist"    => artworks.OrderBy(a => a.Artist).ToList(),
                _           => artworks.OrderBy(a => a.Title).ToList()
            };
        }

        public List<string> GetAllArtists()
        {
            return _artworkRepository.GetDistinctArtists();
        }

        public List<string> GetAllMuseums()
        {
            return _artworkRepository.GetDistinctMuseums();
        }
    }
}
