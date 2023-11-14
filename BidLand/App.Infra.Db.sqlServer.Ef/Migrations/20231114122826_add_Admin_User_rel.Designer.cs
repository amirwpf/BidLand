﻿// <auto-generated />
using System;
using App.Infra.Db.sqlServer.Ef.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace App.Infra.Db.sqlServer.Ef.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20231114122826_add_Admin_User_rel")]
    partial class add_Admin_User_rel
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("App.Domin.Core._01_Purchause.Entities.Auction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CurrentHighestPrice")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("InsertionDate")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<int>("MinimumFinalPrice")
                        .HasColumnType("int");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("StockId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StockId");

                    b.ToTable("Auctions");
                });

            modelBuilder.Entity("App.Domin.Core._01_Purchause.Entities.Bid", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AuctionId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("BidDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("BuyerId")
                        .HasColumnType("int");

                    b.Property<bool?>("HasWon")
                        .HasColumnType("bit");

                    b.Property<int?>("Price")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AuctionId");

                    b.HasIndex("BuyerId");

                    b.ToTable("Bids");
                });

            modelBuilder.Entity("App.Domin.Core._01_Purchause.Entities.Booth", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("InsertionDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SellerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SellerId")
                        .IsUnique()
                        .HasFilter("[SellerId] IS NOT NULL");

                    b.ToTable("Booths");
                });

            modelBuilder.Entity("App.Domin.Core._01_Purchause.Entities.Cart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BuyerId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("InsertionDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("PurchaseCompeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("PurchaseDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Value")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BuyerId");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("App.Domin.Core._01_Purchause.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("InsertionDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("App.Domin.Core._01_Purchause.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BasePrice")
                        .HasColumnType("int");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("InsertionDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsConfirm")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("App.Domin.Core._01_Purchause.Entities.Stock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AdditionalDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("AvailableNumber")
                        .HasColumnType("int");

                    b.Property<int?>("BoothId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("InsertionDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsAuction")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BoothId");

                    b.HasIndex("ProductId");

                    b.ToTable("Stocks");
                });

            modelBuilder.Entity("App.Domin.Core._01_Purchause.Entities.StocksCart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CartId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("InsertionDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("StockId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CartId");

                    b.HasIndex("StockId");

                    b.ToTable("StocksCarts");
                });

            modelBuilder.Entity("App.Domin.Core._02_Users.Entities.Admin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("InsertionDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("SiteCommissionIncome")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("App.Domin.Core._02_Users.Entities.Buyer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("Credit")
                        .HasColumnType("int");

                    b.Property<DateTime?>("InsertionDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsBan")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<int?>("TotalPurchaseAmount")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Buyers");
                });

            modelBuilder.Entity("App.Domin.Core._02_Users.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Desciption")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("App.Domin.Core._02_Users.Entities.Seller", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double?>("CommissionPercentage")
                        .HasColumnType("float");

                    b.Property<int?>("CommissionsAmount")
                        .HasColumnType("int");

                    b.Property<DateTime?>("InsertionDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsBan")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<int?>("SalesAmount")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Sellers");
                });

            modelBuilder.Entity("App.Domin.Core._02_Users.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("App.Domin.Core._03_Extras.Entities.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BuyerId")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("No")
                        .HasColumnType("int");

                    b.Property<int?>("Phone")
                        .HasColumnType("int");

                    b.Property<int?>("PostalCode")
                        .HasColumnType("int");

                    b.Property<string>("Province")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SellerId")
                        .HasColumnType("int");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BuyerId");

                    b.HasIndex("SellerId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("App.Domin.Core._03_Extras.Entities.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Advantages")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("BuyerId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ConfirmDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DisAdvantages")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsConfirm")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsPositive")
                        .HasColumnType("bit");

                    b.Property<int?>("StockId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BuyerId");

                    b.HasIndex("StockId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("App.Domin.Core._03_Extras.Entities.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("InsertionDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("App.Domin.Core._03_Extras.Entities.Medal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("InsertionDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("LevelType")
                        .HasColumnType("int");

                    b.Property<int?>("SellerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SellerId");

                    b.ToTable("Medals");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("App.Domin.Core._01_Purchause.Entities.Auction", b =>
                {
                    b.HasOne("App.Domin.Core._01_Purchause.Entities.Stock", "Stock")
                        .WithMany()
                        .HasForeignKey("StockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Stock");
                });

            modelBuilder.Entity("App.Domin.Core._01_Purchause.Entities.Bid", b =>
                {
                    b.HasOne("App.Domin.Core._01_Purchause.Entities.Auction", "Auction")
                        .WithMany("Bids")
                        .HasForeignKey("AuctionId");

                    b.HasOne("App.Domin.Core._02_Users.Entities.Buyer", "Buyer")
                        .WithMany("Bids")
                        .HasForeignKey("BuyerId");

                    b.Navigation("Auction");

                    b.Navigation("Buyer");
                });

            modelBuilder.Entity("App.Domin.Core._01_Purchause.Entities.Booth", b =>
                {
                    b.HasOne("App.Domin.Core._02_Users.Entities.Seller", "Seller")
                        .WithOne("Booth")
                        .HasForeignKey("App.Domin.Core._01_Purchause.Entities.Booth", "SellerId");

                    b.Navigation("Seller");
                });

            modelBuilder.Entity("App.Domin.Core._01_Purchause.Entities.Cart", b =>
                {
                    b.HasOne("App.Domin.Core._02_Users.Entities.Buyer", "Buyer")
                        .WithMany("Carts")
                        .HasForeignKey("BuyerId");

                    b.Navigation("Buyer");
                });

            modelBuilder.Entity("App.Domin.Core._01_Purchause.Entities.Category", b =>
                {
                    b.HasOne("App.Domin.Core._01_Purchause.Entities.Category", "Parent")
                        .WithMany("InverseParent")
                        .HasForeignKey("ParentId");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("App.Domin.Core._01_Purchause.Entities.Product", b =>
                {
                    b.HasOne("App.Domin.Core._01_Purchause.Entities.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("App.Domin.Core._01_Purchause.Entities.Stock", b =>
                {
                    b.HasOne("App.Domin.Core._01_Purchause.Entities.Booth", "Booth")
                        .WithMany("Stocks")
                        .HasForeignKey("BoothId");

                    b.HasOne("App.Domin.Core._01_Purchause.Entities.Product", "Product")
                        .WithMany("Stocks")
                        .HasForeignKey("ProductId");

                    b.Navigation("Booth");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("App.Domin.Core._01_Purchause.Entities.StocksCart", b =>
                {
                    b.HasOne("App.Domin.Core._01_Purchause.Entities.Cart", "Cart")
                        .WithMany("StocksCarts")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.Domin.Core._01_Purchause.Entities.Stock", "Stock")
                        .WithMany("StocksCarts")
                        .HasForeignKey("StockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cart");

                    b.Navigation("Stock");
                });

            modelBuilder.Entity("App.Domin.Core._02_Users.Entities.Admin", b =>
                {
                    b.HasOne("App.Domin.Core._02_Users.Entities.User", "User")
                        .WithOne("Admin")
                        .HasForeignKey("App.Domin.Core._02_Users.Entities.Admin", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("App.Domin.Core._02_Users.Entities.Buyer", b =>
                {
                    b.HasOne("App.Domin.Core._02_Users.Entities.User", "User")
                        .WithMany("Buyers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("App.Domin.Core._02_Users.Entities.Seller", b =>
                {
                    b.HasOne("App.Domin.Core._02_Users.Entities.User", "User")
                        .WithMany("Sellers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("App.Domin.Core._03_Extras.Entities.Address", b =>
                {
                    b.HasOne("App.Domin.Core._02_Users.Entities.Buyer", "Buyer")
                        .WithMany("Addresses")
                        .HasForeignKey("BuyerId");

                    b.HasOne("App.Domin.Core._02_Users.Entities.Seller", "Seller")
                        .WithMany("Addresses")
                        .HasForeignKey("SellerId");

                    b.Navigation("Buyer");

                    b.Navigation("Seller");
                });

            modelBuilder.Entity("App.Domin.Core._03_Extras.Entities.Comment", b =>
                {
                    b.HasOne("App.Domin.Core._02_Users.Entities.Buyer", "Buyer")
                        .WithMany("Comments")
                        .HasForeignKey("BuyerId");

                    b.HasOne("App.Domin.Core._01_Purchause.Entities.Stock", "Stock")
                        .WithMany("Comments")
                        .HasForeignKey("StockId");

                    b.Navigation("Buyer");

                    b.Navigation("Stock");
                });

            modelBuilder.Entity("App.Domin.Core._03_Extras.Entities.Image", b =>
                {
                    b.HasOne("App.Domin.Core._01_Purchause.Entities.Product", "Product")
                        .WithMany("Images")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Product");
                });

            modelBuilder.Entity("App.Domin.Core._03_Extras.Entities.Medal", b =>
                {
                    b.HasOne("App.Domin.Core._02_Users.Entities.Seller", "Seller")
                        .WithMany("Medals")
                        .HasForeignKey("SellerId");

                    b.Navigation("Seller");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("App.Domin.Core._02_Users.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("App.Domin.Core._02_Users.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("App.Domin.Core._02_Users.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("App.Domin.Core._02_Users.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.Domin.Core._02_Users.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("App.Domin.Core._02_Users.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("App.Domin.Core._01_Purchause.Entities.Auction", b =>
                {
                    b.Navigation("Bids");
                });

            modelBuilder.Entity("App.Domin.Core._01_Purchause.Entities.Booth", b =>
                {
                    b.Navigation("Stocks");
                });

            modelBuilder.Entity("App.Domin.Core._01_Purchause.Entities.Cart", b =>
                {
                    b.Navigation("StocksCarts");
                });

            modelBuilder.Entity("App.Domin.Core._01_Purchause.Entities.Category", b =>
                {
                    b.Navigation("InverseParent");

                    b.Navigation("Products");
                });

            modelBuilder.Entity("App.Domin.Core._01_Purchause.Entities.Product", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("Stocks");
                });

            modelBuilder.Entity("App.Domin.Core._01_Purchause.Entities.Stock", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("StocksCarts");
                });

            modelBuilder.Entity("App.Domin.Core._02_Users.Entities.Buyer", b =>
                {
                    b.Navigation("Addresses");

                    b.Navigation("Bids");

                    b.Navigation("Carts");

                    b.Navigation("Comments");
                });

            modelBuilder.Entity("App.Domin.Core._02_Users.Entities.Seller", b =>
                {
                    b.Navigation("Addresses");

                    b.Navigation("Booth");

                    b.Navigation("Medals");
                });

            modelBuilder.Entity("App.Domin.Core._02_Users.Entities.User", b =>
                {
                    b.Navigation("Admin")
                        .IsRequired();

                    b.Navigation("Buyers");

                    b.Navigation("Sellers");
                });
#pragma warning restore 612, 618
        }
    }
}
