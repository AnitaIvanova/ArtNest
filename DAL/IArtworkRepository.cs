using ARTNEST.Models;

namespace ARTNEST.DAL
{
 
    public interface IArtworkRepository
    {
        List<Artwork> GetAllArtworks();
        List<Artwork> SearchArtworks(string? searchQuery, string? filterArtist, string? filterMuseum, int? filterYear);
        List<string> GetDistinctArtists();
        List<string> GetDistinctMuseums();
        Artwork? GetById(int id);
    }
}
