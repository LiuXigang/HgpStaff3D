using Microsoft.EntityFrameworkCore.Migrations;

namespace HgpStaff3D.Migrations
{
    public partial class DepartmentCapUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CapUpdated",
                table: "Departments",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CapUpdated",
                table: "Departments");
        }
    }
}
