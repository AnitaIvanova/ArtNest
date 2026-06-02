using ARTNEST.DAL;
using ARTNEST.Models;

namespace ARTNEST.BLL
{

    public class WishlistService
    {
        private readonly IWishlistRepository _wishlistRepository;

        public WishlistService(IWishlistRepository wishlistRepository)
        {
            _wishlistRepository = wishlistRepository;
        }

        public List<Artwork> GetWishlist(int userId)
        {
            return _wishlistRepository.GetWishlistByUserId(userId);
        }

        public int GetWishlistCount(int userId)
        {
            return _wishlistRepository.GetWishlistByUserId(userId).Count;
        }

        public void SaveArtwork(int userId, int artworkId)
        {
            _wishlistRepository.SaveToWishlist(userId, artworkId);
        }

        public void RemoveArtwork(int userId, int artworkId)
        {
            _wishlistRepository.RemoveFromWishlist(userId, artworkId);
        }

        public bool IsInWishlist(int userId, int artworkId)
        {
            return _wishlistRepository.IsAlreadyInWishlist(userId, artworkId);
        }
    }
}
