using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Infrastructures.Database.SqlServer.Migrations
{
    public partial class seedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "c7b013f0-5201-4317-abd8-c211f91b6540", "3", "Expert", "Expert" },
                    { "c7b013f0-5201-4317-abd8-c211f91b6822", "4", "Normal", "Normal" },
                    { "c7b013f0-5201-4317-abd8-c211f91b7330", "2", "Customer", "Customer" },
                    { "fab4fac1-c546-41de-aebc-a14da6895711", "1", "Admin", "Admin" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "Family", "FirstName", "FullAddress", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "b74ddd14-6340-4840-95c2-db12554843e5", 0, "d284b9fd-4dc4-4e9b-ad12-daa895d809b5", "ApplicationUser", "admin@gmail.com", false, "Parsa", "Mahdi", null, false, null, null, null, null, "09173229408", false, "16176599-5986-4202-aa11-cca05b700854", false, "Admin" });

            migrationBuilder.InsertData(
                table: "MainCategories",
                columns: new[] { "Id", "IsDeleted", "Title" },
                values: new object[,]
                {
                    { 1, false, "لوازم خانگی" },
                    { 2, false, " نظافت و بهداشت" },
                    { 3, false, " خدمات اداری" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "fab4fac1-c546-41de-aebc-a14da6895711", "b74ddd14-6340-4840-95c2-db12554843e5" });

            migrationBuilder.InsertData(
                table: "SecondaryCategories",
                columns: new[] { "Id", "IsDeleted", "MainCategoryId", "Title" },
                values: new object[,]
                {
                    { 1, false, 1, "لوازم آشپزخانه" },
                    { 2, false, 1, "لوازم صوتی و تصویری" },
                    { 3, false, 2, "خشکشویی و اتوشویی" },
                    { 4, false, 2, "قالی شویی و مبل شویی" },
                    { 5, false, 3, "ماشین اداری" },
                    { 6, false, 3, "مبلمان اداری" }
                });

            migrationBuilder.InsertData(
                table: "ThirdCategories",
                columns: new[] { "Id", "Description", "IsDeleted", "Price", "SecondaryCategoryId", "Title" },
                values: new object[] { 1, "استادکار با معرفی بهترین تعمیرکارهای یخچال در منزل، به شما کمک می‌کند تا با سرعت بیشتری بتوانید مشکل یخچال فریزر را حل کنید.", false, 500000L, 1, "یخچال" });

            migrationBuilder.InsertData(
                table: "ThirdCategories",
                columns: new[] { "Id", "Description", "IsDeleted", "Price", "SecondaryCategoryId", "Title" },
                values: new object[] { 2, "ظرفشویی‌ها هم مثل تمامی وسایل برقی و مکانیکی خراب می‌شوند و نیاز به سرویس دارند. گاهی سر‌و‌صدای ظرفشویی و گاهی نقص در شستشوی ظروف باعث می‌شود برای تعمیر ظرفشویی آن اقدام کنیم.", false, 400000L, 1, "ماشین ظرفشویی" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c7b013f0-5201-4317-abd8-c211f91b6540");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c7b013f0-5201-4317-abd8-c211f91b6822");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c7b013f0-5201-4317-abd8-c211f91b7330");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "fab4fac1-c546-41de-aebc-a14da6895711", "b74ddd14-6340-4840-95c2-db12554843e5" });

            migrationBuilder.DeleteData(
                table: "SecondaryCategories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SecondaryCategories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SecondaryCategories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "SecondaryCategories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "SecondaryCategories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ThirdCategories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ThirdCategories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fab4fac1-c546-41de-aebc-a14da6895711");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5");

            migrationBuilder.DeleteData(
                table: "MainCategories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MainCategories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SecondaryCategories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MainCategories",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
