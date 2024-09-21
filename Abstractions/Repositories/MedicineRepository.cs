using mediAPI.Abstractions.Interfaces;
using mediAPI.Data;
using mediAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace mediAPI.Abstractions.Repositories
{
    public class MedicineRepository : IMedicineRepository
    {
        private readonly MediDbContext _dbContext;
        public MedicineRepository(MediDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Medicine>> GetMedicinesAsync()
        {
            var medicines = await _dbContext.Medicines.ToListAsync();
            return medicines;
        }

        public Task<IEnumerable<Medicine>> GetMedicinesByQueryAsync()
        {
            throw new NotImplementedException();
        }


        public Task<IEnumerable<Medicine>> SearchMedicineByNameAsync(string name)
        {
            throw new NotImplementedException();
        }





        public async Task<Medicine?> GetMedicineByIdAsync(Guid id)
        {
            var medicine = await _dbContext.Medicines.FirstOrDefaultAsync(m => m.MedicineId == id);
            if (medicine == null) return null;
            return medicine;
        }

        public async Task<Medicine> AddMedicineAsync(Medicine medicine)
        {
            _dbContext.Medicines.Add(medicine);
            await _dbContext.SaveChangesAsync();
            return medicine;
        }
        public async Task<Medicine> UpdateMedicineAsync(Medicine medicine)
        {
            _dbContext.Medicines.Update(medicine);
            await _dbContext.SaveChangesAsync();
            return medicine;
        }
        public async Task<Medicine> DeleteMedicineAsync(Medicine medicine)
        {
            _dbContext.Medicines.Remove(medicine);
            await _dbContext.SaveChangesAsync();
            return medicine;
        }

        public async Task<IEnumerable<Medicine>> AddMedicineRangeAsync(List<Medicine> medicines)
        {
            _dbContext.Medicines.AddRange(medicines);
            await _dbContext.SaveChangesAsync();
            return medicines;
        }
    }
}
