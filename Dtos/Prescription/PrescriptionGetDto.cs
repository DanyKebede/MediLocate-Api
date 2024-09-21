using mediAPI.Dtos.Customer;
using mediAPI.Dtos.Pharmacy;

namespace mediAPI.Dtos.Prescription
{
    public class PrescriptionGetDto
    {
        public Guid PrescriptionId { get; set; }
        public string Dosage { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public int NumberOfTabsToPurchase { get; set; }
        public CustomerDto Customer { get; set; }
        public PharmacyMedicineGetDto pharmacyMedicine { get; set; }
    }
}
