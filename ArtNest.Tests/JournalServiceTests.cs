using ARTNEST.BLL;
using ArtNest.Tests.Mocks;
using Xunit;

namespace ArtNest.Tests
{
    public class JournalServiceTests
    {
        private JournalService CreateService()
        {
            var mock = new MockJournalRepository();
            return new JournalService(mock);
        }

        [Fact]
        public void AddEntry_WithValidReflection_ReturnsNullAndSaves()
        {
            var service = CreateService();
            string? result = service.AddEntry(1, 10, "This painting really moved me today.");
            Assert.Null(result);
            Assert.Single(service.GetEntries(1));
        }

        [Fact]
        public void AddEntry_WithNoArtwork_ReturnsErrorAndSavesNothing()
        {
            var service = CreateService();
            string? result = service.AddEntry(1, 0, "A perfectly long reflection here.");
            Assert.NotNull(result);
            Assert.Empty(service.GetEntries(1));
        }

        [Fact]
        public void AddEntry_WithBlankReflection_ReturnsError()
        {
            var service = CreateService();
            string? result = service.AddEntry(1, 10, "   ");
            Assert.NotNull(result);
            Assert.Empty(service.GetEntries(1));
        }

        [Fact]
        public void GetEntries_ReturnsOnlyThatUsersEntries()
        {
            var service = CreateService();
            service.AddEntry(1, 10, "User one's reflection here.");
            service.AddEntry(1, 11, "User one's second reflection.");
            service.AddEntry(2, 10, "User two's reflection here.");
            Assert.Equal(2, service.GetEntries(1).Count);
            Assert.Single(service.GetEntries(2));
        }

        [Fact]
        public void UpdateEntry_ChangesTheReflection()
        {
            var service = CreateService();
            service.AddEntry(1, 10, "Original reflection text.");
            var entry = service.GetEntries(1)[0];
            service.UpdateEntry(entry.Id, 1, "Updated reflection text here.");
            Assert.Equal("Updated reflection text here.", service.GetEntries(1)[0].Reflection);
        }

        [Fact]
        public void UpdateEntry_WithShortReflection_StillUpdates()
        {
            var service = CreateService();
            service.AddEntry(1, 10, "Original reflection text.");
            var entry = service.GetEntries(1)[0];
            service.UpdateEntry(entry.Id, 1, "ok");
            Assert.Equal("ok", service.GetEntries(1)[0].Reflection);
        }

        [Fact]
        public void DeleteEntry_RemovesTheEntry()
        {
            var service = CreateService();
            service.AddEntry(1, 10, "A reflection to be deleted.");
            var entry = service.GetEntries(1)[0];
            service.DeleteEntry(entry.Id, 1);
            Assert.Empty(service.GetEntries(1));
        }

        [Fact]
        public void DeleteEntry_DoesNotDeleteAnotherUsersEntry()
        {
            var service = CreateService();
            service.AddEntry(1, 10, "User one's private reflection.");
            var entry = service.GetEntries(1)[0];
            service.DeleteEntry(entry.Id, 2);
            Assert.Single(service.GetEntries(1));
        }
    }
}