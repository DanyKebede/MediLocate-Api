namespace mediAPI.Models
{
    public class Message
    {
        public Guid MessageId { get; set; }
        public Guid SenderId { get; set; }
        public string SenderUsername { get; set; }
        public Account Sender { get; set; }
        public Guid RecipientId { get; set; }
        public string RecipientUsername { get; set; }
        public Account Recipient { get; set; }
        public string? Content { get; set; }
        public string? ImageData { get; set; }
        public DateTime? DateRead { get; set; }
        public DateTime MessageSent { get; set; } = DateTime.UtcNow;

    }
}
