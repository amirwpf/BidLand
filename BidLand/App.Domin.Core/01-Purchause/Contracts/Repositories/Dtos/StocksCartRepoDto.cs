

using App.Domin.Core._01_Purchause.Entities;

namespace App.Domin.Core._01_Purchause.Contracts.Repositories.Dtos;

public class StocksCartRepoDto
{
	public int Id { get; set; }
	public int CartId { get; set; }

	public int StockId { get; set; }

	public int? Quantity { get; set; }

	public DateTime? InsertionDate { get; set; }

	public virtual Cart Cart { get; set; } = null!;

	public virtual Stock Stock { get; set; } = null!;
}
