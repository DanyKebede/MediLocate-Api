using mediAPI.Abstractions.Interfaces;
using mediAPI.Data;
using mediAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace mediAPI.Abstractions.Repositories
{
    public class PharmacyRepository : IPharmacyRepository
    {
        private readonly MediDbContext _dbContext;


        public PharmacyRepository(MediDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Pharmacy>> GetPharmaciesAsync()
        {
            var pharmacies = await _dbContext.Pharmacies.Include(p => p.Account).ToListAsync();
            return pharmacies;
        }

        public async Task<Pharmacy?> GetPharmacyByIdAsync(Guid id)
        {
            var pharmacy = await _dbContext.Pharmacies.Include(p => p.Account).FirstOrDefaultAsync(p => p.PharmacyId == id);
            if (pharmacy == null) return null;
            return pharmacy;
        }

        public Task<Pharmacy?> GetPharmacyByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Pharmacy>> SearchPharmacyByNameAsync(string name)
        {
            throw new NotImplementedException();
        }


        public async Task<Pharmacy> UpdatePharmacyAsync(Pharmacy pharmacy)
        {
            _dbContext.Pharmacies.Update(pharmacy);
            await _dbContext.SaveChangesAsync();
            return pharmacy;
        }
        public async Task<Pharmacy> UpdatePharmacyStatusAsync(Pharmacy pharmacy)
        {
            _dbContext.Pharmacies.Update(pharmacy);
            await _dbContext.SaveChangesAsync();
            return pharmacy;
        }

        public Task<Pharmacy> DeletePharmacyAsync(Guid id)
        {
            throw new NotImplementedException();
        }



        public async Task<IEnumerable<PharmacyMedicine>> GetPharamciesByMedicineId(Guid id)
        {
            var pharmacies = await _dbContext.PharmacyMedicines.Where(x => x.MedicineId == id)
                .Include(x => x.Pharmacy).ThenInclude(x => x.Account)
                .Include(x => x.Medicine)
                .ToListAsync();
            return pharmacies;
        }








        public async Task<IEnumerable<PharmacyMedicine>> GetMedicinesByPharmacyIdAsync(Guid pharmacyId)
        {
            var pharmacyMedicines = await _dbContext.PharmacyMedicines.Include(x => x.Pharmacy).ThenInclude(x => x.Account).Include(x => x.Medicine).Where(x => x.PharmacyId == pharmacyId).ToListAsync();
            return pharmacyMedicines;
        }



        public async Task<PharmacyMedicine?> GetMedicineDetailInPharmacyAsync(Guid pharmacyId, Guid medicineId)
        {
            var pharmacyMedicine = await _dbContext.PharmacyMedicines.Include(x => x.Pharmacy).ThenInclude(x => x.Account).Include(x => x.Medicine).FirstOrDefaultAsync(pm => pm.PharmacyId == pharmacyId && pm.MedicineId == medicineId);
            if (pharmacyMedicine == null) return null;
            return pharmacyMedicine;
        }

        public async Task<PharmacyMedicine> AddMedicineToPharmacyAsync(PharmacyMedicine pharmacyMedicine)
        {

            _dbContext.PharmacyMedicines.Add(pharmacyMedicine);
            await _dbContext.SaveChangesAsync();
            return pharmacyMedicine;
        }

        public async Task<PharmacyMedicine> RemoveMedicineFromPharmacyAsync(PharmacyMedicine pharmacyMedicine)
        {
            _dbContext.PharmacyMedicines.Remove(pharmacyMedicine);
            await _dbContext.SaveChangesAsync();
            return pharmacyMedicine;
        }
        public async Task<PharmacyMedicine> UpdateMedicineInPharmacyAsync(PharmacyMedicine pharmacyMedicine)
        {
            _dbContext.PharmacyMedicines.Update(pharmacyMedicine);
            await _dbContext.SaveChangesAsync();
            return pharmacyMedicine;
        }


    }
}
