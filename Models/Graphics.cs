namespace backend.Models
{
    public class Graphics
    {
        public int Id { get; set; }

        public string graphic { get; set; } = string.Empty;

        public string title { get; set; } = string.Empty;

        public string description { get; set; } = string.Empty;

        public DateTime created_at { get; set; }

        public DateTime updated_at { get; set; }
    }
}
