using mediAPI.Models;

namespace mediAPI.Abstractions.Interfaces
{
    public interface IPharmacyRepository
    {
        Task<IEnumerable<Pharmacy>> GetPharmaciesAsync();
        Task<Pharmacy?> GetPharmacyByIdAsync(Guid id);
        Task<Pharmacy?> GetPharmacyByNameAsync(string name);
        Task<IEnumerable<Pharmacy>> SearchPharmacyByNameAsync(string name);
        Task<Pharmacy> UpdatePharmacyAsync(Pharmacy pharmacy);
        Task<Pharmacy> DeletePharmacyAsync(Guid id);

        Task<Pharmacy> UpdatePharmacyStatusAsync(Pharmacy pharmacy);



        // get pharmacies by medicine ID
        Task<IEnumerable<PharmacyMedicine>> GetPharamciesByMedicineId(Guid id);


        // pharmacyMedicine
        Task<IEnumerable<PharmacyMedicine>> GetMedicinesByPharmacyIdAsync(Guid pharmacyId);
        Task<PharmacyMedicine?> GetMedicineDetailInPharmacyAsync(Guid pharmacyId, Guid medicineId);

        Task<PharmacyMedicine> AddMedicineToPharmacyAsync(PharmacyMedicine pharmacyMedicine);
        Task<PharmacyMedicine> RemoveMedicineFromPharmacyAsync(PharmacyMedicine pharmacyMedicine);
        Task<PharmacyMedicine> UpdateMedicineInPharmacyAsync(PharmacyMedicine pharmacyMedicine);


    }
}
