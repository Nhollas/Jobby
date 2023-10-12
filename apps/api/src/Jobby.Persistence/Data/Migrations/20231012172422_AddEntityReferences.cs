using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobby.Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddEntityReferences : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Boards_BoardId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Jobs_JobId",
                table: "Activities");

            migrationBuilder.AddColumn<string>(
                name: "Reference",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Reference",
                table: "JobLists",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Reference",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Reference",
                table: "Boards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Reference",
                table: "Activities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Boards_BoardId",
                table: "Activities",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Jobs_JobId",
                table: "Activities",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Boards_BoardId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Jobs_JobId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "Reference",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Reference",
                table: "JobLists");

            migrationBuilder.DropColumn(
                name: "Reference",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "Reference",
                table: "Boards");

            migrationBuilder.DropColumn(
                name: "Reference",
                table: "Activities");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Boards_BoardId",
                table: "Activities",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Jobs_JobId",
                table: "Activities",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
