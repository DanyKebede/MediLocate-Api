using MediLast.Models;

namespace MediLast.Abstractions.Interfaces
{
    public interface IPharmacyReviewRepository
    {
        Task<PharmacyReview> AddPharmacyReviewAsync(PharmacyReview pharmacyReview);

        Task<IEnumerable<PharmacyReview>> GetPharmacyReviewsByPharmacyIdAsync(Guid pharmacyId);

        Task<PharmacyReview?> GetPharamcyReviewByReviewIdAsync(Guid reviewId);
    }
}
