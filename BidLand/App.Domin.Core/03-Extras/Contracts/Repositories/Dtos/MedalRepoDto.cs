
using App.Domin.Core._02_Users.Entities;
using App.Domin.Core._03_Extras.Enums;

namespace App.Domin.Core._03_Extras.Contracts.Repositories.Dtos;

public class MedalRepoDto
{
	public int Id { get; set; }

	public MedalEnum? LevelType { get; set; }
	public int? Percentage { get; set; }

	public int? SellerId { get; set; }

	public DateTime? InsertionDate { get; set; }
	public virtual Seller? Seller { get; set; }
}
