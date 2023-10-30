

namespace App.Domin.Core._01_Purchause.Contracts.Repositories.Dtos;

public class CartRepoDto
{
	public int Id { get; set; }

	public int? Value { get; set; }

	public int? BuyerId { get; set; }

	public DateTime? PurchaseDate { get; set; }

	public bool PurchaseCompeleted { get; set; }

	public DateTime? InsertionDate { get; set; }
}
