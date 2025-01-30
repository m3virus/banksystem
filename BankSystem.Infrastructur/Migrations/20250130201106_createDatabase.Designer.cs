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
    [Migration("20250130201106_createDatabase")]
    partial class createDatabase
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

                    b.HasKey("Id");

                    b.HasIndex("CustomerId")
                        .IsUnique();

                    b.ToTable("Accounts");
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

                    b.HasIndex("TransactionNumber")
                        .IsUnique();

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
                        .HasMaxLength(10)
                        .HasColumnType("varchar");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

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
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("NationalCode")
                        .IsUnique();

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("BankSystem.Domain.Models.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("Users");
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

            modelBuilder.Entity("BankSystem.Domain.Models.Entities.ChangeTracking", b =>
                {
                    b.HasOne("BankSystem.Domain.Models.Entities.User", "User")
                        .WithMany("ChangeTrackings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
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

            modelBuilder.Entity("BankSystem.Domain.Models.Entities.User", b =>
                {
                    b.Navigation("ChangeTrackings");
                });
#pragma warning restore 612, 618
        }
    }
}
