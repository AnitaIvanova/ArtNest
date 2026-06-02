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

        public int GetEntryCount(int userId)
        {
            return _journalRepository.CountByUserId(userId);
        }


        public string? AddEntry(int userId, int artworkId, string reflection)
        {
            if (artworkId == 0)
                return "Please select an artwork.";
            if (string.IsNullOrWhiteSpace(reflection))
                return "Please write a reflection before saving.";
            if (reflection.Trim().Length < 10)
                return "Your reflection is too short. Write at least a sentence.";

            _journalRepository.Add(userId, artworkId, reflection.Trim());
            return null;
        }

        public string? UpdateEntry(int entryId, int userId, string reflection)
        {
            if (string.IsNullOrWhiteSpace(reflection) || reflection.Trim().Length < 10)
                return "Reflection is too short. Write at least a sentence.";

            _journalRepository.Update(entryId, userId, reflection.Trim());
            return null;
        }

        public void DeleteEntry(int entryId, int userId)
        {
            _journalRepository.Delete(entryId, userId);
        }
    }
}
