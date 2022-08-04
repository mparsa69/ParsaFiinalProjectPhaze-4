using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Infrastructures.Database.SqlServer.Migrations
{
    public partial class seed2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "926ddc1a-24ca-4e23-8138-641163a6acff", true, "ADMIN@GMAIL.COM", "AQAAAAEAACcQAAAAEARw1J9g5Lq4Gh9CeP2URCJqRpTU3qYolKKVA3TjfE8I/iZRnBdcDC21aU9B8z9b/Q==", "cc0002ec-3ffe-4d19-924f-65f973e4f713" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d284b9fd-4dc4-4e9b-ad12-daa895d809b5", false, null, null, "16176599-5986-4202-aa11-cca05b700854" });

        }
    }
}
