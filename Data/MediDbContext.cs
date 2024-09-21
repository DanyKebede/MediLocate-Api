using mediAPI.Models;
using MediLast.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace mediAPI.Data
{
    public class MediDbContext : IdentityDbContext<Account, IdentityRole<Guid>, Guid>
    {
        public MediDbContext(DbContextOptions<MediDbContext> options) : base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Pharmacy> Pharmacies { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<PharmacyMedicine> PharmacyMedicines { get; set; }
        public DbSet<PharmacyReview> PharmacyReviews { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Message> Messages { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);



            // for many-to-many relationship of pharmacy and medicine 
            builder.Entity<PharmacyMedicine>()
            .HasKey(pm => new { pm.PharmacyId, pm.MedicineId });

            builder.Entity<PharmacyMedicine>()
                .HasOne(pm => pm.Pharmacy)
                .WithMany(p => p.PharmaciesMedicines)
                .HasForeignKey(pm => pm.PharmacyId);

            builder.Entity<PharmacyMedicine>()
                .HasOne(pm => pm.Medicine)
                .WithMany(m => m.PharmaciesMedicines)
                .HasForeignKey(pm => pm.MedicineId);

            builder.Entity<Message>()
                .HasOne(u => u.Recipient)
                .WithMany(m => m.MessagesReceived)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Message>()
                .HasOne(u => u.Sender)
                .WithMany(m => m.MessagesSent)
                .OnDelete(DeleteBehavior.Restrict);



            // for many-to-one relationship of prescription and pharmacy, customer and medicine

            builder.Entity<Account>(entity => { entity.ToTable("Accounts"); });

            builder.Entity<IdentityRole<Guid>>(entity => { entity.ToTable("Roles"); });
            builder.Entity<IdentityUserRole<Guid>>(entity => { entity.ToTable("AccountRoles"); });
            builder.Entity<IdentityUserClaim<Guid>>(entity => { entity.ToTable("AccountClaims"); });
            builder.Entity<IdentityUserLogin<Guid>>(entity => { entity.ToTable("AccountLogins"); });
            builder.Entity<IdentityUserToken<Guid>>(entity => { entity.ToTable("AccountTokens"); });
            builder.Entity<IdentityRoleClaim<Guid>>(entity => { entity.ToTable("RoleClaims"); });



            List<IdentityRole<Guid>> roles = new List<IdentityRole<Guid>>
            {
                new IdentityRole<Guid>
                {
                    Id = Guid.NewGuid(),
                    Name = "Customer",
                    NormalizedName = "CUSTOMER"
                },
                new IdentityRole<Guid>
                {
                    Id = Guid.NewGuid(),
                    Name = "Pharmacy",
                    NormalizedName = "PHARMACY"
                },
                  new IdentityRole<Guid>
                {
                    Id = Guid.NewGuid(),
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
            };

            builder.Entity<IdentityRole<Guid>>().HasData(roles);

        }
    }
}
