using ARTNEST.BLL;
using ArtNest.Tests.Mocks;
using Xunit;

namespace ArtNest.Tests
{
    public class ArtworkServiceTests
    {
        private ArtworkService CreateService()
        {
            var mock = new MockArtworkRepository();
            return new ArtworkService(mock);
        }

        [Fact]
        public void GetAllArtworks_ReturnsAllSeededArtworks()
        {
            var service = CreateService();

            var all = service.GetAllArtworks();

            Assert.Equal(3, all.Count);
        }

        [Fact]
        public void GetArtworkById_WithValidId_ReturnsArtwork()
        {
            var service = CreateService();

            var artwork = service.GetArtworkById(1);

            Assert.NotNull(artwork);
            Assert.Equal("Starry Night", artwork!.Title);
        }

        [Fact]
        public void GetArtworkById_WithUnknownId_ReturnsNull()
        {
            var service = CreateService();

            var artwork = service.GetArtworkById(999);

            Assert.Null(artwork);
        }

        [Fact]
        public void SearchAndFilter_ByQuery_ReturnsMatchingArtworks()
        {
            var service = CreateService();

            var results = service.SearchAndFilter("van gogh", null, null);

            Assert.Single(results);
            Assert.Equal("Starry Night", results[0].Title);
        }

        [Fact]
        public void SearchAndFilter_ByMuseum_ReturnsOnlyThatMuseum()
        {
            var service = CreateService();

            var results = service.SearchAndFilter(null, null, "MoMA");

            Assert.Equal(2, results.Count);
            Assert.All(results, a => Assert.Equal("MoMA", a.Museum));
        }

        [Fact]
        public void SearchAndFilter_NoCriteria_ReturnsAll()
        {
            var service = CreateService();

            var results = service.SearchAndFilter(null, null, null);

            Assert.Equal(3, results.Count);
        }

        [Fact]
        public void SearchAndFilter_ByArtist_ReturnsOnlyThatArtist()
        {
            var service = CreateService();

            var results = service.SearchAndFilter(null, "Salvador Dalí", null);

            Assert.Single(results);
            Assert.Equal("The Persistence of Memory", results[0].Title);
        }

        [Fact]
        public void GetAllArtists_ReturnsDistinctSortedArtists()
        {
            var service = CreateService();

            var artists = service.GetAllArtists();

            Assert.Equal(3, artists.Count);
            Assert.Equal("Johannes Vermeer", artists[0]);
        }

        [Fact]
        public void GetAllMuseums_ReturnsDistinctMuseums()
        {
            var service = CreateService();

            var museums = service.GetAllMuseums();

            Assert.Equal(2, museums.Count);
        }
    }
}
