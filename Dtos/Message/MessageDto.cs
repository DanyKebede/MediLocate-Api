namespace MediLast.Dtos.Message
{
    public class MessageDto
    {
        public Guid MessageId { get; set; }
        public Guid SenderId { get; set; }
        public string SenderUsername { get; set; }
        public Guid RecipientId { get; set; }
        public string RecipientUsername { get; set; }
        public string? Content { get; set; }
        public string? ImageData { get; set; }
        public DateTime? DateRead { get; set; }
        public DateTime MessageSent { get; set; }
    }
}
