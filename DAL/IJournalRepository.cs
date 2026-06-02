using ARTNEST.Models;

namespace ARTNEST.DAL
{
 
    public interface IJournalRepository
    {
        List<JournalEntry> GetByUserId(int userId);
        int CountByUserId(int userId);
        void Add(int userId, int artworkId, string reflection);
        void Update(int entryId, int userId, string reflection);
        void Delete(int entryId, int userId);
    }
}
