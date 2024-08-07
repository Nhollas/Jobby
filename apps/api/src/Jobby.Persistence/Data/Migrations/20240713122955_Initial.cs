﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobby.Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Boards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boards", x => new { x.Id, x.Reference });
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Socials_TwitterUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Socials_FacebookUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Socials_LinkedInUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Socials_GithubUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BoardReference = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BoardId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => new { x.Id, x.Reference });
                    table.ForeignKey(
                        name: "FK_Contacts_Boards_BoardId_BoardReference",
                        columns: x => new { x.BoardId, x.BoardReference },
                        principalTable: "Boards",
                        principalColumns: new[] { "Id", "Reference" });
                });

            migrationBuilder.CreateTable(
                name: "JobLists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    BoardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BoardReference = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobLists", x => new { x.Id, x.Reference });
                    table.ForeignKey(
                        name: "FK_JobLists_Boards_BoardId_BoardReference",
                        columns: x => new { x.BoardId, x.BoardReference },
                        principalTable: "Boards",
                        principalColumns: new[] { "Id", "Reference" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContactReference = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => new { x.Id, x.Reference });
                    table.ForeignKey(
                        name: "FK_Company_Contacts_ContactId_ContactReference",
                        columns: x => new { x.ContactId, x.ContactReference },
                        principalTable: "Contacts",
                        principalColumns: new[] { "Id", "Reference" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Email",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    ContactId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContactReference = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Email", x => new { x.Id, x.Reference });
                    table.ForeignKey(
                        name: "FK_Email_Contacts_ContactId_ContactReference",
                        columns: x => new { x.ContactId, x.ContactReference },
                        principalTable: "Contacts",
                        principalColumns: new[] { "Id", "Reference" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Phone",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    ContactId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContactReference = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phone", x => new { x.Id, x.Reference });
                    table.ForeignKey(
                        name: "FK_Phone_Contacts_ContactId_ContactReference",
                        columns: x => new { x.ContactId, x.ContactReference },
                        principalTable: "Contacts",
                        principalColumns: new[] { "Id", "Reference" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salary = table.Column<double>(type: "float", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deadline = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Position = table.Column<int>(type: "int", nullable: false),
                    JobListId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobListReference = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BoardReference = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BoardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => new { x.Id, x.Reference });
                    table.ForeignKey(
                        name: "FK_Jobs_Boards_BoardId_BoardReference",
                        columns: x => new { x.BoardId, x.BoardReference },
                        principalTable: "Boards",
                        principalColumns: new[] { "Id", "Reference" });
                    table.ForeignKey(
                        name: "FK_Jobs_JobLists_JobListId_JobListReference",
                        columns: x => new { x.JobListId, x.JobListReference },
                        principalTable: "JobLists",
                        principalColumns: new[] { "Id", "Reference" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    EndDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Completed = table.Column<bool>(type: "bit", nullable: false),
                    BoardReference = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BoardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobReference = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    JobId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => new { x.Id, x.Reference });
                    table.ForeignKey(
                        name: "FK_Activities_Boards_BoardId_BoardReference",
                        columns: x => new { x.BoardId, x.BoardReference },
                        principalTable: "Boards",
                        principalColumns: new[] { "Id", "Reference" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Activities_Jobs_JobId_JobReference",
                        columns: x => new { x.JobId, x.JobReference },
                        principalTable: "Jobs",
                        principalColumns: new[] { "Id", "Reference" });
                });

            migrationBuilder.CreateTable(
                name: "JobContacts",
                columns: table => new
                {
                    JobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContactId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobReference = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ContactReference = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobContacts", x => new { x.ContactId, x.JobId });
                    table.ForeignKey(
                        name: "FK_JobContacts_Contacts_ContactId_ContactReference",
                        columns: x => new { x.ContactId, x.ContactReference },
                        principalTable: "Contacts",
                        principalColumns: new[] { "Id", "Reference" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobContacts_Jobs_JobId_JobReference",
                        columns: x => new { x.JobId, x.JobReference },
                        principalTable: "Jobs",
                        principalColumns: new[] { "Id", "Reference" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Note",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobReference = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    JobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Note", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Note_Jobs_JobId_JobReference",
                        columns: x => new { x.JobId, x.JobReference },
                        principalTable: "Jobs",
                        principalColumns: new[] { "Id", "Reference" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activities_BoardId_BoardReference",
                table: "Activities",
                columns: new[] { "BoardId", "BoardReference" });

            migrationBuilder.CreateIndex(
                name: "IX_Activities_JobId_JobReference",
                table: "Activities",
                columns: new[] { "JobId", "JobReference" });

            migrationBuilder.CreateIndex(
                name: "IX_Company_ContactId_ContactReference",
                table: "Company",
                columns: new[] { "ContactId", "ContactReference" });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_BoardId_BoardReference",
                table: "Contacts",
                columns: new[] { "BoardId", "BoardReference" });

            migrationBuilder.CreateIndex(
                name: "IX_Email_ContactId_ContactReference",
                table: "Email",
                columns: new[] { "ContactId", "ContactReference" });

            migrationBuilder.CreateIndex(
                name: "IX_JobContacts_ContactId_ContactReference",
                table: "JobContacts",
                columns: new[] { "ContactId", "ContactReference" });

            migrationBuilder.CreateIndex(
                name: "IX_JobContacts_JobId_JobReference",
                table: "JobContacts",
                columns: new[] { "JobId", "JobReference" });

            migrationBuilder.CreateIndex(
                name: "IX_JobLists_BoardId_BoardReference",
                table: "JobLists",
                columns: new[] { "BoardId", "BoardReference" });

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_BoardId_BoardReference",
                table: "Jobs",
                columns: new[] { "BoardId", "BoardReference" });

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_JobListId_JobListReference",
                table: "Jobs",
                columns: new[] { "JobListId", "JobListReference" });

            migrationBuilder.CreateIndex(
                name: "IX_Note_JobId_JobReference",
                table: "Note",
                columns: new[] { "JobId", "JobReference" });

            migrationBuilder.CreateIndex(
                name: "IX_Phone_ContactId_ContactReference",
                table: "Phone",
                columns: new[] { "ContactId", "ContactReference" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "Email");

            migrationBuilder.DropTable(
                name: "JobContacts");

            migrationBuilder.DropTable(
                name: "Note");

            migrationBuilder.DropTable(
                name: "Phone");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "JobLists");

            migrationBuilder.DropTable(
                name: "Boards");
        }
    }
}
