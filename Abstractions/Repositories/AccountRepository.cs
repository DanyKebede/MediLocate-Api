using mediAPI.Abstractions.Interfaces;
using mediAPI.Data;
using mediAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace mediAPI.Abstractions.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly MediDbContext _dbContext;
        public AccountRepository(MediDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Admin> CreateAdminAsync(Account account)
        {
            var newAdmin = new Admin();
            newAdmin.AccountId = account.Id;
            _dbContext.Admins.Add(newAdmin);
            await _dbContext.SaveChangesAsync();
            return newAdmin;

        }

        public async Task<Admin?> GetAdminByAccountIdAsync(Guid accountId)
        {
            var admin = await _dbContext.Admins
                    .Include(p => p.Account)
                    .FirstOrDefaultAsync(p => p.AccountId == accountId);
            return admin;
        }

        public async Task<Customer> CreateCustomerAsync(Account account)
        {
            var newCustomer = new Customer();
            newCustomer.AccountId = account.Id;
            _dbContext.Customers.Add(newCustomer);
            await _dbContext.SaveChangesAsync();
            return newCustomer;
        }

        public async Task<Pharmacy> CreatePharmacyAsync(Account account)
        {
            var newPharmacy = new Pharmacy();
            newPharmacy.AccountId = account.Id;
            newPharmacy.name = account.UserName;
            _dbContext.Pharmacies.Add(newPharmacy);
            await _dbContext.SaveChangesAsync();
            return newPharmacy;
        }



        public async Task<Customer?> GetCustomerByAccountIdAsync(Guid accountId)
        {
            var customer = await _dbContext.Customers
               .Include(p => p.Account)
               .FirstOrDefaultAsync(p => p.AccountId == accountId);

            return customer;
        }

        public async Task<Pharmacy?> GetPharmacyByAccountIdAsync(Guid accountId)
        {
            var pharmacy = await _dbContext.Pharmacies
               .Include(p => p.Account)
               .FirstOrDefaultAsync(p => p.AccountId == accountId);
            return pharmacy;
        }


    }
}
