

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using App.Domin.Core._03_Extras.Entities;
using System.Reflection.Emit;

namespace App.Infra.Db.sqlServer.Ef.Configurations.EntitesConfiguration;

public class AddressConfig : IEntityTypeConfiguration<Address>
{
	public void Configure(EntityTypeBuilder<Address> entity)
	{

		entity.HasIndex(e => e.Phone, "IX_Addresses_SellerId")
			.IsUnique()
			.HasFilter("([SellerId] IS NOT NULL)");

		entity.HasOne(d => d.Buyer).WithMany(p => p.Addresses).HasConstraintName("FK_Addresses_Customer_CustomerId");


	}
}
