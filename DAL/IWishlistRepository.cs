using ARTNEST.Models;

namespace ARTNEST.DAL
{
   
    public interface IWishlistRepository
    {
        bool IsAlreadyInWishlist(int userId, int artworkId);
        void SaveToWishlist(int userId, int artworkId);
        void RemoveFromWishlist(int userId, int artworkId);
        List<Artwork> GetWishlistByUserId(int userId);
    }
}
