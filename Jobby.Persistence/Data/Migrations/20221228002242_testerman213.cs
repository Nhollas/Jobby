using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobby.Persistence.Data.Migrations
{
    public partial class testerman213 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ActivityType",
                table: "Activities",
                newName: "Type");

            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "Jobs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "JobLists",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Index",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Index",
                table: "JobLists");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Activities",
                newName: "ActivityType");
        }
    }
}
