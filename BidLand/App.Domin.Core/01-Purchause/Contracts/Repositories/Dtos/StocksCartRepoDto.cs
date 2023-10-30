

namespace App.Domin.Core._01_Purchause.Contracts.Repositories.Dtos;

public class StocksCartRepoDto
{
	public int CartId { get; set; }

	public int StockId { get; set; }

	public int? Quantity { get; set; }

	public DateTime? InsertionDate { get; set; }
}
