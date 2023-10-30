using App.Domin.Core._01_Purchause.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Db.sqlServer.Ef.Configurations.EntitesConfiguration;

public class StockConfig : IEntityTypeConfiguration<Stock>
{
	public void Configure(EntityTypeBuilder<Stock> entity)
	{
		entity.HasOne(d => d.Auction).WithMany(p => p.Stocks)
				.HasPrincipalKey(p => p.StockId)
				.HasForeignKey(d => d.AuctionId)
				.HasConstraintName("FK_Stocks_Auctions");

		entity.HasOne(d => d.Booth).WithMany(p => p.Stocks).HasConstraintName("FK_Stocks_Booths");

		entity.HasOne(d => d.Product).WithMany(p => p.Stocks).HasConstraintName("FK_Stocks_Product");
	}
}
