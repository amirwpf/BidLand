using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Domin.Core._01_Purchause.Entities;

namespace App.Infra.Db.sqlServer.Ef.Configurations.EntitesConfiguration
{
	public class AuctionConfig : IEntityTypeConfiguration<Auction>
	{
		public void Configure(EntityTypeBuilder<Auction> entity)
		{
			//entity.HasOne(d => d.Stock).WithOne(p => p.Auction).HasForeignKey<Stock>(x=>x.AuctionId);
		}
	}
}
