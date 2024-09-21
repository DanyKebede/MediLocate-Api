using AutoMapper;
using mediAPI.Abstractions.Interfaces;
using mediAPI.Dtos.Customer;
using mediAPI.Dtos.Prescription;
using mediAPI.Extensions;
using mediAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace mediAPI.Controllers
{
    [Authorize(Roles = "Customer")]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly UserManager<Account> _userManager;

        private readonly IAccountRepository _accountRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IPrescriptionRepository _prescriptionRepository;
        public CustomerController(IAccountRepository accountRepository, IMapper mapper, UserManager<Account> userManager, ICustomerRepository customerRepository, IPrescriptionRepository prescriptionRepository)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
            _userManager = userManager;
            _customerRepository = customerRepository;
            _prescriptionRepository = prescriptionRepository;
        }


        [HttpGet("profile")]
        public async Task<IActionResult> GetCustomerInfo()
        {
            var userId = Guid.Parse(User.GetUserId());
            var username = User.GetUserName();
            var userEmail = User.GetUserEmail();
            var userRole = User.GetUserRole();

            var customer = await _accountRepository.GetCustomerByAccountIdAsync(userId);
            var customerDto = _mapper.Map<CustomerGetDto>(customer);

            return Ok(customerDto);
        }


        [HttpPut("profile")]
        public async Task<IActionResult> UpdateCustomerInfo([FromBody] CustomerUpdateDto customerUpdateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);


            if (!string.IsNullOrEmpty(customerUpdateDto.UserName) && !await UsernameExists(customerUpdateDto.UserName)) return BadRequest("Username is taken try other username");
            if (!string.IsNullOrEmpty(customerUpdateDto.Email) && !await EmailExists(customerUpdateDto.Email)) return BadRequest("Email is taken try other Email");

            var customer = await GetCustomerProfile();


            if (customer == null) return BadRequest("Customer Doesn't Exist");


            customer.CustomerInfo = string.IsNullOrEmpty(customerUpdateDto.CustomerInfo) ? customer.CustomerInfo : customerUpdateDto.CustomerInfo;
            customer.Account.Email = string.IsNullOrEmpty(customerUpdateDto.Email) ? customer.Account.Email : customerUpdateDto.Email;
            customer.Account.UserName = string.IsNullOrEmpty(customerUpdateDto.UserName) ? customer.Account.UserName : customerUpdateDto.UserName;
            customer.Account.PhoneNumber = string.IsNullOrEmpty(customerUpdateDto.PhoneNumber) ? customer.Account.PhoneNumber : customerUpdateDto.PhoneNumber;

            var customerUpdated = await _customerRepository.UpdateCustomerAsync(customer);


            var customerGetDto = _mapper.Map<CustomerGetDto>(customerUpdated);

            return Ok(customerGetDto);


        }


        [HttpDelete("profile")]
        public async Task<IActionResult> DeleteCustomerInfo()
        {
            return Ok("Customer deleted");
        }


        [HttpGet("my-prescriptions")]
        public async Task<IActionResult> GetAllPrescriptions()
        {
            var customer = await GetCustomerProfile();
            var prescriptions = await _prescriptionRepository.GetPrescriptionsByCustomerIdAsync(customer!.CustomerId);
            var prescriptionsGetDto = _mapper.Map<List<PrescriptionGetDto>>(prescriptions);
            return Ok(prescriptionsGetDto);
        }

        [HttpGet("my-prescriptions/{id}")]
        public async Task<IActionResult> GetPrescriptionById([FromRoute] Guid id)
        {
            var prescription = await _prescriptionRepository.GetPrescriptionByIdAsync(id);
            if (prescription == null) return BadRequest("prescription not found");
            var prescriptionGetDto = _mapper.Map<PrescriptionGetDto>(prescription);
            return Ok(prescriptionGetDto);
        }


        private async Task<bool> UsernameExists(string username)
        {
            return await _userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
        private async Task<bool> EmailExists(string email)
        {
            return await _userManager.Users.AnyAsync(x => x.Email == email.ToLower());

        }

        private async Task<Customer?> GetCustomerProfile()
        {
            var accountId = Guid.Parse(User.GetUserId());
            var customer = await _accountRepository.GetCustomerByAccountIdAsync(accountId);
            if (customer == null) return null;
            return customer;
        }




    }
}
