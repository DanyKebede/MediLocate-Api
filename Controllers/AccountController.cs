using AutoMapper;
using mediAPI.Abstractions.Interfaces;
using mediAPI.Dtos;
using mediAPI.Dtos.Account;
using mediAPI.Dtos.Pharmacy;
using mediAPI.Models;
using MediLast.Dtos.Account;
using MediLast.Dtos.Admin;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace mediAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<Account> _userManager;
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        public AccountController(UserManager<Account> userManager, IAccountRepository accountRepository, IMapper mapper, ITokenService tokenService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _tokenService = tokenService;
            _accountRepository = accountRepository;
        }

        [HttpPost("register/pharmacy")]
        public async Task<IActionResult> RegisterPharmacy([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (await AccountExists(registerDto.Username!.ToLower())) return BadRequest("Username already exists try other username");

            var accountUser = _mapper.Map<Account>(registerDto);

            accountUser.UserName = accountUser.UserName.ToLower();

            var result = await _userManager.CreateAsync(accountUser, registerDto.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);

            var roleResult = await _userManager.AddToRoleAsync(accountUser, "Pharmacy");

            if (!roleResult.Succeeded) return BadRequest(result.Errors);

            var newPharamcy = await _accountRepository.CreatePharmacyAsync(accountUser);
            var pharamcyGetDto = _mapper.Map<PharmacyGetDto>(newPharamcy);

            return new JsonResult(new
            {
                pharmacy = pharamcyGetDto,
                Token = await _tokenService.CreateToken(accountUser)
            })
            { StatusCode = 201 };
        }


        [HttpPost("login/pharmacy")]
        public async Task<IActionResult> LoginPharmacy(LoginDto loginDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username!.ToLower());
            if (user == null) return Unauthorized("Invalid username");

            var res = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!res) return Unauthorized("Invalid password");

            var pharmacy = await _accountRepository.GetPharmacyByAccountIdAsync(user.Id);
            if (pharmacy == null)
            {
                var errResponse = new ApiError(400, "Pharmacy Not Found");
                return new JsonResult(errResponse) { StatusCode = errResponse.StatusCode };
            }
            var pharamcyGetDto = _mapper.Map<PharmacyGetDto>(pharmacy);
            var response = new ApiResponse(200, new
            {
                pharmacy = pharamcyGetDto,
                Token = await _tokenService.CreateToken(user)
            },
                 message: "Successfully logged In"
            );
            return new JsonResult(response) { StatusCode = response.StatusCode };
        }


        [HttpPost("register/customer")]
        public async Task<IActionResult> RegisterCustomer([FromBody] RegisterCustomerDto registerDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (await AccountExists(registerDto.Username!.ToLower())) return BadRequest("Account already exists");

            var accountUser = _mapper.Map<Account>(registerDto);

            accountUser.UserName = accountUser.UserName.ToLower();

            var result = await _userManager.CreateAsync(accountUser, registerDto.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);

            var roleResult = await _userManager.AddToRoleAsync(accountUser, "Customer");

            if (!roleResult.Succeeded) return BadRequest(result.Errors);

            var newCustomer = await _accountRepository.CreateCustomerAsync(accountUser);

            var payload = new
            {
                User = new
                {
                    UserName = newCustomer.Account.UserName,
                    Email = newCustomer.Account.Email,
                }
            };
            var sucResponse = new ApiResponse(201, payload, "Successfully registered");

            return new JsonResult(sucResponse) { StatusCode = sucResponse.StatusCode };
        }


        [HttpPost("login/customer")]
        public async Task<IActionResult> LoginCustomer(LoginDto loginDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username!.ToLower());
            if (user == null) return Unauthorized("Invalid username");

            var res = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!res) return Unauthorized("Invalid password");

            var customer = await _accountRepository.GetCustomerByAccountIdAsync(user.Id);
            if (customer == null)
            {
                var errResponse = new ApiError(404, "Customer Not Found");
                return new JsonResult(errResponse) { StatusCode = errResponse.StatusCode };
            }
            var payload = new
            {
                User = new
                {
                    UserName = customer!.Account.UserName,
                    Email = customer.Account.Email,
                },
                Token = await _tokenService.CreateToken(user)
            };
            var sucResponse = new ApiResponse(201, payload, "Succesfully logged In");
            return new JsonResult(sucResponse) { StatusCode = sucResponse.StatusCode };
        }

        // I want to have a method that will change the user password
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == changePasswordDto.userName!.ToLower());
            if (user == null) return Unauthorized("Invalid username");

            var res = await _userManager.CheckPasswordAsync(user, changePasswordDto.OldPassword);
            if (!res) return Unauthorized("Invalid password");

            var result = await _userManager.ChangePasswordAsync(user, changePasswordDto.OldPassword, changePasswordDto.NewPassword);
            if (!result.Succeeded) return BadRequest(result.Errors);

            var payload = new
            {
                User = new
                {
                    UserName = user.UserName,
                    Email = user.Email,
                }
            };
            var sucResponse = new ApiResponse(201, payload, "Password changed successfully");
            return new JsonResult(sucResponse) { StatusCode = sucResponse.StatusCode };
        }

        [HttpPost("register/admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (await AccountExists(registerDto.Username!.ToLower())) return BadRequest("Username already exists try other username");

            var accountUser = _mapper.Map<Account>(registerDto);

            accountUser.UserName = accountUser.UserName.ToLower();

            var result = await _userManager.CreateAsync(accountUser, registerDto.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);

            var roleResult = await _userManager.AddToRoleAsync(accountUser, "Admin");

            if (!roleResult.Succeeded) return BadRequest(result.Errors);

            var newAdmin = await _accountRepository.CreateAdminAsync(accountUser);


            return new JsonResult(new
            {
                Message = "Successfully Registered Admin",
            })
            { StatusCode = 201 };
        }

        [HttpPost("login/admin")]
        public async Task<IActionResult> LoginAdmin(LoginDto loginDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username!.ToLower());
            if (user == null) return Unauthorized("Invalid username");

            var res = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!res) return Unauthorized("Invalid password");

            var admin = await _accountRepository.GetAdminByAccountIdAsync(user.Id);
            if (admin == null)
            {
                var errResponse = new ApiError(404, "Admin Not Found");
                return new JsonResult(errResponse) { StatusCode = errResponse.StatusCode };
            }
            var adminGetDto = _mapper.Map<AdminGetDto>(admin);
            var payload = new
            {
                Admin = adminGetDto,
                Token = await _tokenService.CreateToken(user)
            };
            var sucResponse = new ApiResponse(201, payload, "Succesfully logged In");
            return new JsonResult(sucResponse) { StatusCode = sucResponse.StatusCode };
        }

        private async Task<bool> AccountExists(string username)
        {
            return await _userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}
