using AutoMapper;
using mediAPI.Dtos.Account;
using mediAPI.Dtos.Customer;
using mediAPI.Dtos.Medicine;
using mediAPI.Dtos.Pharmacy;
using mediAPI.Dtos.Prescription;
using mediAPI.Models;
using MediLast.Dtos.Account;
using MediLast.Dtos.Admin;
using MediLast.Dtos.Medicine;
using MediLast.Dtos.Message;
using MediLast.Dtos.PharmacyReview;
using MediLast.Models;

namespace mediAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {

            CreateMap<RegisterDto, Account>().ReverseMap();

            CreateMap<RegisterCustomerDto, Account>().ReverseMap();

            CreateMap<LoginDto, Account>().ReverseMap();


            // ADMIN DTO

            CreateMap<Admin, AdminGetDto>()
                .ForPath(dest => dest.Account.AccountId, opt => opt.MapFrom(src => src.Account.Id))
                .ForPath(dest => dest.Account.UserName, opt => opt.MapFrom(src => src.Account.UserName))
                .ForPath(dest => dest.Account.Email, opt => opt.MapFrom(src => src.Account.Email))
                .ForPath(dest => dest.Account.PhoneNumber, opt => opt.MapFrom(src => src.Account.PhoneNumber));

            // CUSTOMER DTO

            CreateMap<Customer, CustomerGetDto>()
                 .ForPath(dest => dest.Account.AccountId, opt => opt.MapFrom(src => src.Account.Id))
                 .ForPath(dest => dest.Account.UserName, opt => opt.MapFrom(src => src.Account.UserName))
                 .ForPath(dest => dest.Account.Email, opt => opt.MapFrom(src => src.Account.Email))
                 .ForPath(dest => dest.Account.PhoneNumber, opt => opt.MapFrom(src => src.Account.PhoneNumber));


