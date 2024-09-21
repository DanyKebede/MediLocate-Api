

namespace mediAPI.Models
{
    public class Medicine
    {
        public Guid MedicineId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string SideEffects { get; set; } = string.Empty;
        public string Precautions { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;

        public ICollection<PharmacyMedicine>? PharmaciesMedicines { get; set; } // Navigation property for many-to-many relationship

        //public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>(); // can have many prescriptions
    }
}
