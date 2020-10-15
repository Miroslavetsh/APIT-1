using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseLayer.Migrations
{
    public partial class Development_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "ProfileAddress",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "ConferenceId",
                table: "Articles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UniqueAddress",
                table: "Articles",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Conferences",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UniqueAddress = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    ShortDescription = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateLastModified = table.Column<DateTime>(nullable: false),
                    DateStart = table.Column<DateTime>(nullable: false),
                    Duration = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conferences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConfAdmins",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConferenceId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfAdmins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfAdmins_Conferences_ConferenceId",
                        column: x => x.ConferenceId,
                        principalTable: "Conferences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ConfParticipants",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConferenceId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfParticipants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfParticipants_Conferences_ConferenceId",
                        column: x => x.ConferenceId,
                        principalTable: "Conferences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "769b263b-1048-4195-9937-9faf3a22e4f5", "76ebedea-14cb-4737-ae26-3c60a3acf5e7", null, null });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_ConferenceId",
                table: "Articles",
                column: "ConferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfAdmins_ConferenceId",
                table: "ConfAdmins",
                column: "ConferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfParticipants_ConferenceId",
                table: "ConfParticipants",
                column: "ConferenceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Conferences_ConferenceId",
                table: "Articles",
                column: "ConferenceId",
                principalTable: "Conferences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Conferences_ConferenceId",
                table: "Articles");

            migrationBuilder.DropTable(
                name: "ConfAdmins");

            migrationBuilder.DropTable(
                name: "ConfParticipants");

            migrationBuilder.DropTable(
                name: "Conferences");

            migrationBuilder.DropIndex(
                name: "IX_Articles_ConferenceId",
                table: "Articles");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "769b263b-1048-4195-9937-9faf3a22e4f5");

            migrationBuilder.DropColumn(
                name: "ProfileAddress",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ConferenceId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "UniqueAddress",
                table: "Articles");

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
    }
}
