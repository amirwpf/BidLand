using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Repository.Models;

namespace WebApplication1.Repository;
//namespace App.Infra.Db.sqlServer.Ef.Context

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=MarketPlaceDb;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasIndex(e => e.Phone, "IX_Addresses_SellerId")
                .IsUnique()
                .HasFilter("([SellerId] IS NOT NULL)");

            entity.HasOne(d => d.Buyer).WithMany(p => p.Addresses).HasConstraintName("FK_Addresses_Customer_CustomerId");
        });

        modelBuilder.Entity<Bid>(entity =>
        {
            entity.HasOne(d => d.Buyer).WithMany(p => p.Bids).HasConstraintName("FK_Bids_Customer_CustomerId");
        });

        modelBuilder.Entity<Booth>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Booth__E2D0E1DD5CEB9CEA");

            entity.HasIndex(e => e.SellerId, "IX_Booths_SellerId")
                .IsUnique()
                .HasFilter("([SellerId] IS NOT NULL)");

            entity.HasOne(d => d.Seller).WithOne(p => p.Booth).OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<Buyer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Buyer__4B81C1CA60F39982");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Shopping__7A789A84E74B74AC");

            entity.HasOne(d => d.Buyer).WithMany(p => p.Carts).HasConstraintName("FK_Cart_Customer");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Category__19093A2B7D631E80");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent).HasConstraintName("FK_Categories_Categories");

            entity.HasMany(d => d.Products).WithMany(p => p.Categories)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductsCategory",
                    r => r.HasOne<Product>().WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ProductsI__Produ__4316F928"),
                    l => l.HasOne<Category>().WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ProductsI__Categ__4222D4EF"),
                    j =>
                    {
                        j.HasKey("CategoryId", "ProductId").HasName("PK__Products__D249F64504564426");
                        j.ToTable("ProductsCategory");
                        j.HasIndex(new[] { "ProductId" }, "IX_ProductsCategory_ProductId");
                    });
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasOne(d => d.Buyer).WithMany(p => p.Comments).HasConstraintName("FK_Comments_Customer_CustomerId");

            entity.HasOne(d => d.Stock).WithMany(p => p.Comments).HasConstraintName("FK_Comments_Stocks");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasOne(d => d.Product).WithMany(p => p.Images).HasConstraintName("FK_Image_Product");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Product__B40CC6EDE2FD57A1");
        });

        modelBuilder.Entity<Seller>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Seller__7FE3DBA13EC0B8EB");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Stock>(entity =>
        {
            entity.HasOne(d => d.Auction).WithMany(p => p.Stocks)
                .HasPrincipalKey(p => p.StockId)
                .HasForeignKey(d => d.AuctionId)
                .HasConstraintName("FK_Stocks_Auctions");

            entity.HasOne(d => d.Booth).WithMany(p => p.Stocks).HasConstraintName("FK_Stocks_Booths");

            entity.HasOne(d => d.Product).WithMany(p => p.Stocks).HasConstraintName("FK_Stocks_Product");
        });

        modelBuilder.Entity<StocksCart>(entity =>
        {
            entity.HasOne(d => d.Cart).WithMany(p => p.StocksCarts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductsInCart_ShoppingCart");

            entity.HasOne(d => d.Stock).WithMany(p => p.StocksCarts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StocksCarts_Stocks");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
