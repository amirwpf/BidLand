

namespace App.Domin.Core._01_Purchause.Contracts.Repositories.Dtos;

public class StockRepoDto
{
	public int Id { get; set; }

	public int? ProductId { get; set; }

	public int? BoothId { get; set; }

	public int Price { get; set; }

	public string? AdditionalDescription { get; set; }

	public int? AvailableNumber { get; set; }

	public bool IsActive { get; set; }

	public bool IsDelete { get; set; }

	public bool IsAuction { get; set; }

	public int? AuctionId { get; set; }

	public DateTime? InsertionDate { get; set; }
}
