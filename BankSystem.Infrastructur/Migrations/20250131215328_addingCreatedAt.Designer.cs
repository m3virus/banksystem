﻿// <auto-generated />
using System;
using BankSystem.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BankSystem.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250131215328_addingCreatedAt")]
    partial class addingCreatedAt
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BankSystem.Domain.Models.Entities.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("AccountBalance")
                        .HasColumnType("float");

                    b.Property<string>("AccountNumber")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("varchar");

                    b.Property<int>("AccountStatus")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("PersianCreatedAt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId")
                        .IsUnique();

                    b.ToTable("Accounts");

                    b.HasData(
                        new
                        {
                            Id = new Guid("debd3920-aadb-4d07-9b19-1f9647823a46"),
                            AccountBalance = 0.0,
                            AccountNumber = "11111111111111",
                            AccountStatus = 1,
                            CreatedAt = new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            CustomerId = new Guid("76131e9f-6183-41ad-b3a3-9d6cdccc468d"),
                            IsDeleted = false,
                            PersianCreatedAt = "1403/11/13 00:00:00"
                        });
                });

            modelBuilder.Entity("BankSystem.Domain.Models.Entities.BankTransaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("DestinationAccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid?>("OriginAccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PersianCreatedAt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TransactionEnum")
                        .HasColumnType("int");

                    b.Property<string>("TransactionNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<double>("TransactionValue")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("DestinationAccountId");

                    b.HasIndex("OriginAccountId");

                    b.ToTable("BankTransactions");
                });

            modelBuilder.Entity("BankSystem.Domain.Models.Entities.ChangeTracking", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Entity")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("PersianCreatedAt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar");

                    b.HasKey("Id");

                    b.ToTable("ChangeTrackings");
                });

            modelBuilder.Entity("BankSystem.Domain.Models.Entities.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("NationalCode")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.Property<string>("PersianCreatedAt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<string>("PostCode")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<int>("UserType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("NationalCode")
                        .IsUnique();

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            Id = new Guid("76131e9f-6183-41ad-b3a3-9d6cdccc468d"),
                            AccountId = new Guid("debd3920-aadb-4d07-9b19-1f9647823a46"),
                            Address = "Iran-Tehran",
                            BirthDate = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            CreatedAt = new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            IsDeleted = false,
                            Name = "Bank-Mohaymen",
                            NationalCode = "1234567890",
                            PersianCreatedAt = "1403/11/13 00:00:00",
                            PhoneNumber = "09123456789",
                            PostCode = "1234567899",
                            UserType = 2
                        });
                });

            modelBuilder.Entity("BankSystem.Domain.Models.Entities.Account", b =>
                {
                    b.HasOne("BankSystem.Domain.Models.Entities.Customer", "Customer")
                        .WithOne("Account")
                        .HasForeignKey("BankSystem.Domain.Models.Entities.Account", "CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("BankSystem.Domain.Models.Entities.BankTransaction", b =>
                {
                    b.HasOne("BankSystem.Domain.Models.Entities.Account", "DestinationAccount")
                        .WithMany("TransactionsAsDestination")
                        .HasForeignKey("DestinationAccountId");

                    b.HasOne("BankSystem.Domain.Models.Entities.Account", "OriginAccount")
                        .WithMany("TransactionsAsOrigin")
                        .HasForeignKey("OriginAccountId");

                    b.Navigation("DestinationAccount");

                    b.Navigation("OriginAccount");
                });

            modelBuilder.Entity("BankSystem.Domain.Models.Entities.Account", b =>
                {
                    b.Navigation("TransactionsAsDestination");

                    b.Navigation("TransactionsAsOrigin");
                });

            modelBuilder.Entity("BankSystem.Domain.Models.Entities.Customer", b =>
                {
                    b.Navigation("Account")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
