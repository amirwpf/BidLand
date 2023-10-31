

using App.Domin.Core._01_Purchause.Entities;
using App.Domin.Core._02_Users.Entities;

namespace App.Domin.Core._01_Purchause.Contracts.Repositories.Dtos;

public class CartRepoDto
{
	public int Id { get; set; }

	public int? Value { get; set; }

	public int? BuyerId { get; set; }

	public DateTime? PurchaseDate { get; set; }

	public bool PurchaseCompeleted { get; set; }

	public DateTime? InsertionDate { get; set; }

	public virtual Buyer? Buyer { get; set; }

	public virtual ICollection<StocksCart> StocksCarts { get; set; } = new List<StocksCart>();
}
