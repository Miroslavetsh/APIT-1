using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseLayer.Migrations
{
    public partial class Development_3c : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d873ec31-3b4e-4ec0-924d-9a7fa59c6e70");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c1915b23-c4d0-470d-b30f-c0aec168f7db", "cfdabefe-0e14-4066-b47e-c8971864085b", "admin", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c1915b23-c4d0-470d-b30f-c0aec168f7db");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d873ec31-3b4e-4ec0-924d-9a7fa59c6e70", "afc42589-dd7b-46a1-88b3-f3a4999dc618", "admin", null });
        }
    }
}
