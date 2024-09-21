using mediAPI.Abstractions.Interfaces;
using mediAPI.Abstractions.Repositories;
using mediAPI.Data;
using mediAPI.Helpers;
using mediAPI.Services;
using MediLast.Abstractions.Interfaces;
using MediLast.Abstractions.Repositories;
using MediLast.Services;
using Microsoft.EntityFrameworkCore;

namespace mediAPI.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationServiceExtension(this IServiceCollection services, IConfiguration config)
        {


            // add automapper service
            services.AddAutoMapper(typeof(AutoMapperProfiles));


            // add repository services

            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IPharmacyRepository, PharmacyRepository>();
            services.AddScoped<IMedicineRepository, MedicineRepository>();
            services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IPharmacyReviewRepository, PharmacyReviewRepository>();
            services.AddSingleton<IPresenceRepository, PresenceRepository>();

            services.AddHttpClient<ISmsService, SmsService>();

            // add json serializer settings to ignore reference loop handling
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            // create a service to inject db context for sqlserver 
            // create a service to inject db context for Postgres
            services.AddDbContext<MediDbContext>(options =>
            {
                options.UseNpgsql(config.GetConnectionString("DefaultConnection"));
            });

            //services.AddDbContext<MediDbContext>(options =>
            //   {
            //      options.UseSqlServer(config.GetConnectionString("SQLConnection"));
            //   });

            return services;
        }

    }
}
