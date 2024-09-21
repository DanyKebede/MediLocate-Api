namespace mediAPI.Models
{
    public class Admin
    {
        public Guid AdminId { get; set; }
        public string AdminInfo { get; set; } = string.Empty;
        public Account Account { get; set; }
        public Guid AccountId { get; set; }
    }
}
