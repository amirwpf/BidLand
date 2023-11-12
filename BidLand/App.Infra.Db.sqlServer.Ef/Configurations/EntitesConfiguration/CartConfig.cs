

using App.Domin.Core._01_Purchause.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infra.Db.sqlServer.Ef.Configurations.EntitesConfiguration;

public class CartConfig : IEntityTypeConfiguration<Cart>
{
	public void Configure(EntityTypeBuilder<Cart> entity)
	{
		//entity.HasKey(e => e.Id).HasName("PK__Shopping__7A789A84E74B74AC");

		//entity.HasOne(d => d.Buyer).WithMany(p => p.Carts).HasConstraintName("FK_Cart_Customer");
	}
}
