using App.Domin.Core._03_Extras.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Db.sqlServer.Ef.Configurations.EntitesConfiguration;

public class CommentConfig : IEntityTypeConfiguration<Comment>
{
	public void Configure(EntityTypeBuilder<Comment> entity)
	{
		//entity.HasOne(d => d.Buyer).WithMany(p => p.Comments).HasConstraintName("FK_Comments_Customer_CustomerId");

		//entity.HasOne(d => d.Stock).WithMany(p => p.Comments).HasConstraintName("FK_Comments_Stocks");
	}
}
