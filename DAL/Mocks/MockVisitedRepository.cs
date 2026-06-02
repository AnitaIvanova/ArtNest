using ARTNEST.Models;

namespace ARTNEST.DAL.Mocks
{
  
    public class MockVisitedRepository : IVisitedRepository
    {
        private readonly List<(int UserId, int ArtworkId)> _visited = new();
        private readonly IArtworkRepository _artworkRepository;

        public MockVisitedRepository(IArtworkRepository artworkRepository)
        {
            _artworkRepository = artworkRepository;
        }

        public bool IsVisited(int userId, int artworkId) =>
            _visited.Any(v => v.UserId == userId && v.ArtworkId == artworkId);

        public void MarkVisited(int userId, int artworkId)
        {
            if (!IsVisited(userId, artworkId))
                _visited.Add((userId, artworkId));
        }

        public void UnmarkVisited(int userId, int artworkId) =>
            _visited.RemoveAll(v => v.UserId == userId && v.ArtworkId == artworkId);

        public List<Artwork> GetVisitedByUserId(int userId)
        {
            return _visited
                .Where(v => v.UserId == userId)
                .Select(v => _artworkRepository.GetById(v.ArtworkId))
                .Where(a => a != null)
                .Cast<Artwork>()
                .ToList();
        }

        public int CountByUserId(int userId) =>
            _visited.Count(v => v.UserId == userId);
    }
}
