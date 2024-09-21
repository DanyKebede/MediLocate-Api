using mediAPI.Abstractions.Interfaces;
using mediAPI.Data;
using mediAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace mediAPI.Abstractions.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {

        private readonly MediDbContext _dbContext;

        public CustomerRepository(MediDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            var customers = await _dbContext.Customers.Include(x => x.Account).ToListAsync();
            return customers;
        }
        public async Task<Customer?> GetCustomerByIdAsync(Guid id)
        {
            var customer = await _dbContext.Customers.Include(x => x.Account).FirstOrDefaultAsync(x => x.CustomerId == id);
            if (customer == null) return null;
            return customer;
        }
        public async Task<Customer> UpdateCustomerAsync(Customer customer)
        {
            _dbContext.Customers.Update(customer);
            await _dbContext.SaveChangesAsync();
            return customer;
        }
        public async Task<Customer> DeleteCustomerAsync(Customer customer)
        {
            _dbContext.Customers.Remove(customer);
            await _dbContext.SaveChangesAsync();
            return customer;
        }
    }
}
