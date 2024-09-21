using mediAPI.Abstractions.Interfaces;
using mediAPI.Data;
using mediAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace mediAPI.Abstractions.Repositories
{
    public class PrescriptionRepository : IPrescriptionRepository
    {
        private readonly MediDbContext _dbContext;

        public PrescriptionRepository(MediDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Prescription>> GetPrescriptionsByCustomerIdAsync(Guid id)
        {
            var prescriptions = await _dbContext.Prescriptions.Where(x => x.CustomerId == id)
                .Include(x => x.Customer).Include(x => x.pharmacyMedicine).ThenInclude(x => x.Pharmacy).ThenInclude(x => x.Account)
                .Include(x => x.pharmacyMedicine).ThenInclude(x => x.Medicine).ToListAsync();
            return prescriptions;
        }

        public async Task<IEnumerable<Prescription>> GetPrescriptionsByPharmacyIdAsync(Guid id)
        {
            var prescriptions = await _dbContext.Prescriptions.Include(x => x.pharmacyMedicine)
                .Where(x => x.pharmacyMedicine!.PharmacyId == id)
                .Include(x => x.Customer).ThenInclude(x => x.Account)
                .Include(x => x.pharmacyMedicine!.Pharmacy).ThenInclude(x => x.Account)
                .Include(x => x.pharmacyMedicine!.Medicine).ToListAsync();
            return prescriptions;
        }
        public async Task<Prescription?> GetPrescriptionByIdAsync(Guid id)
        {
            var prescription = await _dbContext.Prescriptions
                .Include(x => x.Customer).ThenInclude(x => x.Account)
                .Include(x => x.pharmacyMedicine).ThenInclude(x => x.Pharmacy).ThenInclude(x => x.Account)
                .Include(x => x.pharmacyMedicine).ThenInclude(x => x.Medicine).FirstOrDefaultAsync(x => x.PrescriptionId == id);

            if (prescription == null) return null;
            return prescription;
        }


        public async Task<Prescription> AddPrescriptionAsync(Prescription prescription)
        {
            _dbContext.Prescriptions.Add(prescription);
            await _dbContext.SaveChangesAsync();
            return prescription;
        }

        public async Task<Prescription> UpdatePrescriptionAsync(Prescription prescription)
        {
            _dbContext.Prescriptions.Update(prescription);
            await _dbContext.SaveChangesAsync();
            return prescription;
        }
        public async Task<Prescription> DeletePrescriptionAsync(Prescription prescription)
        {
            _dbContext.Prescriptions.Remove(prescription);
            await _dbContext.SaveChangesAsync();
            return prescription;
        }



    }
}
