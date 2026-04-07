namespace ARTNEST.Models
{
    public class Artwork
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Artist { get; set; } = "";
        public string Museum { get; set; } = "";
        public string City { get; set; } = "";
        public string Year { get; set; } = "";
        public string Description { get; set; } = "";
        public string Image { get; set; } = "";
    }
}