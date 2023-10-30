using App.Domin.Core._02_Users.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Db.sqlServer.Ef.Configurations.EntitesConfiguration;

public class BuyerConfig : IEntityTypeConfiguration<Buyer>
{
	public void Configure(EntityTypeBuilder<Buyer> entity)
	{
		entity.HasKey(e => e.Id).HasName("PK__Buyer__4B81C1CA60F39982");

		entity.Property(e => e.Id).ValueGeneratedNever();
	}
}
