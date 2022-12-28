using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VetApi.Models;

namespace VetApi.Context
{
    public class AppDbContext : IdentityDbContext
    {
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Veterinarian> Veterinarians { get; set; }
        public DbSet<Tutor> Tutors { get; set; }
        public DbSet<QueryData> DatasOfTheDay { get; set; }
        public DbSet<Query> Querys { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Animals
            modelBuilder.Entity<Animal>()
                .Property(a => a.Name)
                .HasMaxLength(25);

            modelBuilder.Entity<Animal>()
                .Property(a => a.IdentificationCode)
                .HasMaxLength(20);

            modelBuilder.Entity<Animal>()
                .Property(a => a.Species)
                .HasMaxLength(15);

            modelBuilder.Entity<Animal>()
                .Property(a => a.Weigth)
                .HasPrecision(10, 2);

            // Tutors

            modelBuilder.Entity<Tutor>()
                .Property(a => a.SSN)
                .HasMaxLength(14);

            modelBuilder.Entity<Tutor>()
                .Property(a => a.Name)
                .HasMaxLength(25);

            // Veterinarian

            modelBuilder.Entity<Veterinarian>()
                .Property(a => a.SSN)
                .HasMaxLength(14);

            modelBuilder.Entity<Veterinarian>()
                .Property(a => a.Name)
                .HasMaxLength(25);

            modelBuilder.Entity<Veterinarian>()
                .Property(a => a.CRMV)
                .HasMaxLength(10);

            // Datas od the day

            modelBuilder.Entity<QueryData>()
                .Property(a => a.Weitgth)
                .HasPrecision(10, 2);

            modelBuilder.Entity<QueryData>()
                .Property(a => a.Temperature)
                .HasPrecision(10, 2);

            // Querys

            modelBuilder.Entity<Query>()
                .Property(a => a.Comments)
                .HasMaxLength(50);
            
            modelBuilder.Entity<Query>()
                .Property(a => a.Symptoms)
                .HasMaxLength(50);

            IdentityUser admin = new IdentityUser
            {
                UserName = "admin@vet.com",
                NormalizedUserName = "ADMIN@VET.COM",
                Email = "admin@vet.com",
                NormalizedEmail = "ADMIN@VET.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                Id = "adminIDAPI00213554856Pqwus"

            };

            PasswordHasher<IdentityUser> hasher = new PasswordHasher<IdentityUser>();
            admin.PasswordHash = hasher.HashPassword(admin, "Admin@123");
            modelBuilder.Entity<IdentityUser>().HasData(admin);

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "adminIDAPI00213554856Pqwus", Name = "admin", NormalizedName = "ADMIN" }
            );

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "regular123aosdm123bJASNd", Name = "regular", NormalizedName = "REGULAR" }
            );

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "vet125sasD31asdADA516as2da6A", Name = "vet", NormalizedName = "VET" }
            );

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { RoleId = "adminIDAPI00213554856Pqwus", UserId = admin.Id }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}