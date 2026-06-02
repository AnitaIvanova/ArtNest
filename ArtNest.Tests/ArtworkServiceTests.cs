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

            var results = service.SearchAndFilter("van gogh", null, null, null);

            Assert.Single(results);
            Assert.Equal("Starry Night", results[0].Title);
        }

        [Fact]
        public void SearchAndFilter_ByMuseum_ReturnsOnlyThatMuseum()
        {
            var service = CreateService();

            var results = service.SearchAndFilter(null, null, "MoMA", null);

            Assert.Equal(2, results.Count);
            Assert.All(results, a => Assert.Equal("MoMA", a.Museum));
        }

        [Fact]
        public void SearchAndFilter_NoCriteria_ReturnsAll()
        {
            var service = CreateService();

            var results = service.SearchAndFilter(null, null, null, null);

            Assert.Equal(3, results.Count);
        }

        [Fact]
        public void SearchAndFilter_SortByYearAscending_OrdersOldestFirst()
        {
            var service = CreateService();

            var results = service.SearchAndFilter(null, null, null, "year_asc");

            Assert.Equal(1665, results[0].Year);
            Assert.Equal(1931, results[2].Year);
        }

        [Fact]
        public void SearchAndFilter_SortByYearDescending_OrdersNewestFirst()
        {
            var service = CreateService();

            var results = service.SearchAndFilter(null, null, null, "year_desc");

            Assert.Equal(1931, results[0].Year);
            Assert.Equal(1665, results[2].Year);
        }

        [Fact]
        public void SearchAndFilter_DefaultSort_OrdersByTitle()
        {
            var service = CreateService();

            var results = service.SearchAndFilter(null, null, null, null);

            Assert.Equal("Girl with a Pearl Earring", results[0].Title);
            Assert.Equal("The Persistence of Memory", results[2].Title);
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