using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseLayer.Migrations
{
    public partial class Development_1B : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "2d3c10cf-1b7a-4908-ab0c-0ad017f7f6c2", "316655fc-f17e-4972-9d78-6fbe3b0a5a19" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "316655fc-f17e-4972-9d78-6fbe3b0a5a19");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2d3c10cf-1b7a-4908-ab0c-0ad017f7f6c2");

            migrationBuilder.AlterColumn<string>(
                name: "DataFile",
                table: "Articles",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "dab70346-9bc0-4229-a8fe-f6f556d0900f", "bec6d7b1-047e-47c2-b74a-f3b7fa2867be", null, null });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AcademicTitle", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "MiddleName", "NormalizedEmail", "NormalizedUserName", "ParticipationForm", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePhotoPath", "ScienceDegree", "SecurityStamp", "TwoFactorEnabled", "UserName", "WorkingFor" },
                values: new object[] { "9de855d3-1147-4233-8d1c-62723e5941a8", (short)1, 0, "1bbf778b-1880-4488-b4bb-12e63f265dfc", "test@test.com", true, "Big", "Boss", false, null, null, "TEST@TEST.COM", "ADMIN", (short)254, "AQAAAAEAACcQAAAAEFAeTk486vY/s6J52k4R2k1LCnbMCEXYjppfvvv3JdGmfcYHRQLQZhRI5Gac82kCHw==", null, false, null, (short)0, "5ebc436f-3f7d-40e1-bd9a-1a1c2b1aa16c", false, "admin", null });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "9de855d3-1147-4233-8d1c-62723e5941a8", "dab70346-9bc0-4229-a8fe-f6f556d0900f" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "9de855d3-1147-4233-8d1c-62723e5941a8", "dab70346-9bc0-4229-a8fe-f6f556d0900f" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dab70346-9bc0-4229-a8fe-f6f556d0900f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9de855d3-1147-4233-8d1c-62723e5941a8");

            migrationBuilder.AlterColumn<string>(
                name: "DataFile",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "316655fc-f17e-4972-9d78-6fbe3b0a5a19", "5ddf2067-c1c5-4838-a235-f33ac34793c0", null, null });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AcademicTitle", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "MiddleName", "NormalizedEmail", "NormalizedUserName", "ParticipationForm", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePhotoPath", "ScienceDegree", "SecurityStamp", "TwoFactorEnabled", "UserName", "WorkingFor" },
                values: new object[] { "2d3c10cf-1b7a-4908-ab0c-0ad017f7f6c2", (short)1, 0, "0edae5a2-abca-457d-b29c-350731e39d4e", "test@test.com", true, "Big", "Boss", false, null, null, "TEST@TEST.COM", "ADMIN", (short)254, "AQAAAAEAACcQAAAAEFZNQiLfmS7Q5+j6kce5ZHk4EaiTiNut5De/i4yjnD4/krK1L2dL1tVLC31wZd2nyw==", null, false, null, (short)0, "7405c3f4-4b33-4690-9774-b98348e2d843", false, "admin", null });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "2d3c10cf-1b7a-4908-ab0c-0ad017f7f6c2", "316655fc-f17e-4972-9d78-6fbe3b0a5a19" });
        }
    }
}
