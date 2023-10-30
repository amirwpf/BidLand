using App.Domin.Core._01_Purchause.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Db.sqlServer.Ef.Configurations.EntitesConfiguration;

public class StocksCartConfig : IEntityTypeConfiguration<StocksCart>
{
	public void Configure(EntityTypeBuilder<StocksCart> entity)
	{
		entity.HasOne(d => d.Cart).WithMany(p => p.StocksCarts)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK_ProductsInCart_ShoppingCart");

		entity.HasOne(d => d.Stock).WithMany(p => p.StocksCarts)
			.OnDelete(DeleteBehavior.ClientSetNull)
			.HasConstraintName("FK_StocksCarts_Stocks");
	}
}
