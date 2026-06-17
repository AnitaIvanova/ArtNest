using ARTNEST.Models;

namespace ARTNEST.DAL
{
   
    public interface IVisitedRepository
    {
        bool IsVisited(int userId, int artworkId);
        void MarkVisited(int userId, int artworkId);
        void UnmarkVisited(int userId, int artworkId);
        List<Artwork> GetVisitedByUserId(int userId);
        int CountByUserId(int userId);
        HashSet<int> GetVisitedIdsByUserId(int userId);
    }
}
