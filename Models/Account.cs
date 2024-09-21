using Microsoft.AspNetCore.Identity;

namespace mediAPI.Models
{
    public class Account : IdentityUser<Guid>
    {
        public ICollection<Message> MessagesSent { get; set; }
        public ICollection<Message> MessagesReceived { get; set; }
    }
}
