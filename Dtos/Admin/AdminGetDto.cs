using mediAPI.Dtos.Account;

namespace MediLast.Dtos.Admin
{
    public class AdminGetDto
    {
        public Guid AdminId { get; set; }
        public string AdminInfo { get; set; } = string.Empty;
        public AccountDto Account { get; set; }
    }
}
