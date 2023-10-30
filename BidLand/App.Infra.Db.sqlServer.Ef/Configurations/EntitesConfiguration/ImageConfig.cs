using App.Domin.Core._03_Extras.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Db.sqlServer.Ef.Configurations.EntitesConfiguration;

public class ImageConfig : IEntityTypeConfiguration<Image>
{
	public void Configure(EntityTypeBuilder<Image> entity)
	{
		entity.HasOne(d => d.Product).WithMany(p => p.Images).HasConstraintName("FK_Image_Product");
	}
}
