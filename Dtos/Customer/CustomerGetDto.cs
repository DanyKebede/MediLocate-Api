using mediAPI.Dtos.Account;

namespace mediAPI.Dtos.Customer
{
    public class CustomerGetDto
    {
        public Guid CustomerId { get; set; }
        public string? CustomerInfo { get; set; }
        public AccountDto Account { get; set; }
    }
}
