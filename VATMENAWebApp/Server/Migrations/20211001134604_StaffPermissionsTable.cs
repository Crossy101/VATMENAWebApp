using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace VATMENAWebApp.Server.Migrations
{
    public partial class StaffPermissionsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StaffPermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    StaffPosition = table.Column<string>(type: "text", nullable: true),
                    SiteAdmin = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    RegionStaff = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DivisionStaff = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    SubDivisionStaff = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AccountPermissions = table.Column<int>(type: "int", nullable: false),
                    EventPermissions = table.Column<int>(type: "int", nullable: false),
                    RatingPermissions = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffPermissions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StaffPermissions");
        }
    }
}
