using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobby.Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class NewReferences : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BoardReference",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JobReference",
                table: "Activities",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BoardReference",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "JobReference",
                table: "Activities");
        }
    }
}
