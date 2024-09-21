namespace mediAPI.Dtos.Account
{
    public class AccountDto
    {
        public Guid AccountId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        //   public bool EmailConfirmed { get; set; }
        //  public bool PhoneNumberConfirmed { get; set; }
    }
}
