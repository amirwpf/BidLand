using App.Domin.Core._01_Purchause.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infra.Db.sqlServer.Ef.Configurations.EntitesConfiguration;

public class CategoryConfig : IEntityTypeConfiguration<Category>
{
	public void Configure(EntityTypeBuilder<Category> entity)
	{
		entity.HasKey(e => e.Id).HasName("PK__Category__19093A2B7D631E80");

		entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent).HasConstraintName("FK_Categories_Categories");
	}
}
