using ARTNEST.BLL;
using ArtNest.Tests.Mocks;
using Xunit;

namespace ArtNest.Tests
{
    public class VisitedServiceTests
    {
        private VisitedService CreateService()
        {
            
            var artworkRepo = new MockArtworkRepository();
            var visitedRepo = new MockVisitedRepository(artworkRepo);
            return new VisitedService(visitedRepo);
        }

        [Fact]
        public void IsVisited_ReturnsFalse_WhenNotMarked()
        {
            var service = CreateService();

            Assert.False(service.IsVisited(1, 1));
        }

        [Fact]
        public void ToggleVisited_FirstCall_MarksAndReturnsTrue()
        {
            var service = CreateService();

            bool nowVisited = service.ToggleVisited(1, 1);

            Assert.True(nowVisited);               
            Assert.True(service.IsVisited(1, 1));  
        }

        [Fact]
        public void ToggleVisited_SecondCall_UnmarksAndReturnsFalse()
        {
            var service = CreateService();
            service.ToggleVisited(1, 1); 

            bool nowVisited = service.ToggleVisited(1, 1); 

            Assert.False(nowVisited);
            Assert.False(service.IsVisited(1, 1));
        }

        [Fact]
        public void ToggleVisited_DoesNotCreateDuplicates()
        {
            var service = CreateService();

            service.ToggleVisited(1, 1); 
            service.ToggleVisited(1, 2);

            Assert.Equal(2, service.GetVisitedCount(1));
        }

        [Fact]
        public void GetVisitedCount_IsPerUser()
        {
            var service = CreateService();

            service.ToggleVisited(1, 1);
            service.ToggleVisited(1, 2);
            service.ToggleVisited(2, 1);

            Assert.Equal(2, service.GetVisitedCount(1));
            Assert.Equal(1, service.GetVisitedCount(2));
        }

        [Fact]
        public void GetVisitedArtworks_ReturnsFullArtworkObjects()
        {
            var service = CreateService();
            service.ToggleVisited(1, 1); 

            var artworks = service.GetVisitedArtworks(1);

            Assert.Single(artworks);
            Assert.Equal("Starry Night", artworks[0].Title);
        }

        [Fact]
        public void GetVisitedIds_ReturnsSetOfArtworkIds()
        {
            var service = CreateService();
            service.ToggleVisited(1, 1);
            service.ToggleVisited(1, 3);

            var ids = service.GetVisitedIds(1);

            Assert.Equal(2, ids.Count);
            Assert.Contains(1, ids);
            Assert.Contains(3, ids);
        }

        [Fact]
        public void GetVisitedArtworks_DoesNotReturnAnotherUsersVisits()
        {
            var service = CreateService();
            service.ToggleVisited(1, 1);

            Assert.Empty(service.GetVisitedArtworks(2));
        }
    }
}