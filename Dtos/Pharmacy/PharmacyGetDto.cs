using mediAPI.Dtos.Account;
using mediAPI.Models;

namespace mediAPI.Dtos.Pharmacy
{
    public class PharmacyGetDto
    {
        public Guid PharmacyId { get; set; }
        public string description { get; set; } = string.Empty;
        public string name { get; set; } = string.Empty;
        public string address { get; set; } = string.Empty;
        public string imageUrl { get; set; } = string.Empty;
        public string document { get; set; } = string.Empty;
        public double lattitude { get; set; }
        public double longitude { get; set; }
        public TimeSpan openingHours { get; set; }
        public TimeSpan closingHours { get; set; }
        public List<string> openingDays { get; set; }
        public AccountDto Account { get; set; }
        public Boolean isFirstTime { get; set; }
        public PharmacyStatus status { get; set; }
    }
}
