using App.Domin.Core._01_Purchause.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Db.sqlServer.Ef.Configurations.EntitesConfiguration;

public class ProductConfig : IEntityTypeConfiguration<Product>
{
	public void Configure(EntityTypeBuilder<Product> entity)
	{
		//entity.HasKey(e => e.Id).HasName("PK__Product__B40CC6EDE2FD57A1");

		//entity.HasOne(d => d.Category).WithMany(p => p.Products).HasConstraintName("FK_Product_Categories");
	}
}
