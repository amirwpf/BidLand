using App.Domin.Core._02_Users.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Db.sqlServer.Ef.Configurations.EntitesConfiguration;

public class SellerConfig : IEntityTypeConfiguration<Seller>
{
	public void Configure(EntityTypeBuilder<Seller> entity)
	{
		entity.HasKey(e => e.Id).HasName("PK__Seller__7FE3DBA13EC0B8EB");

		entity.Property(e => e.Id).ValueGeneratedNever();
	}
}
