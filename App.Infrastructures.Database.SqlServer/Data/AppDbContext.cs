using App.Domain.Core.ApplicationUserAgg.Entities;
using App.Domain.Core.BaseService.Entities;
using App.Domain.Core.ExpertAgg.Entities;
using App.Domain.Core.FileAgg.Entities;
using App.Domain.Core.OrderAgg.Entities;
using App.Domain.Core.SuggestionAgg.Entities;
using App.Infrastructures.Database.SqlServer.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructures.Database.SqlServer.Data
{
    public class AppDbContext:IdentityDbContext
    {
        public DbSet<MainCategory> MainCategories { get; set; }
        public DbSet<SecondaryCategory> SecondaryCategories { get; set; }
        public DbSet<ThirdCategory> ThirdCategories { get; set; }
        public DbSet<ThirdCategoryFile> ThirdCategoryFiles { get; set; }
        public DbSet<UserFile> UserFiles { get; set; }
        public DbSet<AppFile> AppFiles { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Suggestion> Suggestions { get; set; }
        public DbSet<ExpertSkill> ExpertSkills { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(ExpertSkillConfiguration).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MainCategory>().HasData(new MainCategory { Id = 1, Title = "لوازم خانگی", IsDeleted = false });
            modelBuilder.Entity<MainCategory>().HasData(new MainCategory { Id = 2, Title = " نظافت و بهداشت", IsDeleted = false });
            modelBuilder.Entity<MainCategory>().HasData(new MainCategory { Id = 3, Title = " خدمات اداری", IsDeleted = false });


            modelBuilder.Entity<SecondaryCategory>().HasData(new SecondaryCategory { Id = 1, Title = "لوازم آشپزخانه", MainCategoryId = 1, IsDeleted = false });
            modelBuilder.Entity<SecondaryCategory>().HasData(new SecondaryCategory { Id = 2, Title = "لوازم صوتی و تصویری", MainCategoryId = 1, IsDeleted = false });
            modelBuilder.Entity<SecondaryCategory>().HasData(new SecondaryCategory { Id = 3, Title = "خشکشویی و اتوشویی", MainCategoryId = 2, IsDeleted = false });
            modelBuilder.Entity<SecondaryCategory>().HasData(new SecondaryCategory { Id = 4, Title = "قالی شویی و مبل شویی", MainCategoryId = 2, IsDeleted = false });
            modelBuilder.Entity<SecondaryCategory>().HasData(new SecondaryCategory { Id = 5, Title = "ماشین اداری", MainCategoryId = 3, IsDeleted = false });
            modelBuilder.Entity<SecondaryCategory>().HasData(new SecondaryCategory { Id = 6, Title = "مبلمان اداری", MainCategoryId = 3, IsDeleted = false });


            modelBuilder.Entity<ThirdCategory>().HasData(new ThirdCategory { Id = 1, Title = "یخچال", Description = "استادکار با معرفی بهترین تعمیرکارهای یخچال در منزل، به شما کمک می‌کند تا با سرعت بیشتری بتوانید مشکل یخچال فریزر را حل کنید.", Price = 500000, SecondaryCategoryId = 1, IsDeleted = false });
            modelBuilder.Entity<ThirdCategory>().HasData(new ThirdCategory { Id = 2, Title = "ماشین ظرفشویی", Description = "ظرفشویی‌ها هم مثل تمامی وسایل برقی و مکانیکی خراب می‌شوند و نیاز به سرویس دارند. گاهی سر‌و‌صدای ظرفشویی و گاهی نقص در شستشوی ظروف باعث می‌شود برای تعمیر ظرفشویی آن اقدام کنیم.", Price = 400000, SecondaryCategoryId = 1, IsDeleted = false });




            this.SeedUsers(modelBuilder);
            this.SeedRoles(modelBuilder);
            this.SeedUserRoles(modelBuilder);

            
            
        }

        private void SeedUsers(ModelBuilder builder)
        {
            ApplicationUser user = new ApplicationUser()
            {
                Id = "b74ddd14-6340-4840-95c2-db12554843e5",
                UserName = "Admin",
                Email = "admin@gmail.com",
                EmailConfirmed=true,
                PhoneNumber = "09173229408",
                FirstName="Mahdi",
                Family="Parsa",
                NormalizedUserName = "ADMIN@GMAIL.COM"

            };

            PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = passwordHasher.HashPassword(user, "Parsa.itsu69");

            builder.Entity<ApplicationUser>().HasData(user);
        }

        private void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Id = "fab4fac1-c546-41de-aebc-a14da6895711", Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
                new IdentityRole() { Id = "c7b013f0-5201-4317-abd8-c211f91b7330", Name = "Customer", ConcurrencyStamp = "2", NormalizedName = "Customer" },
                new IdentityRole() { Id = "c7b013f0-5201-4317-abd8-c211f91b6540", Name = "Expert", ConcurrencyStamp = "3", NormalizedName = "Expert" },
                new IdentityRole() { Id = "c7b013f0-5201-4317-abd8-c211f91b6822", Name = "Normal", ConcurrencyStamp = "4", NormalizedName = "Normal" }
                );
        }

        private void SeedUserRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>() { RoleId = "fab4fac1-c546-41de-aebc-a14da6895711", UserId = "b74ddd14-6340-4840-95c2-db12554843e5" }
                );
        }
    }

}

