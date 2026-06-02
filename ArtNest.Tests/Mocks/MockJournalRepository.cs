using ARTNEST.Models;
using ARTNEST.DAL;

namespace ArtNest.Tests.Mocks
{
  
    public class MockJournalRepository : IJournalRepository
    {
        private readonly List<JournalEntry> _entries = new();
        private int _nextId = 1;

        public List<JournalEntry> GetByUserId(int userId) =>
            _entries.Where(e => e.UserId == userId)
                    .OrderByDescending(e => e.Date)
                    .ToList();

        public int CountByUserId(int userId) =>
            _entries.Count(e => e.UserId == userId);

        public void Add(int userId, int artworkId, string reflection)
        {
            _entries.Add(new JournalEntry
            {
                Id = _nextId++,
                UserId = userId,
                ArtworkId = artworkId,
                Reflection = reflection,
                Date = DateTime.Now
            });
        }

        public void Update(int entryId, int userId, string reflection)
        {
            JournalEntry? entry = _entries.FirstOrDefault(e => e.Id == entryId && e.UserId == userId);
            if (entry != null)
                entry.Reflection = reflection;
        }

        public void Delete(int entryId, int userId) =>
            _entries.RemoveAll(e => e.Id == entryId && e.UserId == userId);
    }
}
