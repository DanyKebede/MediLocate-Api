using AutoMapper;
using mediAPI.Abstractions.Interfaces;
using mediAPI.Dtos.Medicine;
using mediAPI.Dtos.Pharmacy;
using mediAPI.Models;
using MediLast.Abstractions.Interfaces;
using MediLast.Dtos.Medicine;
using MediLast.Dtos.PharmacyReview;
using MediLast.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace mediAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAccountRepository _accountRepository;
        private readonly IPharmacyRepository _pharmacyRepository;
        private readonly IPharmacyReviewRepository _pharmacyReviewRepository;
        private readonly IMedicineRepository _medicineRepository;
        private readonly IPrescriptionRepository _prescriptionRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly UserManager<Account> _userManager;

        public AdminController(IAccountRepository accountRepository, IMapper mapper, IPharmacyRepository pharmacyRepository, UserManager<Account> userManager,
            IMedicineRepository medicineRepository, ICustomerRepository customerRepository, IPharmacyReviewRepository pharmacyReviewRepository)
        {
            _mapper = mapper;
            _userManager = userManager;
            _accountRepository = accountRepository;
            _pharmacyRepository = pharmacyRepository;
            _medicineRepository = medicineRepository;
            _customerRepository = customerRepository;
            _pharmacyReviewRepository = pharmacyReviewRepository;
        }


        [HttpGet("profile")]
        public IActionResult GetAdminProfile()
        {
            return Ok("Admin Profile");
        }

        [HttpPut("profile")]
        public IActionResult UpdateAdminProfile()
        {
            return Ok("update Admin Profile");
        }

        [HttpDelete("profile")]
        public IActionResult DeleteAdminProfile()
        {
            return Ok("Delete Admin Profile");
        }

        [HttpGet("medicines")]
        public async Task<IActionResult> GetMedicines()
        {
            var medicines = await _medicineRepository.GetMedicinesAsync();
            var medicinesGetDto = _mapper.Map<List<MedicineDto>>(medicines);
            return Ok(medicinesGetDto);
        }

        [HttpGet("medicines/{id}")]
        public async Task<IActionResult> GetMedicineById([FromRoute] Guid id)
        {
            var medicine = await _medicineRepository.GetMedicineByIdAsync(id);
            var medicineDto = _mapper.Map<MedicineDto>(medicine);
            return Ok(medicineDto);
        }



        [HttpPost("medicines")]
        public async Task<IActionResult> CreateMedicines([FromBody] IEnumerable<MedicineAddDto> medicineAddDtos)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var medicines = _mapper.Map<IEnumerable<Medicine>>(medicineAddDtos);
            var newMedicines = new List<Medicine>();

            foreach (var medicine in medicines)
            {
                var newMedicine = await _medicineRepository.AddMedicineAsync(medicine);
                newMedicines.Add(newMedicine);
            }

            var medicineDtos = _mapper.Map<IEnumerable<MedicineDto>>(newMedicines);
            return Ok(medicineDtos);
        }

        [HttpPut("medicines/{id}")]
        public IActionResult UpdateMedicine([FromRoute] string id)
        {
            return Ok("update medicine");
        }

        [HttpDelete("medicines/{id}")]
        public IActionResult DeleteMedicine([FromRoute] string id)
        {
            return Ok("delete medicine");
        }

        [HttpPost("pharmacies")]
        public IActionResult AddPharmacy([FromRoute] string id)
        {
            return Ok("Create pharmacy");
        }

        [HttpGet("pharmacies")]
        public async Task<IActionResult> GetAllPharmacies()
        {
            var pharmacies = await _pharmacyRepository.GetPharmaciesAsync();
            var pharmaciesDto = _mapper.Map<List<PharmacyDto>>(pharmacies);
            return new JsonResult(pharmaciesDto) { StatusCode = 200 };
        }

        [HttpGet("pharmacies/{id}")]
        public async Task<IActionResult> GetPharmacyDetail([FromRoute] Guid id)
        {
            var pharmacy = await _pharmacyRepository.GetPharmacyByIdAsync(id);
            var pharmacyDto = _mapper.Map<PharmacyDto>(pharmacy);

            if (pharmacy == null) return new JsonResult(new { message = "Pharmacy Not Found" }) { StatusCode = 404 };
            return new JsonResult(pharmacyDto) { StatusCode = 200 };
        }

        [HttpGet("pharmacies/{id}/review")]
        public async Task<IActionResult> GetPharmacyReviews([FromRoute] Guid id)
        {
            var pharmacy = await _pharmacyRepository.GetPharmacyByIdAsync(id);
            if (pharmacy == null) return Ok("Pharmacy Not Found");
            var reviews = await _pharmacyReviewRepository.GetPharmacyReviewsByPharmacyIdAsync(pharmacy!.PharmacyId);
            var reviewsDto = _mapper.Map<List<PharmacyReviewGetDto>>(reviews);
            return Ok(reviewsDto);
        }

        [HttpGet("pharmacies/review/{reviewId}")]
        public async Task<IActionResult> GetPharmacyReviewById([FromRoute] Guid reviewId)
        {
            var review = await _pharmacyReviewRepository.GetPharamcyReviewByReviewIdAsync(reviewId);
            var reviewDto = _mapper.Map<PharmacyReviewGetDto>(review);
            return Ok(reviewDto);
        }


        [HttpPost("pharmacies/{id}/review")]
        public async Task<IActionResult> AddPharmacyReview([FromRoute] Guid id, [FromBody] PharmacyReviewAddDto pharmacyReviewAddDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var pharmacy = await _pharmacyRepository.GetPharmacyByIdAsync(id);
            if (pharmacy == null) return NotFound("Pharmacy not found");


            var pharmacyReview = _mapper.Map<PharmacyReview>(pharmacyReviewAddDto);
            pharmacyReview.PharmacyId = id;


            switch (pharmacyReviewAddDto.Decision)
            {
                case ReviewDecision.Approved:
                    pharmacy.status = PharmacyStatus.Approved;
                    break;
                case ReviewDecision.Rejected:
                    pharmacy.status = PharmacyStatus.Rejected;
                    break;
                default:
                    pharmacy.status = PharmacyStatus.OnProgress;
                    break;
            }



            await _pharmacyRepository.UpdatePharmacyStatusAsync(pharmacy);

            var newPharmacyReview = await _pharmacyReviewRepository.AddPharmacyReviewAsync(pharmacyReview);
            var pharmacyReviewGetDto = _mapper.Map<PharmacyReviewGetDto>(newPharmacyReview);
            return Ok(pharmacyReviewGetDto);

        }

        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboardData()
        {
            // get all pharmacies
            var pharmacies = await _pharmacyRepository.GetPharmaciesAsync();
            var customers = await _customerRepository.GetCustomersAsync();

            // get all medicines 
            var medicines = await _medicineRepository.GetMedicinesAsync();
            // return the count of each

            // return all values
            return Ok(new
            {
                pharmacies = pharmacies.ToArray(),
                customers = customers.ToList(),
                medicines = medicines.ToList()
            });

        }

    }
}
