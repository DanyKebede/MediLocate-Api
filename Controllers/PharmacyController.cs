using AutoMapper;
using mediAPI.Abstractions.Interfaces;
using mediAPI.Dtos.Pharmacy;
using mediAPI.Dtos.Prescription;
using mediAPI.Extensions;
using mediAPI.Models;
using MediLast.Abstractions.Interfaces;
using MediLast.Dtos.PharmacyReview;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace mediAPI.Controllers
{
    [Authorize(Roles = "Pharmacy")]
    [Route("api/[controller]")]
    [ApiController]
    public class PharmacyController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAccountRepository _accountRepository;
        private readonly IPharmacyRepository _pharmacyRepository;
        private readonly IMedicineRepository _medicineRepository;
        private readonly IPrescriptionRepository _prescriptionRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IPharmacyReviewRepository _pharmacyReviewRepository;
        private readonly UserManager<Account> _userManager;
        private readonly ISmsService _smsService;

        public PharmacyController(IAccountRepository accountRepository, IMapper mapper, IPharmacyRepository pharmacyRepository, UserManager<Account> userManager,
         IPharmacyReviewRepository pharmacyReviewRepository, ISmsService smsService, IMedicineRepository medicineRepository, IPrescriptionRepository prescriptionRepository, ICustomerRepository customerRepository)
        {
            _mapper = mapper;
            _userManager = userManager;
            _accountRepository = accountRepository;
            _pharmacyRepository = pharmacyRepository;
            _medicineRepository = medicineRepository;
            _prescriptionRepository = prescriptionRepository;
            _customerRepository = customerRepository;
            _pharmacyReviewRepository = pharmacyReviewRepository;
            _smsService = smsService;
        }


        [HttpGet("profile")]
        public async Task<IActionResult> GetPharmacyInfo()
        {
            var userId = Guid.Parse(User.GetUserId());
            var username = User.GetUserName();
            var userEmail = User.GetUserEmail();
            var userRole = User.GetUserRole();

            var pharmacy = await _accountRepository.GetPharmacyByAccountIdAsync(userId);
            var pharmacyDto = _mapper.Map<PharmacyGetDto>(pharmacy);
            return Ok(pharmacyDto);
        }


        [HttpPut("profile")]
        public async Task<IActionResult> UpdatePharmacyInfo([FromBody] PharmacyUpdateDto pharmacyUpdateDto)
        {
            // if (!ModelState.IsValid) return BadRequest(ModelState);

            //if (!string.IsNullOrEmpty(pharmacyUpdateDto.UserName) && await UsernameExists(pharmacyUpdateDto.UserName)) return BadRequest("Username is taken try other username");
            // if (!string.IsNullOrEmpty(pharmacyUpdateDto.Email) && await EmailExists(pharmacyUpdateDto.Email)) return BadRequest("Email is taken try other email");


            var pharmacy = await GetPharmacyProfile();
            if (pharmacy == null) return BadRequest("Pharmacy Doesn't Exist");

            //never
            //   pharmacy.Account.UserName = string.IsNullOrEmpty(pharmacyUpdateDto.UserName) ? pharmacy.Account.UserName : pharmacyUpdateDto.UserName;

            pharmacy.description = string.IsNullOrEmpty(pharmacyUpdateDto.description) ? pharmacy.description : pharmacyUpdateDto.description;
            pharmacy.name = string.IsNullOrEmpty(pharmacyUpdateDto.name) ? pharmacy.name : pharmacyUpdateDto.name;
            pharmacy.address = string.IsNullOrEmpty(pharmacyUpdateDto.address) ? pharmacy.address : pharmacyUpdateDto.address;
            pharmacy.lattitude = pharmacyUpdateDto.lattitude == 0 ? pharmacy.lattitude : pharmacyUpdateDto.lattitude;
            pharmacy.longitude = pharmacyUpdateDto.longitude == 0 ? pharmacy.longitude : pharmacyUpdateDto.longitude;

            pharmacy.openingHours = pharmacyUpdateDto.openingHours == TimeSpan.Zero ? pharmacy.openingHours : pharmacyUpdateDto.openingHours;
            pharmacy.closingHours = pharmacyUpdateDto.closingHours == TimeSpan.Zero ? pharmacy.closingHours : pharmacyUpdateDto.closingHours;

            pharmacy.openingDays = pharmacyUpdateDto.openingDays == null ? pharmacy.openingDays : pharmacyUpdateDto.openingDays;

            pharmacy.imageUrl = string.IsNullOrEmpty(pharmacyUpdateDto.imageUrl) ? pharmacy.imageUrl : pharmacyUpdateDto.imageUrl;
            pharmacy.document = string.IsNullOrEmpty(pharmacyUpdateDto.document) ? pharmacy.document : pharmacyUpdateDto.document;

            pharmacy.Account.Email = string.IsNullOrEmpty(pharmacyUpdateDto.Email) ? pharmacy.Account.Email : pharmacyUpdateDto.Email;
            pharmacy.Account.PhoneNumber = string.IsNullOrEmpty(pharmacyUpdateDto.PhoneNumber) ? pharmacy.Account.PhoneNumber : pharmacyUpdateDto.PhoneNumber;

            pharmacy.status = pharmacyUpdateDto.status;

            pharmacy.isFirstTime = false;

            var pharmacyUpdated = await _pharmacyRepository.UpdatePharmacyAsync(pharmacy);


            var pharmacyGetDto = _mapper.Map<PharmacyGetDto>(pharmacyUpdated);

            return Ok(pharmacyGetDto);
        }


        //Pharmacy/profile/ask

        [HttpPut("profile/ask")]
        public async Task<IActionResult> UpdatePharmacyRefillInfo([FromBody] PharmacyUpdateDto pharmacyUpdateDto)
        {
            // if (!ModelState.IsValid) return BadRequest(ModelState);

            //if (!string.IsNullOrEmpty(pharmacyUpdateDto.UserName) && await UsernameExists(pharmacyUpdateDto.UserName)) return BadRequest("Username is taken try other username");
            // if (!string.IsNullOrEmpty(pharmacyUpdateDto.Email) && await EmailExists(pharmacyUpdateDto.Email)) return BadRequest("Email is taken try other email");


            var pharmacy = await GetPharmacyProfile();
            if (pharmacy == null) return BadRequest("Pharmacy Doesn't Exist");

            //never
            //   pharmacy.Account.UserName = string.IsNullOrEmpty(pharmacyUpdateDto.UserName) ? pharmacy.Account.UserName : pharmacyUpdateDto.UserName;

            pharmacy.description = string.IsNullOrEmpty(pharmacyUpdateDto.description) ? pharmacy.description : pharmacyUpdateDto.description;
            pharmacy.name = string.IsNullOrEmpty(pharmacyUpdateDto.name) ? pharmacy.name : pharmacyUpdateDto.name;
            pharmacy.address = string.IsNullOrEmpty(pharmacyUpdateDto.address) ? pharmacy.address : pharmacyUpdateDto.address;
            pharmacy.lattitude = pharmacyUpdateDto.lattitude == 0 ? pharmacy.lattitude : pharmacyUpdateDto.lattitude;
            pharmacy.longitude = pharmacyUpdateDto.longitude == 0 ? pharmacy.longitude : pharmacyUpdateDto.longitude;

            pharmacy.openingHours = pharmacyUpdateDto.openingHours == TimeSpan.Zero ? pharmacy.openingHours : pharmacyUpdateDto.openingHours;
            pharmacy.closingHours = pharmacyUpdateDto.closingHours == TimeSpan.Zero ? pharmacy.closingHours : pharmacyUpdateDto.closingHours;

            pharmacy.openingDays = pharmacyUpdateDto.openingDays == null ? pharmacy.openingDays : pharmacyUpdateDto.openingDays;

            pharmacy.imageUrl = string.IsNullOrEmpty(pharmacyUpdateDto.imageUrl) ? pharmacy.imageUrl : pharmacyUpdateDto.imageUrl;
            pharmacy.document = string.IsNullOrEmpty(pharmacyUpdateDto.document) ? pharmacy.document : pharmacyUpdateDto.document;

            pharmacy.Account.Email = string.IsNullOrEmpty(pharmacyUpdateDto.Email) ? pharmacy.Account.Email : pharmacyUpdateDto.Email;
            pharmacy.Account.PhoneNumber = string.IsNullOrEmpty(pharmacyUpdateDto.PhoneNumber) ? pharmacy.Account.PhoneNumber : pharmacyUpdateDto.PhoneNumber;


            pharmacy.status = pharmacyUpdateDto.status == PharmacyStatus.Rejected ? PharmacyStatus.Idle : 0;

            pharmacy.isFirstTime = true;

            var pharmacyUpdated = await _pharmacyRepository.UpdatePharmacyAsync(pharmacy);


            var pharmacyGetDto = _mapper.Map<PharmacyGetDto>(pharmacyUpdated);

            return Ok(pharmacyGetDto);
        }



        [HttpDelete("profile")]
        public async Task<IActionResult> DeletePharmacyAccount()
        {
            return Ok("pharmacy profile deleted");
        }





        [HttpGet("medicines")]
        public async Task<IActionResult> GetPharmacyMedicines()
        {
            var pharmacy = await GetPharmacyProfile();
            var pharmacyMedicines = await _pharmacyRepository.GetMedicinesByPharmacyIdAsync(pharmacy!.PharmacyId);
            var pharmacyMedicinesGetDto = _mapper.Map<List<PharmacyMedicineGetDto>>(pharmacyMedicines);


            return Ok(pharmacyMedicinesGetDto);
        }




        [HttpPost("medicines")]
        public async Task<IActionResult> AddPharmacyMedicine([FromBody] List<PharmacyMedicineAddDto> pharmacyMedicineAddDtos)
        {
            if (pharmacyMedicineAddDtos == null || !pharmacyMedicineAddDtos.Any())
                return BadRequest("No data provided.");

            var results = new List<PharmacyMedicineGetDto>();
            var errors = new List<string>();

            foreach (var dto in pharmacyMedicineAddDtos)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var medicine = await _medicineRepository.GetMedicineByIdAsync(dto.MedicineId);

                if (medicine == null)
                {
                    errors.Add($"Medicine not found for ID {dto.MedicineId}");
                    continue;
                }

                var pharmacy = await GetPharmacyProfile();

                var totalNumberOfTab = dto.NumberOfPackage * dto.TabPerPackage;

                var pharmacyMedicine = new PharmacyMedicine()
                {
                    CostOfTab = dto.CostOfTab,
                    TotalNumberOfTab = totalNumberOfTab,
                    Pharmacy = pharmacy!,
                    Medicine = medicine,
                };

                var newPharmacyMedicine = await _pharmacyRepository.AddMedicineToPharmacyAsync(pharmacyMedicine);
                var pharmacyMedicineGetDto = _mapper.Map<PharmacyMedicineGetDto>(newPharmacyMedicine);
                results.Add(pharmacyMedicineGetDto);
            }

            if (errors.Any())
                return BadRequest(new { Errors = errors, Results = results });

            return Ok(results);
        }


        [HttpGet("medicines/{id}")]
        public async Task<IActionResult> GetSingleMedicines([FromRoute] Guid id)
        {

            var pharmacy = await GetPharmacyProfile();
            var pharmacyMedicine = await _pharmacyRepository.GetMedicineDetailInPharmacyAsync(pharmacy!.PharmacyId, id);
            if (pharmacyMedicine == null) return BadRequest("Medicine not found");
            var pharmacyMedicineGetDto = _mapper.Map<PharmacyMedicineGetDto>(pharmacyMedicine);

            return Ok(pharmacyMedicineGetDto);
        }


        [HttpPut("medicines/{id}")]
        public async Task<IActionResult> UpdateMedicine([FromRoute] Guid id, [FromBody] PharmacyMedicineUpdateDto pharmacyMedicineUpdateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var pharmacy = await GetPharmacyProfile();

            var pharmacyMedicine = await _pharmacyRepository.GetMedicineDetailInPharmacyAsync(pharmacy!.PharmacyId, id);
            if (pharmacyMedicine == null) return BadRequest("Medicine not found");

            pharmacyMedicine.CostOfTab = string.IsNullOrEmpty(pharmacyMedicineUpdateDto.CostOfTab.ToString()) ? pharmacyMedicine.CostOfTab : pharmacyMedicineUpdateDto.CostOfTab;
            pharmacyMedicine.TotalNumberOfTab = string.IsNullOrEmpty(pharmacyMedicineUpdateDto.NumberOfPackage.ToString()) ? pharmacyMedicine.TotalNumberOfTab : pharmacyMedicineUpdateDto.NumberOfPackage * pharmacyMedicineUpdateDto.TabPerPackage;
            pharmacyMedicine.TotalNumberOfTab = string.IsNullOrEmpty(pharmacyMedicineUpdateDto.TabPerPackage.ToString()) ? pharmacyMedicine.TotalNumberOfTab : pharmacyMedicineUpdateDto.NumberOfPackage * pharmacyMedicineUpdateDto.TabPerPackage;

            var updatedPharmacyMedicine = await _pharmacyRepository.UpdateMedicineInPharmacyAsync(pharmacyMedicine);
            var pharmacyMedicineGetDto = _mapper.Map<PharmacyMedicineGetDto>(updatedPharmacyMedicine);

            return Ok(pharmacyMedicineGetDto);



        }


        [HttpDelete("medicines/{id}")]
        public async Task<IActionResult> DeleteMedicine([FromRoute] Guid id)
        {
            var pharmacy = await GetPharmacyProfile();
            var pharmacyMedicine = await _pharmacyRepository.GetMedicineDetailInPharmacyAsync(pharmacy.PharmacyId, id);
            if (pharmacyMedicine == null) return BadRequest("medicine doesn't exist");
            await _pharmacyRepository.RemoveMedicineFromPharmacyAsync(pharmacyMedicine);
            var pharmacyMedicineGetDto = _mapper.Map<PharmacyMedicineGetDto>(pharmacyMedicine);
            return Ok(pharmacyMedicineGetDto);
        }






        [HttpGet("inventory")]
        public async Task<IActionResult> GetPharmacyInventory()
        {
            return Ok("Pharmacy Dashboard Data");
        }



        [HttpGet("prescriptions")]
        public async Task<IActionResult> GetPrescriptions()
        {
            var pharmacy = await GetPharmacyProfile();
            var prescriptions = await _prescriptionRepository.GetPrescriptionsByPharmacyIdAsync(pharmacy!.PharmacyId);
            var prescriptionsGetDto = _mapper.Map<List<PrescriptionGetDto>>(prescriptions);
            return Ok(prescriptionsGetDto);
        }


        [HttpPost("prescriptions")]
        public async Task<IActionResult> CreatePrescriptions([FromBody] PrescriptionBatchAddDto prescriptionBatchAddDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (prescriptionBatchAddDto.Prescriptions == null || !prescriptionBatchAddDto.Prescriptions.Any())
                return BadRequest("No prescriptions provided.");

            var results = new List<PrescriptionGetDto>();
            var errors = new List<string>();

            var pharmacy = await GetPharmacyProfile();
            var customer = await _customerRepository.GetCustomerByIdAsync(prescriptionBatchAddDto.CustomerId);

            if (customer == null)
            {
                return BadRequest("Customer not found");
            }

            foreach (var dto in prescriptionBatchAddDto.Prescriptions)
            {
                var pharmacyMedicine = await _pharmacyRepository.GetMedicineDetailInPharmacyAsync(pharmacy!.PharmacyId, dto.MedicineId);
                if (pharmacyMedicine == null)
                {
                    errors.Add($"Medicine not found for ID {dto.MedicineId}");
                    continue;
                }

                if (pharmacyMedicine.TotalNumberOfTab < dto.NumberOfTabsToPurchase)
                {
                    errors.Add($"Not enough tabs in the pharmacy for medicine ID {dto.MedicineId}");
                    continue;
                }

                var prescriptionModel = new Prescription
                {
                    Customer = customer,
                    pharmacyMedicine = pharmacyMedicine,
                    Dosage = dto.Dosage,
                    Notes = dto.Notes, // Assuming description maps to Notes
                    NumberOfTabsToPurchase = dto.NumberOfTabsToPurchase,
                };

                // Reduce the total quantity of tabs
                pharmacyMedicine.TotalNumberOfTab -= dto.NumberOfTabsToPurchase;
                await _pharmacyRepository.UpdateMedicineInPharmacyAsync(pharmacyMedicine);

                var newPrescription = await _prescriptionRepository.AddPrescriptionAsync(prescriptionModel);
                var prescriptionGetDto = _mapper.Map<PrescriptionGetDto>(newPrescription);
                results.Add(prescriptionGetDto);

                // Send SMS to the customer
                var message = $"Your medicine instruction has been created by {pharmacy.name}. Please visit the pharmacy for additional service.";
                var receipentNumber = customer.Account.PhoneNumber;
                if (receipentNumber.StartsWith("0"))
                {
                    receipentNumber = "+251" + receipentNumber.Substring(1);
                }
                await _smsService.SendSmsAsync(receipentNumber, message);
            }

            if (errors.Any())
                return BadRequest(new { Errors = errors, Results = results });

            return Ok(results);
        }

        //[HttpGet("reviews")]
        [HttpGet("reviews")]
        public async Task<IActionResult> GetPharmacyReviews()
        {
            var pharmacy = await GetPharmacyProfile();
            var reviews = await _pharmacyReviewRepository.GetPharmacyReviewsByPharmacyIdAsync(pharmacy!.PharmacyId);
            var reviewsDto = _mapper.Map<List<PharmacyReviewGetDto>>(reviews);
            return Ok(reviewsDto);
        }

        [HttpGet("reviews/{id}")]
        public async Task<IActionResult> GetPharmacyReviewById([FromRoute] Guid id)
        {
            var pharmacy = await GetPharmacyProfile();
            var review = await _pharmacyReviewRepository.GetPharamcyReviewByReviewIdAsync(id);
            var reviewDto = _mapper.Map<PharmacyReviewGetDto>(review);
            return Ok(reviewDto);
        }


        [HttpGet("dashboard")]
        public async Task<IActionResult> GetPharmacyDashboardData()
        {
            var pharmacy = await GetPharmacyProfile();
            var pharmacyMedicines = await _pharmacyRepository.GetMedicinesByPharmacyIdAsync(pharmacy!.PharmacyId);
            var prescriptions = await _prescriptionRepository.GetPrescriptionsByPharmacyIdAsync(pharmacy.PharmacyId);

            // total medicine in the pharmacy
            var totalMedicine = pharmacyMedicines.Count();
            var pharmacyMedicinesGetDto = _mapper.Map<List<PharmacyMedicineGetDto>>(pharmacyMedicines);
            // total revenue
            //var totalRevenue = prescriptions.Sum(x => x.pharmacyMedicine?.CostOfTab * x.NumberOfTabsToPurchase);
            var totalRevenue = prescriptions.Sum(x => x.pharmacyMedicine?.CostOfTab * x.NumberOfTabsToPurchase);

            var prescriptionsGetDto = _mapper.Map<List<PrescriptionGetDto>>(prescriptions);
            // shortage medicines
            var shortageMedicines = pharmacyMedicines.Where(x => x.TotalNumberOfTab < 50).ToList();
            var shortageMedicinesDto = _mapper.Map<List<PharmacyMedicineGetDto>>(shortageMedicines);

            var dashboardData = new
            {
                medicine = new
                {
                    totalMedicine,
                    medicines = pharmacyMedicinesGetDto,
                },
                revenue = new
                {
                    totalRevenue,
                    prescriptions = prescriptionsGetDto,
                },
                shortageMedicines = shortageMedicinesDto
            };

            return Ok(dashboardData);
        }


        [HttpGet("prescriptions/{id}")]
        public async Task<IActionResult> GetPrescription([FromRoute] string id)
        {
            return Ok("Prescription Detail");
        }



        [HttpPut("prescriptions/{id}")]
        public async Task<IActionResult> UpdatePrescription()
        {
            return Ok("Update Prescription");
        }


        [HttpDelete("prescriptions/{id}")]
        public async Task<IActionResult> DeletePrescription()
        {
            return Ok("Delete Prescription");
        }


        private async Task<bool> UsernameExists(string username)
        {
            return await _userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
        private async Task<bool> EmailExists(string email)
        {
            return await _userManager.Users.AnyAsync(x => x.Email == email.ToLower());
        }

        private async Task<Pharmacy?> GetPharmacyProfile()
        {
            var accountId = Guid.Parse(User.GetUserId());
            var pharmacy = await _accountRepository.GetPharmacyByAccountIdAsync(accountId);
            if (pharmacy == null) return null;
            return pharmacy;
        }

    }
}
