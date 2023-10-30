using App.Domin.Core._01_Purchause.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infra.Db.sqlServer.Ef.Configurations.EntitesConfiguration;

public class CategoryConfig : IEntityTypeConfiguration<Category>
{
	public void Configure(EntityTypeBuilder<Category> entity)
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
	}
}
