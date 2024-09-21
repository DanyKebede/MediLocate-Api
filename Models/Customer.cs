namespace mediAPI.Models
{
    public class Customer
    {
        public Guid CustomerId { get; set; }

        public string CustomerInfo { get; set; } = string.Empty;

        public Account Account { get; set; }
        public Guid AccountId { get; set; }

        // Relationships
        public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>(); // can have many prescriptions
    }
}
