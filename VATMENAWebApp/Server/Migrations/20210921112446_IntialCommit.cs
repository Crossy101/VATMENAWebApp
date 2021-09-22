using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VATMENAWebApp.Server.Migrations
{
    public partial class IntialCommit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PersonalDetails",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(767)", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    FullName = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Country = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserPermissions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(767)", nullable: false),
                    IsAdmin = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AdminEvents = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPermissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VatsimDivisions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    ParentRegion = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true),
                    SubDivisionAllowed = table.Column<int>(type: "int", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VatsimDivisions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VatsimMembers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    PilotRating = table.Column<int>(type: "int", maxLength: 3, nullable: false),
                    Susp_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    Reg_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    Region = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true),
                    Division = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true),
                    SubDivision = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: true),
                    LastRatingChange = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VatsimMembers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VatsimRegions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Director = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VatsimRegions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VatsimSubDivisions",
                columns: table => new
                {
                    Code = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    Fullname = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    ParentDivision = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VatsimSubDivisions", x => x.Code);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonalDetails");

            migrationBuilder.DropTable(
                name: "UserPermissions");

            migrationBuilder.DropTable(
                name: "VatsimDivisions");

            migrationBuilder.DropTable(
                name: "VatsimMembers");

            migrationBuilder.DropTable(
                name: "VatsimRegions");

            migrationBuilder.DropTable(
                name: "VatsimSubDivisions");
        }
    }
}
