

namespace App.Domin.Core._02_Users.Contracts.Repositories.Dtos;

public class BuyerRepoDto
{
	public int Id { get; set; }

	public string? FullName { get; set; }

	public int? Credit { get; set; }

	public int? TotalPurchaseAmount { get; set; }

	public bool? IsBan { get; set; }

	public DateTime? InsertionDate { get; set; }
}
