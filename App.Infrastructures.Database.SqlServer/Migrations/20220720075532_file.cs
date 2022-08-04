using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Infrastructures.Database.SqlServer.Migrations
{
    public partial class file : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThirdCategoryFiles_AppFile_AppFileId",
                table: "ThirdCategoryFiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppFile",
                table: "AppFile");

            migrationBuilder.RenameTable(
                name: "AppFile",
                newName: "AppFiles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppFiles",
                table: "AppFiles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ThirdCategoryFiles_AppFiles_AppFileId",
                table: "ThirdCategoryFiles",
                column: "AppFileId",
                principalTable: "AppFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThirdCategoryFiles_AppFiles_AppFileId",
                table: "ThirdCategoryFiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppFiles",
                table: "AppFiles");

            migrationBuilder.RenameTable(
                name: "AppFiles",
                newName: "AppFile");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppFile",
                table: "AppFile",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ThirdCategoryFiles_AppFile_AppFileId",
                table: "ThirdCategoryFiles",
                column: "AppFileId",
                principalTable: "AppFile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
