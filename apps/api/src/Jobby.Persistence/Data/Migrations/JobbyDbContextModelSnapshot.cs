﻿// <auto-generated />
using System;
using Jobby.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Jobby.Persistence.Data.Migrations
{
    [DbContext(typeof(JobbyDbContext))]
    partial class JobbyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Jobby.Domain.Entities.Activity", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Reference")
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid>("BoardId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BoardReference")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Completed")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("JobId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("JobReference")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id", "Reference");

                    b.HasIndex("BoardId", "BoardReference");

                    b.HasIndex("JobId", "JobReference");

                    b.ToTable("Activities");
                });

            modelBuilder.Entity("Jobby.Domain.Entities.Board", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Reference")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id", "Reference");

                    b.ToTable("Boards");
                });

            modelBuilder.Entity("Jobby.Domain.Entities.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Reference")
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid>("ContactId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ContactReference")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id", "Reference");

                    b.HasIndex("ContactId", "ContactReference");

                    b.ToTable("Company");
                });

            modelBuilder.Entity("Jobby.Domain.Entities.Contact", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Reference")
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid?>("BoardId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BoardReference")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("JobTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id", "Reference");

                    b.HasIndex("BoardId", "BoardReference");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("Jobby.Domain.Entities.Email", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Reference")
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid>("ContactId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ContactReference")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id", "Reference");

                    b.HasIndex("ContactId", "ContactReference");

                    b.ToTable("Email");
                });

            modelBuilder.Entity("Jobby.Domain.Entities.Job", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Reference")
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid>("BoardId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BoardReference")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Company")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Deadline")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Index")
                        .HasColumnType("int");

                    b.Property<Guid>("JobListId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("JobListReference")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Salary")
                        .HasColumnType("float");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id", "Reference");

                    b.HasIndex("BoardId", "BoardReference");

                    b.HasIndex("JobListId", "JobListReference");

                    b.ToTable("Jobs");
                });

            modelBuilder.Entity("Jobby.Domain.Entities.JobContact", b =>
                {
                    b.Property<Guid>("ContactId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("JobId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ContactReference")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("JobReference")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ContactId", "JobId");

                    b.HasIndex("ContactId", "ContactReference");

                    b.HasIndex("JobId", "JobReference");

                    b.ToTable("JobContacts");
                });

            modelBuilder.Entity("Jobby.Domain.Entities.JobList", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Reference")
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid>("BoardId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BoardReference")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Index")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id", "Reference");

                    b.HasIndex("BoardId", "BoardReference");

                    b.ToTable("JobLists");
                });

            modelBuilder.Entity("Jobby.Domain.Entities.Note", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("JobId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("JobReference")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("JobId", "JobReference");

                    b.ToTable("Note");
                });

            modelBuilder.Entity("Jobby.Domain.Entities.Phone", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Reference")
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid>("ContactId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ContactReference")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Number")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id", "Reference");

                    b.HasIndex("ContactId", "ContactReference");

                    b.ToTable("Phone");
                });

            modelBuilder.Entity("Jobby.Domain.Entities.Activity", b =>
                {
                    b.HasOne("Jobby.Domain.Entities.Board", "Board")
                        .WithMany("Activities")
                        .HasForeignKey("BoardId", "BoardReference")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Jobby.Domain.Entities.Job", "Job")
                        .WithMany("Activities")
                        .HasForeignKey("JobId", "JobReference")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Board");

                    b.Navigation("Job");
                });

            modelBuilder.Entity("Jobby.Domain.Entities.Company", b =>
                {
                    b.HasOne("Jobby.Domain.Entities.Contact", "Contact")
                        .WithMany("Companies")
                        .HasForeignKey("ContactId", "ContactReference")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Contact");
                });

            modelBuilder.Entity("Jobby.Domain.Entities.Contact", b =>
                {
                    b.HasOne("Jobby.Domain.Entities.Board", "Board")
                        .WithMany("Contacts")
                        .HasForeignKey("BoardId", "BoardReference")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.OwnsOne("Jobby.Domain.Entities.Social", "Socials", b1 =>
                        {
                            b1.Property<Guid>("ContactId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("ContactReference")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("FacebookUrl")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("GithubUrl")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("LinkedInUrl")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("TwitterUrl")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("ContactId", "ContactReference");

                            b1.ToTable("Contacts");

                            b1.WithOwner()
                                .HasForeignKey("ContactId", "ContactReference");
                        });

                    b.Navigation("Board");

                    b.Navigation("Socials");
                });

            modelBuilder.Entity("Jobby.Domain.Entities.Email", b =>
                {
                    b.HasOne("Jobby.Domain.Entities.Contact", "Contact")
                        .WithMany("Emails")
                        .HasForeignKey("ContactId", "ContactReference")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Contact");
                });

            modelBuilder.Entity("Jobby.Domain.Entities.Job", b =>
                {
                    b.HasOne("Jobby.Domain.Entities.Board", "Board")
                        .WithMany("Jobs")
                        .HasForeignKey("BoardId", "BoardReference")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Jobby.Domain.Entities.JobList", "JobList")
                        .WithMany("Jobs")
                        .HasForeignKey("JobListId", "JobListReference")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Board");

                    b.Navigation("JobList");
                });

            modelBuilder.Entity("Jobby.Domain.Entities.JobContact", b =>
                {
                    b.HasOne("Jobby.Domain.Entities.Contact", "Contact")
                        .WithMany("JobContacts")
                        .HasForeignKey("ContactId", "ContactReference")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Jobby.Domain.Entities.Job", "Job")
                        .WithMany("JobContacts")
                        .HasForeignKey("JobId", "JobReference")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contact");

                    b.Navigation("Job");
                });

            modelBuilder.Entity("Jobby.Domain.Entities.JobList", b =>
                {
                    b.HasOne("Jobby.Domain.Entities.Board", "Board")
                        .WithMany("Lists")
                        .HasForeignKey("BoardId", "BoardReference")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Board");
                });

            modelBuilder.Entity("Jobby.Domain.Entities.Note", b =>
                {
                    b.HasOne("Jobby.Domain.Entities.Job", "Job")
                        .WithMany("Notes")
                        .HasForeignKey("JobId", "JobReference")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Job");
                });

            modelBuilder.Entity("Jobby.Domain.Entities.Phone", b =>
                {
                    b.HasOne("Jobby.Domain.Entities.Contact", "Contact")
                        .WithMany("Phones")
                        .HasForeignKey("ContactId", "ContactReference")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Contact");
                });

            modelBuilder.Entity("Jobby.Domain.Entities.Board", b =>
                {
                    b.Navigation("Activities");

                    b.Navigation("Contacts");

                    b.Navigation("Jobs");

                    b.Navigation("Lists");
                });

            modelBuilder.Entity("Jobby.Domain.Entities.Contact", b =>
                {
                    b.Navigation("Companies");

                    b.Navigation("Emails");

                    b.Navigation("JobContacts");

                    b.Navigation("Phones");
                });

            modelBuilder.Entity("Jobby.Domain.Entities.Job", b =>
                {
                    b.Navigation("Activities");

                    b.Navigation("JobContacts");

                    b.Navigation("Notes");
                });

            modelBuilder.Entity("Jobby.Domain.Entities.JobList", b =>
                {
                    b.Navigation("Jobs");
                });
#pragma warning restore 612, 618
        }
    }
}
