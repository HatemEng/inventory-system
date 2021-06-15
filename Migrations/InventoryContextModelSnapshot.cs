﻿// <auto-generated />
using System;
using Inventory.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Inventory.Migrations
{
    [DbContext(typeof(InventoryContext))]
    partial class InventoryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.6");

            modelBuilder.Entity("Inventory.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("longtext");

                    b.Property<string>("City")
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext");

                    b.Property<string>("Mobile")
                        .HasColumnType("longtext");

                    b.Property<string>("PharmacyName")
                        .HasColumnType("longtext");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "Baghdad",
                            City = "Baghdad",
                            Email = "test@test.com",
                            FirstName = "Ali",
                            LastName = "Ahamad",
                            Mobile = "07XXXXXXXXX",
                            PharmacyName = "T-Pharmacy",
                            UserId = 3
                        });
                });

            modelBuilder.Entity("Inventory.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext");

                    b.Property<string>("Gender")
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext");

                    b.Property<string>("Mobile")
                        .HasColumnType("longtext");

                    b.Property<string>("Role")
                        .HasColumnType("longtext");

                    b.Property<int>("Salary")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "Baghdad, Karada",
                            BirthDate = new DateTime(1996, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "test@test.com",
                            FirstName = "Hussain",
                            Gender = "Male",
                            LastName = "Nasir",
                            Mobile = "07XXXXXXXXX",
                            Role = "admin",
                            Salary = 1000,
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            Address = "Baghdad, Al-Sadir City",
                            BirthDate = new DateTime(1996, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "test@test.com",
                            FirstName = "Hatem",
                            Gender = "Male",
                            LastName = "karim",
                            Mobile = "07XXXXXXXXX",
                            Role = "admin",
                            Salary = 1000,
                            UserId = 2
                        });
                });

            modelBuilder.Entity("Inventory.Models.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Category")
                        .HasColumnType("longtext");

                    b.Property<string>("DangerousLevel")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Manufactures")
                        .HasColumnType("longtext");

                    b.Property<string>("Packing")
                        .HasColumnType("longtext");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("ScientificName")
                        .HasColumnType("longtext");

                    b.Property<string>("TradeName")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("Inventory.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsActive = true,
                            Name = "hussain",
                            Password = "5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5"
                        },
                        new
                        {
                            Id = 2,
                            IsActive = true,
                            Name = "hatem",
                            Password = "5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5"
                        },
                        new
                        {
                            Id = 3,
                            IsActive = true,
                            Name = "ali",
                            Password = "5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5"
                        });
                });

            modelBuilder.Entity("Inventory.Models.Customer", b =>
                {
                    b.HasOne("Inventory.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Inventory.Models.Employee", b =>
                {
                    b.HasOne("Inventory.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