            CreateMap<Customer, CustomerDto>()
              .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
              .ForMember(dest => dest.CustomerInfo, opt => opt.MapFrom(src => src.CustomerInfo))
              .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Account.UserName))
              .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Account.Email))
              .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Account.PhoneNumber));



            CreateMap<Customer, CustomerUpdateDto>().ReverseMap();


            // PHARMACY DTO
            CreateMap<Pharmacy, PharmacyGetDto>()
                   .ForPath(dest => dest.Account.AccountId, opt => opt.MapFrom(src => src.Account.Id))
                   .ForPath(dest => dest.Account.UserName, opt => opt.MapFrom(src => src.Account.UserName))
                   .ForPath(dest => dest.Account.Email, opt => opt.MapFrom(src => src.Account.Email))
                   .ForPath(dest => dest.Account.PhoneNumber, opt => opt.MapFrom(src => src.Account.PhoneNumber));



            CreateMap<Pharmacy, PharmacyDto>()
              .ForMember(dest => dest.PharmacyId, opt => opt.MapFrom(src => src.PharmacyId))
              .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.name))
              .ForMember(dest => dest.address, opt => opt.MapFrom(src => src.address))
              .ForMember(dest => dest.description, opt => opt.MapFrom(src => src.description))
              .ForMember(dest => dest.imageUrl, opt => opt.MapFrom(src => src.imageUrl))
              .ForMember(dest => dest.document, opt => opt.MapFrom(src => src.document))
              .ForMember(dest => dest.lattitude, opt => opt.MapFrom(src => src.lattitude))
              .ForMember(dest => dest.longitude, opt => opt.MapFrom(src => src.longitude))
              .ForMember(dest => dest.openingHours, opt => opt.MapFrom(src => src.openingHours))
              .ForMember(dest => dest.closingHours, opt => opt.MapFrom(src => src.closingHours))
              .ForMember(dest => dest.openingDays, opt => opt.MapFrom(src => src.openingDays))
              .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Account.UserName))
              .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Account.Email))
              .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Account.PhoneNumber))
              .ForMember(dest => dest.isFirstTime, opt => opt.MapFrom(src => src.isFirstTime))
              .ForMember(dest => dest.status, opt => opt.MapFrom(src => src.status));

            CreateMap<Pharmacy, PharmacyUpdateDto>().ReverseMap();




            // MEDICINE DTOS

            CreateMap<Medicine, MedicineDto>().ReverseMap();

            CreateMap<Medicine, MedicineAddDto>().ReverseMap();




            // PHARMACY MEDICINE DTOS

            CreateMap<PharmacyMedicine, PharmacyMedicineAddDto>().ReverseMap();

            CreateMap<PharmacyMedicine, PharmacyMedicineGetDto>()
                .ForPath(dest => dest.Medicine.MedicineId, opt => opt.MapFrom(src => src.Medicine.MedicineId))
                .ForPath(dest => dest.Medicine.Name, opt => opt.MapFrom(src => src.Medicine.Name))
                .ForPath(dest => dest.Medicine.Description, opt => opt.MapFrom(src => src.Medicine.Description))
                .ForPath(dest => dest.Medicine.SideEffects, opt => opt.MapFrom(src => src.Medicine.SideEffects))
                .ForPath(dest => dest.Medicine.Precautions, opt => opt.MapFrom(src => src.Medicine.Precautions))
                .ForPath(dest => dest.Medicine.ImageUrl, opt => opt.MapFrom(src => src.Medicine.ImageUrl))
                .ForPath(dest => dest.Pharmacy.PharmacyId, opt => opt.MapFrom(src => src.Pharmacy.PharmacyId))
                .ForPath(dest => dest.Pharmacy.UserName, opt => opt.MapFrom(src => src.Pharmacy.Account.UserName))
                .ForPath(dest => dest.Pharmacy.Email, opt => opt.MapFrom(src => src.Pharmacy.Account.Email))
                .ForPath(dest => dest.Pharmacy.PhoneNumber, opt => opt.MapFrom(src => src.Pharmacy.Account.PhoneNumber))
                .ForPath(dest => dest.Pharmacy.name, opt => opt.MapFrom(src => src.Pharmacy.name))
                .ForPath(dest => dest.Pharmacy.address, opt => opt.MapFrom(src => src.Pharmacy.address))
                .ForPath(dest => dest.Pharmacy.description, opt => opt.MapFrom(src => src.Pharmacy.description))
                .ForPath(dest => dest.Pharmacy.lattitude, opt => opt.MapFrom(src => src.Pharmacy.lattitude))
                .ForPath(dest => dest.Pharmacy.longitude, opt => opt.MapFrom(src => src.Pharmacy.longitude))
                .ForPath(dest => dest.Pharmacy.openingHours, opt => opt.MapFrom(src => src.Pharmacy.openingHours))
                .ForPath(dest => dest.Pharmacy.closingHours, opt => opt.MapFrom(src => src.Pharmacy.closingHours))
                .ForPath(dest => dest.Pharmacy.openingDays, opt => opt.MapFrom(src => src.Pharmacy.openingDays))
                .ForMember(dest => dest.CostOfTab, opt => opt.MapFrom(src => src.CostOfTab))
                .ForMember(dest => dest.TotalNumberOfTab, opt => opt.MapFrom(src => src.TotalNumberOfTab));

            // .ForMember(dest => dest.Cost, opt => opt.MapFrom(src => src.Cost));

            CreateMap<PharmacyMedicine, PharmacyMedicineUpdateDto>().ReverseMap();




            // Prescription Dto

            CreateMap<Prescription, PrescriptionGetDto>()
                .ForMember(dest => dest.PrescriptionId, opt => opt.MapFrom(src => src.PrescriptionId))

                .ForPath(dest => dest.Customer.CustomerId, opt => opt.MapFrom(src => src.Customer!.CustomerId))
                .ForPath(dest => dest.Customer.CustomerInfo, opt => opt.MapFrom(src => src.Customer!.CustomerInfo))
                .ForPath(dest => dest.Customer.UserName, opt => opt.MapFrom(src => src.Customer!.Account.UserName))
                .ForPath(dest => dest.Customer.Email, opt => opt.MapFrom(src => src.Customer!.Account.Email))
                .ForPath(dest => dest.Customer.PhoneNumber, opt => opt.MapFrom(src => src.Customer!.Account.PhoneNumber))

                .ForPath(dest => dest.pharmacyMedicine.Pharmacy.PharmacyId, opt => opt.MapFrom(src => src.pharmacyMedicine!.Pharmacy.PharmacyId))
                .ForPath(dest => dest.pharmacyMedicine.Pharmacy.UserName, opt => opt.MapFrom(src => src.pharmacyMedicine!.Pharmacy.Account.UserName))
                .ForPath(dest => dest.pharmacyMedicine.Pharmacy.Email, opt => opt.MapFrom(src => src.pharmacyMedicine!.Pharmacy.Account.Email))
                .ForPath(dest => dest.pharmacyMedicine.Pharmacy.PhoneNumber, opt => opt.MapFrom(src => src.pharmacyMedicine!.Pharmacy.Account.PhoneNumber))
                .ForPath(dest => dest.pharmacyMedicine.Pharmacy.description, opt => opt.MapFrom(src => src.pharmacyMedicine!.Pharmacy.description))
                .ForPath(dest => dest.pharmacyMedicine.Pharmacy.name, opt => opt.MapFrom(src => src.pharmacyMedicine!.Pharmacy.name))
                .ForPath(dest => dest.pharmacyMedicine.Pharmacy.address, opt => opt.MapFrom(src => src.pharmacyMedicine!.Pharmacy.address))
                .ForPath(dest => dest.pharmacyMedicine.Pharmacy.lattitude, opt => opt.MapFrom(src => src.pharmacyMedicine!.Pharmacy.lattitude))
                .ForPath(dest => dest.pharmacyMedicine.Pharmacy.longitude, opt => opt.MapFrom(src => src.pharmacyMedicine!.Pharmacy.longitude))
                .ForPath(dest => dest.pharmacyMedicine.Pharmacy.imageUrl, opt => opt.MapFrom(src => src.pharmacyMedicine!.Pharmacy.imageUrl))
                .ForPath(dest => dest.pharmacyMedicine.Pharmacy.openingHours, opt => opt.MapFrom(src => src.pharmacyMedicine!.Pharmacy.openingHours))
                .ForPath(dest => dest.pharmacyMedicine.Pharmacy.closingHours, opt => opt.MapFrom(src => src.pharmacyMedicine!.Pharmacy.closingHours))
                .ForPath(dest => dest.pharmacyMedicine.Pharmacy.openingDays, opt => opt.MapFrom(src => src.pharmacyMedicine!.Pharmacy.openingDays))

                .ForPath(dest => dest.pharmacyMedicine.Medicine.MedicineId, opt => opt.MapFrom(src => src.pharmacyMedicine!.Medicine.MedicineId))
                .ForPath(dest => dest.pharmacyMedicine.Medicine.Name, opt => opt.MapFrom(src => src.pharmacyMedicine!.Medicine.Name))
                .ForPath(dest => dest.pharmacyMedicine.Medicine.Description, opt => opt.MapFrom(src => src.pharmacyMedicine!.Medicine.Description))
                .ForPath(dest => dest.pharmacyMedicine.Medicine.SideEffects, opt => opt.MapFrom(src => src.pharmacyMedicine!.Medicine.SideEffects))
                .ForPath(dest => dest.pharmacyMedicine.Medicine.Precautions, opt => opt.MapFrom(src => src.pharmacyMedicine!.Medicine.Precautions))
                .ForPath(dest => dest.pharmacyMedicine.Medicine.ImageUrl, opt => opt.MapFrom(src => src.pharmacyMedicine!.Medicine.ImageUrl))

   // .ForPath(dest => dest.pharmacyMedicine.Cost, opt => opt.MapFrom(src => src.pharmacyMedicine!.Cost))
   .ForPath(dest => dest.pharmacyMedicine.CostOfTab, opt => opt.MapFrom(src => src.pharmacyMedicine!.CostOfTab))
                .ForPath(dest => dest.pharmacyMedicine.TotalNumberOfTab, opt => opt.MapFrom(src => src.pharmacyMedicine!.TotalNumberOfTab))


                .ForMember(dest => dest.Dosage, opt => opt.MapFrom(src => src.Dosage))
                .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes))
                   .ForMember(dest => dest.NumberOfTabsToPurchase, opt => opt.MapFrom(src => src.NumberOfTabsToPurchase));

            // Message Dto
            CreateMap<Message, MessageDto>();


            // PHARMACY REVIEW DTOS
            CreateMap<PharmacyReview, PharmacyReviewAddDto>().ReverseMap();

            CreateMap<PharmacyReview, PharmacyReviewGetDto>()
              .ForMember(dest => dest.PharmacyName, opt => opt.MapFrom(src => src.Pharmacy.name));





        }
    }
}
