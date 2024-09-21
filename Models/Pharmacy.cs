namespace mediAPI.Models
{
    public class Pharmacy
    {
        public Guid PharmacyId { get; set; }

        public string description { get; set; } = string.Empty;
        public string name { get; set; } = string.Empty;
        public string address { get; set; } = string.Empty;
        public string imageUrl { get; set; } = string.Empty;
        public string document { get; set; } = string.Empty;
        public double lattitude { get; set; }
        public double longitude { get; set; }
        public TimeSpan openingHours { get; set; } = TimeSpan.Zero;
        public TimeSpan closingHours { get; set; } = TimeSpan.Zero;
        public List<string> openingDays { get; set; } = new List<string>();
        public Account Account { get; set; }
        public Guid AccountId { get; set; }

        public Boolean isFirstTime { get; set; } = true;
        public PharmacyStatus status { get; set; } = PharmacyStatus.Idle;

        // Relationships
        public ICollection<PharmacyMedicine>? PharmaciesMedicines { get; set; }// Navigation property for many-to-many relationship
                                                                               //   public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>(); // can have many prescriptions
    }

    public enum PharmacyStatus
    {
        Idle,
        OnProgress,
        Approved,
        Rejected
    }
}


