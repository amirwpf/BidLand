using App.Domin.Core._01_Purchause.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Db.sqlServer.Ef.Configurations.EntitesConfiguration;

public class BoothConfig : IEntityTypeConfiguration<Booth>
{
	public void Configure(EntityTypeBuilder<Booth> entity)
	{
		//entity.HasKey(e => e.Id).HasName("PK__Booth__E2D0E1DD5CEB9CEA");

		//entity.HasIndex(e => e.SellerId, "IX_Booths_SellerId")
		//	.IsUnique()
		//	.HasFilter("([SellerId] IS NOT NULL)");

		//entity.HasOne(d => d.Seller).WithOne(p => p.Booth).OnDelete(DeleteBehavior.SetNull);
	}
}
