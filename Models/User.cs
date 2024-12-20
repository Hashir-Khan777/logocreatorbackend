namespace backend.Models
{
    public class User
    {
        public int Id { get; set; }

        public string name { get; set; } = string.Empty;

        public string email { get; set; } = string.Empty;

        public string password { get; set; } = string.Empty;

        public string role { get; set; } = string.Empty;

        public DateTime created_at { get; set; }

        public DateTime updated_at { get; set; }
    }
}
