using mediAPI.Models;

namespace mediAPI.Abstractions.Interfaces
{
    public interface IMedicineRepository
    {


        Task<IEnumerable<Medicine>> GetMedicinesAsync();
        Task<IEnumerable<Medicine>> GetMedicinesByQueryAsync();
        Task<IEnumerable<Medicine>> SearchMedicineByNameAsync(string name);
        Task<Medicine?> GetMedicineByIdAsync(Guid id);



        // admin
        Task<Medicine> AddMedicineAsync(Medicine medicine);

        Task<IEnumerable<Medicine>> AddMedicineRangeAsync(List<Medicine> medicines);

        Task<Medicine> UpdateMedicineAsync(Medicine medicine);

        Task<Medicine> DeleteMedicineAsync(Medicine medicine);
    }
}
