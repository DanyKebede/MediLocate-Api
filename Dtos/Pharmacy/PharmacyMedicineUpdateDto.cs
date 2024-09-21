namespace mediAPI.Dtos.Pharmacy
{
    public class PharmacyMedicineUpdateDto
    {

        // public decimal Cost { get; set; } 
        public decimal CostOfTab { get; set; } // Pharmacy-specific cost for the medicine
        public int NumberOfPackage { get; set; }
        public int TabPerPackage { get; set; }

    }
}
