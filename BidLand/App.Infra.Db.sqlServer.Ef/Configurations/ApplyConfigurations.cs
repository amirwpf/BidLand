

using App.Infra.Db.sqlServer.Ef.Configurations.EntitesConfiguration;
using Microsoft.EntityFrameworkCore;

namespace App.Infra.Db.sqlServer.Ef.Configurations;

public static class ApplyConfigurations
{
	public static void ApplyEntityConfigurations
			(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration(new AddressConfig());
		modelBuilder.ApplyConfiguration(new AuctionConfig());
		modelBuilder.ApplyConfiguration(new BidConfig());
		modelBuilder.ApplyConfiguration(new BoothConfig());
		modelBuilder.ApplyConfiguration(new BuyerConfig());
		modelBuilder.ApplyConfiguration(new CartConfig());
		modelBuilder.ApplyConfiguration(new CategoryConfig());
		modelBuilder.ApplyConfiguration(new CommentConfig());
		modelBuilder.ApplyConfiguration(new ImageConfig());
		modelBuilder.ApplyConfiguration(new ProductConfig());
		modelBuilder.ApplyConfiguration(new SellerConfig());
		modelBuilder.ApplyConfiguration(new StockConfig());
		modelBuilder.ApplyConfiguration(new StocksCartConfig());

	}
}
