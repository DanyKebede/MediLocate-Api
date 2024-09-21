namespace MediLast.Dtos.Message
{
    public class CreateMessageDto
    {
        public string RecipientUsername { get; set; }
        public string? Content { get; set; }
        public string? ImageData { get; set; }
    }
}
