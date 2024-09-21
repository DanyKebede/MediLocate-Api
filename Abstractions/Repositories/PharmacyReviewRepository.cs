using mediAPI.Data;
using MediLast.Abstractions.Interfaces;
using MediLast.Models;
using Microsoft.EntityFrameworkCore;

namespace MediLast.Abstractions.Repositories
{
    public class PharmacyReviewRepository : IPharmacyReviewRepository
    {
        private readonly MediDbContext _dbContext;
        public PharmacyReviewRepository(MediDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PharmacyReview> AddPharmacyReviewAsync(PharmacyReview pharmacyReview)
        {
            _dbContext.PharmacyReviews.Add(pharmacyReview);
            await _dbContext.SaveChangesAsync();
            return pharmacyReview;
        }


        public async Task<IEnumerable<PharmacyReview>> GetPharmacyReviewsByPharmacyIdAsync(Guid pharmacyId)
        {
            var reviews = await _dbContext.PharmacyReviews.Include(x => x.Pharmacy).Where(x => x.PharmacyId == pharmacyId).ToListAsync();
            return reviews;
        }

        public async Task<PharmacyReview?> GetPharamcyReviewByReviewIdAsync(Guid reviewId)
        {
            var review = await _dbContext.PharmacyReviews.Include(x => x.Pharmacy).FirstOrDefaultAsync(x => x.PharmacyReviewId == reviewId);
            if (review == null) return null;
            return review;
        }
    }
}
