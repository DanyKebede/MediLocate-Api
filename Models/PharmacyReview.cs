using mediAPI.Models;

namespace MediLast.Models
{

    public class PharmacyReview
    {
        public Guid PharmacyReviewId { get; set; }
        public Guid PharmacyId { get; set; }
        public Pharmacy Pharmacy { get; set; }
        public ReviewDecision Decision { get; set; }
        public string Comments { get; set; } = string.Empty;
        public DateTime ReviewDate { get; set; } = DateTime.UtcNow;
    }

    public enum ReviewDecision
    {
        Rejected,
        Approved
    }
}