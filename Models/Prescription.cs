namespace mediAPI.Models
{
    public class Prescription
    {
        public Guid PrescriptionId { get; set; }

        public string Dosage { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;


        public int NumberOfTabsToPurchase { get; set; }

        // Relationships 
        public Guid CustomerId { get; set; }
        public Customer? Customer { get; set; } // Many-to-One relationship with user (foreign key)  | user can have many prescription

        //   public Guid? PharmacyId { get; set; }
        //   public Pharmacy? Pharmacy { get; set; } // Many-to-One relationship with pharmacy (foreign key)  | pharmacy can write many prescription

        //   public Guid? MedicineId { get; set; }
        //   public Medicine? Medicine { get; set; } // Many-to-One relationship with medicine (foreign key) | medicine can have many prescription written by a pharmacy

        public PharmacyMedicine? pharmacyMedicine { get; set; } // Many-to-One relationship with pharmacyMedicine (foreign key) | pharmacyMedicine can have many prescription written by a pharmacy
    }
}
