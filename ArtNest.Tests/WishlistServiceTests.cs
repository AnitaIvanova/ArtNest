using ARTNEST.BLL;
using ArtNest.Tests.Mocks;
using Xunit;

namespace ArtNest.Tests
{
    public class WishlistServiceTests
    {
        private WishlistService CreateService()
        {
            var mock = new MockWishlistRepository();
            return new WishlistService(mock);
        }

        [Fact]
        public void SaveArtwork_AddsItemToWishlist()
        {
            var service = CreateService();

            service.SaveArtwork(userId: 1, artworkId: 10);

            Assert.True(service.IsInWishlist(1, 10));
            Assert.Equal(1, service.GetWishlistCount(1));
        }

        [Fact]
        public void SaveArtwork_Twice_DoesNotCreateDuplicate()
        {
            var service = CreateService();

            service.SaveArtwork(1, 10);
            service.SaveArtwork(1, 10);

            Assert.Equal(1, service.GetWishlistCount(1));
        }

        [Fact]
        public void RemoveArtwork_RemovesItem()
        {
            var service = CreateService();
            service.SaveArtwork(1, 10);

            service.RemoveArtwork(1, 10);

            Assert.False(service.IsInWishlist(1, 10));
            Assert.Equal(0, service.GetWishlistCount(1));
        }

        [Fact]
        public void IsInWishlist_ReturnsFalse_WhenNotSaved()
        {
            var service = CreateService();

            Assert.False(service.IsInWishlist(1, 99));
        }

        [Fact]
        public void GetWishlistCount_IsPerUser()
        {
            var service = CreateService();

            service.SaveArtwork(1, 10);
            service.SaveArtwork(1, 11);
            service.SaveArtwork(2, 10);

            Assert.Equal(2, service.GetWishlistCount(1));
            Assert.Equal(1, service.GetWishlistCount(2));
        }
    }
}