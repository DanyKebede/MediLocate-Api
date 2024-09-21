using mediAPI.Models;

namespace mediAPI.Abstractions.Interfaces
{
    public interface IPrescriptionRepository
    {
        Task<IEnumerable<Prescription>> GetPrescriptionsByCustomerIdAsync(Guid id);
        Task<IEnumerable<Prescription>> GetPrescriptionsByPharmacyIdAsync(Guid id);
        Task<Prescription?> GetPrescriptionByIdAsync(Guid id);
        Task<Prescription> AddPrescriptionAsync(Prescription prescription);
        Task<Prescription> UpdatePrescriptionAsync(Prescription prescription);
        Task<Prescription> DeletePrescriptionAsync(Prescription prescription);
    }
}
