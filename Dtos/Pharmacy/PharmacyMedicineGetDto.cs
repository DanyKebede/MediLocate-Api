using mediAPI.Dtos.Medicine;

namespace mediAPI.Dtos.Pharmacy
{
    public class PharmacyMedicineGetDto
    {
        // public PharmacyDto Pharmacy { get; set; }
        // public MedicineDto Medicine { get; set; }

        // public decimal Cost { get; set; } 
        public PharmacyDto Pharmacy { get; set; }
        public MedicineDto Medicine { get; set; }

        public decimal CostOfTab { get; set; } // Pharmacy-specific cost for the medicine
        public int TotalNumberOfTab { get; set; }
    }
}
