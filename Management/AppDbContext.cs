using Management.Auth.Dto;
using Management.Extentions.Helpers;
using Management.Roles.Model;
using Management.Users.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace StudentWebApi
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() { }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Role>().HasData(
               new Role { Id = 1, Name = "Admin",CreatedBy="1" },
               new Role { Id = 2, Name = "User",CreatedBy = "1" });

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FirstName = "Kamal",
                    LastName = "Abdulov",
                    Email = "kamal@mail.ru",
                    Age = 29,
                    UserName = "neo",
                    Password = PasswordHelper.CreateMd5("Berbatov123@"),
                    IsDeleted = false,
                    CreatedOn = DateTime.UtcNow,
                    CreatedBy = "1",
                    UpdatedOn = DateTime.UtcNow,
                    UpdatedBy = "1",
                    RoleId = 1
                });
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;UserName=postgres;Password=12345;Database=Managament");
            }
        }


    }
}