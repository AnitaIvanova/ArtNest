using System.Collections.Generic;
using System.Linq;
using ARTNEST.DAL;
using ARTNEST.Models;

namespace ArtNest.Tests.Mocks
{
    public class MockWishlistRepository : IWishlistRepository
    {
        private readonly List<(int UserId, Artwork Artwork)> _items
            = new List<(int, Artwork)>();

        public bool IsAlreadyInWishlist(int userId, int artworkId)
        {
            return _items.Any(i => i.UserId == userId && i.Artwork.Id == artworkId);
        }

        public void SaveToWishlist(int userId, int artworkId)
        {
            if (IsAlreadyInWishlist(userId, artworkId))
                return; // never insert a duplicate

            _items.Add((userId, new Artwork { Id = artworkId }));
        }

        public void RemoveFromWishlist(int userId, int artworkId)
        {
            _items.RemoveAll(i => i.UserId == userId && i.Artwork.Id == artworkId);
        }

        public List<Artwork> GetWishlistByUserId(int userId)
        {
            return _items
                .Where(i => i.UserId == userId)
                .Select(i => i.Artwork)
                .ToList();
        }
    }
}