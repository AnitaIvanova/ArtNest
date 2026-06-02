using ARTNEST.Models;

namespace ARTNEST.DAL.Mocks
{
    
    public class MockWishlistRepository : IWishlistRepository
    {
        private readonly List<(int UserId, int ArtworkId)> _items = new();
        private readonly IArtworkRepository _artworkRepository;

        public MockWishlistRepository(IArtworkRepository artworkRepository)
        {
            _artworkRepository = artworkRepository;
        }

        public bool IsAlreadyInWishlist(int userId, int artworkId) =>
            _items.Any(i => i.UserId == userId && i.ArtworkId == artworkId);

        public void SaveToWishlist(int userId, int artworkId)
        {
            if (!IsAlreadyInWishlist(userId, artworkId))
                _items.Add((userId, artworkId));
        }

        public void RemoveFromWishlist(int userId, int artworkId) =>
            _items.RemoveAll(i => i.UserId == userId && i.ArtworkId == artworkId);

        public List<Artwork> GetWishlistByUserId(int userId)
        {
            return _items
                .Where(i => i.UserId == userId)
                .Select(i => _artworkRepository.GetById(i.ArtworkId))
                .Where(a => a != null)
                .Cast<Artwork>()
                .ToList();
        }
    }
}
