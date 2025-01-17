﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OutOfOfficeApp.Entities;

#nullable disable

namespace OutOfOfficeApp.Migrations
{
    [DbContext(typeof(OOODbContext))]
    [Migration("20240714174742_Null allowance for ApprovalRequest, Employee, LeaveRequest, Project, User added")]
    partial class NullallowanceforApprovalRequestEmployeeLeaveRequestProjectUseradded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.31")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("OutOfOfficeApp.Entities.ApprovalRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("ApproverId")
                        .HasColumnType("int");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("LeaveRequestId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ApproverId");

                    b.HasIndex("LeaveRequestId")
                        .IsUnique()
                        .HasFilter("[LeaveRequestId] IS NOT NULL");

                    b.ToTable("ApprovalRequests");
                });

            modelBuilder.Entity("OutOfOfficeApp.Entities.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("OutOfOfficeBalance")
                        .HasColumnType("real");

                    b.Property<int?>("PeoplePartnerId")
                        .HasColumnType("int");

                    b.Property<string>("Photo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("Subdivision")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PeoplePartnerId");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasFilter("[UserId] IS NOT NULL");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("OutOfOfficeApp.Entities.EmployeeProject", b =>
                {
                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.HasKey("ProjectId", "EmployeeId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("EmployeeProject");
                });

            modelBuilder.Entity("OutOfOfficeApp.Entities.LeaveRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AbsenceReason")
                        .HasColumnType("int");

                    b.Property<int?>("ApprovalRequestId")
                        .HasColumnType("int");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<int?>("EmployeeId1")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("EmployeeId1");

                    b.ToTable("LeaveRequests");
                });

            modelBuilder.Entity("OutOfOfficeApp.Entities.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ProjectManager")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int>("ProjectType")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("OutOfOfficeApp.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("OutOfOfficeApp.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RoleId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("OutOfOfficeApp.Entities.ApprovalRequest", b =>
                {
                    b.HasOne("OutOfOfficeApp.Entities.Employee", "Approver")
                        .WithMany("ApprovalRequests")
                        .HasForeignKey("ApproverId");

                    b.HasOne("OutOfOfficeApp.Entities.LeaveRequest", "LeaveRequest")
                        .WithOne("ApprovalRequest")
                        .HasForeignKey("OutOfOfficeApp.Entities.ApprovalRequest", "LeaveRequestId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Approver");

                    b.Navigation("LeaveRequest");
                });

            modelBuilder.Entity("OutOfOfficeApp.Entities.Employee", b =>
                {
                    b.HasOne("OutOfOfficeApp.Entities.Employee", "PeoplePartner")
                        .WithMany()
                        .HasForeignKey("PeoplePartnerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("OutOfOfficeApp.Entities.User", "User")
                        .WithOne("Employee")
                        .HasForeignKey("OutOfOfficeApp.Entities.Employee", "UserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("PeoplePartner");

                    b.Navigation("User");
                });

            modelBuilder.Entity("OutOfOfficeApp.Entities.EmployeeProject", b =>
                {
                    b.HasOne("OutOfOfficeApp.Entities.Employee", "Employee")
                        .WithMany("EmployeeProjects")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OutOfOfficeApp.Entities.Project", "Project")
                        .WithMany("EmployeeProjects")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("OutOfOfficeApp.Entities.LeaveRequest", b =>
                {
                    b.HasOne("OutOfOfficeApp.Entities.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("OutOfOfficeApp.Entities.Employee", null)
                        .WithMany("LeaveRequests")
                        .HasForeignKey("EmployeeId1");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("OutOfOfficeApp.Entities.Project", b =>
                {
                    b.HasOne("OutOfOfficeApp.Entities.Employee", "Employee")
                        .WithMany("Projects")
                        .HasForeignKey("EmployeeId");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("OutOfOfficeApp.Entities.User", b =>
                {
                    b.HasOne("OutOfOfficeApp.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("OutOfOfficeApp.Entities.Employee", b =>
                {
                    b.Navigation("ApprovalRequests");

                    b.Navigation("EmployeeProjects");

                    b.Navigation("LeaveRequests");

                    b.Navigation("Projects");
                });

            modelBuilder.Entity("OutOfOfficeApp.Entities.LeaveRequest", b =>
                {
                    b.Navigation("ApprovalRequest");
                });

            modelBuilder.Entity("OutOfOfficeApp.Entities.Project", b =>
                {
                    b.Navigation("EmployeeProjects");
                });

            modelBuilder.Entity("OutOfOfficeApp.Entities.User", b =>
                {
                    b.Navigation("Employee");
                });
#pragma warning restore 612, 618
        }
    }
}
