namespace ARTNEST.Models
{
    public class WishlistItem
    {
        public int Id { get; set; }
        public int ArtworkId { get; set; }
        public DateTime SavedDate { get; set; }
        public Artwork? Artwork { get; set; }
    }
}