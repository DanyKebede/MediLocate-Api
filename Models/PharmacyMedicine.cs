namespace mediAPI.Models
{

    public class PharmacyMedicine
    {
        public Guid PharmacyId { get; set; }
        public Pharmacy Pharmacy { get; set; } // Navigation property (foreign key)

        public Guid MedicineId { get; set; }
        public Medicine Medicine { get; set; } // Navigation property (foreign key)

        // other properties

        //public int Quantity { get; set; }

        // public decimal Cost { get; set; }

        public decimal CostOfTab { get; set; }

        public int TotalNumberOfTab { get; set; }

        // expire date
        public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>(); // can have many prescriptions
    }
}
