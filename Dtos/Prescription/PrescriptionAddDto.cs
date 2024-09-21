using System.ComponentModel.DataAnnotations;

namespace mediAPI.Dtos.Prescription
{

    public class PrescriptionBatchAddDto
    {
        [Required]
        public Guid CustomerId { get; set; }

        [Required]
        public List<PrescriptionAddDto> Prescriptions { get; set; }
    }

    public class PrescriptionAddDto
    {

        [Required]
        public string Dosage { get; set; }

        [Required]
        public string Notes { get; set; }

        [Required]
        public int NumberOfTabsToPurchase { get; set; }


        [Required]
        public Guid CustomerId { get; set; }


        [Required]
        public Guid MedicineId { get; set; }
    }
}
