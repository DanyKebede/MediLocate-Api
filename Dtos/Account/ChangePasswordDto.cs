namespace MediLast.Dtos.Account
{
    public class ChangePasswordDto
    {
        public string userName { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}