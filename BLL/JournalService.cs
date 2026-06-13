using ARTNEST.DAL;
using ARTNEST.Models;

namespace ARTNEST.BLL
{
   
    public class JournalService
    {
        private readonly IJournalRepository _journalRepository;

        public JournalService(IJournalRepository journalRepository)
        {
            _journalRepository = journalRepository;
        }

        public List<JournalEntry> GetEntries(int userId)
        {
            return _journalRepository.GetByUserId(userId);
        }

        public string? AddEntry(int userId, int artworkId, string reflection)
        {
            if (artworkId == 0)
                return "Please select an artwork.";
            if (string.IsNullOrWhiteSpace(reflection))
                return "Please write a reflection before saving.";

            _journalRepository.Add(userId, artworkId, reflection);
            return null;
        }

        public void UpdateEntry(int entryId, int userId, string reflection)
        {
            _journalRepository.Update(entryId, userId, reflection);
           
        }

        public void DeleteEntry(int entryId, int userId)
        {
            _journalRepository.Delete(entryId, userId);
        }
    }
}
