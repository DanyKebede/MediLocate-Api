using AutoMapper;
using mediAPI.Abstractions.Interfaces;
using mediAPI.Dtos.Customer;
using mediAPI.Dtos.Medicine;
using mediAPI.Dtos.Pharmacy;
using Microsoft.AspNetCore.Mvc;

namespace mediAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMedicineRepository _medicineRepository;
        private readonly IPharmacyRepository _pharmacyRepository;
        private readonly ICustomerRepository _customerRepository;

        public PublicController(IMedicineRepository medicineRepository, IPharmacyRepository pharmacyRepository, IMapper mapper,
            ICustomerRepository customerRepository)
        {
            _medicineRepository = medicineRepository;
            _pharmacyRepository = pharmacyRepository;
            _mapper = mapper;
            _customerRepository = customerRepository;
        }


        [HttpGet("medicines")]
        public async Task<IActionResult> GetAllMedicines()
        {
            var medicines = await _medicineRepository.GetMedicinesAsync();
            var medicinesGetDto = _mapper.Map<List<MedicineDto>>(medicines);
            return Ok(medicinesGetDto);
        }

        [HttpGet("medicines/search")]
        public async Task<IActionResult> SearchMedicine()
        {
            return Ok("List all medicines");
        }


        [HttpGet("medicines/{id}")]
        public async Task<IActionResult> GetMedicineById([FromRoute] Guid id)
        {
            return Ok("get medicine detail");
        }


        [HttpGet("medicines/{id}/pharmacies")]
        public async Task<IActionResult> GetPharmaciesWithMedicineId([FromRoute] Guid id)
        {
            var pharmacies = await _pharmacyRepository.GetPharamciesByMedicineId(id);
            var pharmacyMedicineGetDto = _mapper.Map<List<PharmacyMedicineGetDto>>(pharmacies);
            return new JsonResult(pharmacyMedicineGetDto) { StatusCode = 200 };
        }



        [HttpGet("pharmacies")]
        public async Task<IActionResult> GetAllPharmacies()
        {
            var pharmacies = await _pharmacyRepository.GetPharmaciesAsync();
            var approvedPharmacies = pharmacies.Where(p => p.status == Models.PharmacyStatus.Approved).ToList();
            var pharmaciesDto = _mapper.Map<List<PharmacyDto>>(approvedPharmacies);
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



        [HttpGet("pharmacies/{id}/medicines")]
        public async Task<IActionResult> GetPharmacyMedicines([FromRoute] Guid id)
        {
            var pharmacyMedicines = await _pharmacyRepository.GetMedicinesByPharmacyIdAsync(id);
            var pharmacyMedicinesGetDto = _mapper.Map<List<PharmacyMedicineGetDto>>(pharmacyMedicines);
            return Ok(pharmacyMedicinesGetDto);

        }



        [HttpGet("customers")]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _customerRepository.GetCustomersAsync();
            var customerDto = _mapper.Map<List<CustomerDto>>(customers);
            return Ok(customerDto);
        }

        [HttpGet("customers/{id}")]
        public async Task<IActionResult> GetCustomerDetail([FromRoute] Guid id)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(id);
            var customerDto = _mapper.Map<CustomerDto>(customer);
            return Ok(customerDto);
        }

    }
}
