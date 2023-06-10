﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ParkingApplication.DAL.Context;

#nullable disable

namespace ParkingApplication.DAL.Migrations
{
    [DbContext(typeof(DataBaseContext))]
    partial class DataBaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0-preview.4.23259.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ParkingApplication.DAL.Entities.Admin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(80)");

                    b.Property<bool>("IsSuperAdmin")
                        .HasColumnType("bit");

                    b.Property<int?>("ParkingTemplateId")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(300)");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.HasIndex("ParkingTemplateId");

                    b.ToTable("Admins");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "superadmin.parking@gmail.com",
                            IsSuperAdmin = true,
                            Password = "123"
                        },
                        new
                        {
                            Id = 2,
                            Email = "secret@gmail.com",
                            IsSuperAdmin = false,
                            Password = "123"
                        });
                });

            modelBuilder.Entity("ParkingApplication.DAL.Entities.Car", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("CarName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("CarNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(15)");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("OwnerId");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("ParkingApplication.DAL.Entities.Owner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.ToTable("Owners");
                });

            modelBuilder.Entity("ParkingApplication.DAL.Entities.Parking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CarId")
                        .HasColumnType("int");

                    b.Property<int>("FloorNumber")
                        .HasColumnType("int");

                    b.Property<int>("ParkingTemplateId")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("SlotNumber")
                        .HasColumnType("int");

                    b.Property<DateTime>("StandsUntil")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.HasIndex("ParkingTemplateId");

                    b.ToTable("Parking");
                });

            modelBuilder.Entity("ParkingApplication.DAL.Entities.ParkingTemplate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("FloorsCount")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("SlotsCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.ToTable("ParkingTemplates");
                });

            modelBuilder.Entity("ParkingApplication.DAL.Entities.Admin", b =>
                {
                    b.HasOne("ParkingApplication.DAL.Entities.ParkingTemplate", "ParkingTemplate")
                        .WithMany("Admins")
                        .HasForeignKey("ParkingTemplateId");

                    b.Navigation("ParkingTemplate");
                });

            modelBuilder.Entity("ParkingApplication.DAL.Entities.Car", b =>
                {
                    b.HasOne("ParkingApplication.DAL.Entities.Parking", "Parking")
                        .WithOne("Car")
                        .HasForeignKey("ParkingApplication.DAL.Entities.Car", "Id")
                        .HasPrincipalKey("ParkingApplication.DAL.Entities.Parking", "CarId")
                        .IsRequired();

                    b.HasOne("ParkingApplication.DAL.Entities.Owner", "Owner")
                        .WithMany("Cars")
                        .HasForeignKey("OwnerId")
                        .IsRequired();

                    b.Navigation("Owner");

                    b.Navigation("Parking");
                });

            modelBuilder.Entity("ParkingApplication.DAL.Entities.Parking", b =>
                {
                    b.HasOne("ParkingApplication.DAL.Entities.ParkingTemplate", "ParkingTemplate")
                        .WithMany("Parkings")
                        .HasForeignKey("ParkingTemplateId")
                        .IsRequired();

                    b.Navigation("ParkingTemplate");
                });

            modelBuilder.Entity("ParkingApplication.DAL.Entities.Owner", b =>
                {
                    b.Navigation("Cars");
                });

            modelBuilder.Entity("ParkingApplication.DAL.Entities.Parking", b =>
                {
                    b.Navigation("Car");
                });

            modelBuilder.Entity("ParkingApplication.DAL.Entities.ParkingTemplate", b =>
                {
                    b.Navigation("Admins");

                    b.Navigation("Parkings");
                });
#pragma warning restore 612, 618
        }
    }
}
