

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using App.Domin.Core._03_Extras.Entities;
using System.Reflection.Emit;
using App.Domin.Core._01_Purchause.Entities;

namespace App.Infra.Db.sqlServer.Ef.Configurations.EntitesConfiguration;

public class BidConfig : IEntityTypeConfiguration<Bid>
{

	public void Configure(EntityTypeBuilder<Bid> entity)
	{
		//entity.HasOne(d => d.Buyer).WithMany(p => p.Bids).HasConstraintName("FK_Bids_Customer_CustomerId");

	}
}
