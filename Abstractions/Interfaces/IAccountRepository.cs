using mediAPI.Models;

namespace mediAPI.Abstractions.Interfaces
{
    public interface IAccountRepository
    {
        Task<Pharmacy> CreatePharmacyAsync(Account account);
        Task<Customer> CreateCustomerAsync(Account account);

        Task<Customer?> GetCustomerByAccountIdAsync(Guid accountId);
        Task<Pharmacy?> GetPharmacyByAccountIdAsync(Guid accountId);

        Task<Admin> CreateAdminAsync(Account account);
        Task<Admin> GetAdminByAccountIdAsync(Guid accountId);
    }
}
