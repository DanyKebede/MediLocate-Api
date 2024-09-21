using System.ComponentModel.DataAnnotations;

namespace mediAPI.Dtos.Pharmacy
{


    public class PharmacyMedicineAddDto
    {
        // [Required]
        // public Guid MedicineId { get; set; }
        // [Required]
        // public decimal Cost { get; set; } 
        [Required]
        public Guid MedicineId { get; set; }
        [Required]
        public decimal CostOfTab { get; set; } // Pharmacy-specific cost for the medicine
        [Required]
        public int NumberOfPackage { get; set; }
        [Required]
        public int TabPerPackage { get; set; }
    }
}
