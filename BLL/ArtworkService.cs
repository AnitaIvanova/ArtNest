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

        
      
        public List<Artwork> SearchAndFilter(string? query, string? artist, string? museum )
        {
             return _artworkRepository.SearchArtworks(query, artist, museum, null);
}

        public List<string> GetAllArtists()
        {
            return _artworkRepository.GetDistinctArtists();
        }

        public List<string> GetAllMuseums()
        {
            return _artworkRepository.GetDistinctMuseums();
        }
        public void UpdateArtwork(Artwork artwork)
{
       _artworkRepository.Update(artwork);
}

    }
}
