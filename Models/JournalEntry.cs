namespace ARTNEST.Models
{
    public class JournalEntry
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ArtworkId { get; set; }
        public string Reflection { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public Artwork? Artwork { get; set; }
    }
}
