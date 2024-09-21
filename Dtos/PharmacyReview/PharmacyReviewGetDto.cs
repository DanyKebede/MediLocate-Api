using MediLast.Models;

namespace MediLast.Dtos.PharmacyReview
{
    public class PharmacyReviewGetDto
    {
        public Guid PharmacyReviewId { get; set; }
        public Guid PharmacyId { get; set; }
        public string PharmacyName { get; set; }
        //        public PharmacyDto Pharmacy { get; set; }
        public ReviewDecision Decision { get; set; }
        public string Comments { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}
