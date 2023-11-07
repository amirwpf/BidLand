using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Reflection.Emit;
using App.Domin.Core._01_Purchause.Entities;
using App.Domin.Core._02_Users.Entities;
using App.Domin.Core._03_Extras.Entities;
using App.Infra.Db.sqlServer.Ef.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace App.Infra.Db.sqlServer.Ef.Context;

public partial class AppDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
{
	public AppDbContext(DbContextOptions<AppDbContext> options)
		: base(options)
	{
	}

	#region Tables

	public virtual DbSet<Address> Addresses { get; set; }

	public virtual DbSet<Admin> Admins { get; set; }

	public virtual DbSet<Auction> Auctions { get; set; }

	public virtual DbSet<Bid> Bids { get; set; }

	public virtual DbSet<Booth> Booths { get; set; }

	public virtual DbSet<Buyer> Buyers { get; set; }

	public virtual DbSet<Cart> Carts { get; set; }

	public virtual DbSet<Category> Categories { get; set; }

	public virtual DbSet<Comment> Comments { get; set; }

	public virtual DbSet<Image> Images { get; set; }

	public virtual DbSet<Medal> Medals { get; set; }

	public virtual DbSet<Product> Products { get; set; }

	public virtual DbSet<Seller> Sellers { get; set; }

	public virtual DbSet<Stock> Stocks { get; set; }

	public virtual DbSet<StocksCart> StocksCarts { get; set; }


	#endregion

	#region connection

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
		=> optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=MarketPlaceDb;User Id=MarktetAdmin;Password=123456;Trusted_Connection=True;TrustServerCertificate=True;");


	#endregion

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{

	
		
		#region Config
		ApplyConfigurations.ApplyEntityConfigurations(modelBuilder);
		#endregion

		
		base.OnModelCreating(modelBuilder);

		//Seeding a  'Administrator' role to AspNetRoles table
		modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "2c5e174e-3b0e-446f-86af-483d56fd7210", Name = "Admin", NormalizedName = "admin".ToUpper() });

	}

	//partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
