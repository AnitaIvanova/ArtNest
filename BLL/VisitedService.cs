using ARTNEST.DAL;
using ARTNEST.Models;

namespace ARTNEST.BLL
{
   
    public class VisitedService
    {
        private readonly IVisitedRepository _visitedRepository;

        public VisitedService(IVisitedRepository visitedRepository)
        {
            _visitedRepository = visitedRepository;
        }

        public bool IsVisited(int userId, int artworkId)
        {
            return _visitedRepository.IsVisited(userId, artworkId);
        }

              public bool ToggleVisited(int userId, int artworkId)
        {
            bool alreadyVisited = _visitedRepository.IsVisited(userId, artworkId);
            if (alreadyVisited)
                _visitedRepository.UnmarkVisited(userId, artworkId);
            else
                _visitedRepository.MarkVisited(userId, artworkId);

            return !alreadyVisited;
        }

        public List<Artwork> GetVisitedArtworks(int userId)
        {
            return _visitedRepository.GetVisitedByUserId(userId);
        }

        public HashSet<int> GetVisitedIds(int userId)
        {
            return _visitedRepository.GetVisitedByUserId(userId)
                                     .Select(a => a.Id)
                                     .ToHashSet();
        }

        public int GetVisitedCount(int userId)
        {
            return _visitedRepository.CountByUserId(userId);
        }
    }
}
