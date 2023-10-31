
using App.Domin.Core._02_Users.Entities;

namespace App.Domin.Core._03_Extras.Contracts.Repositories.Dtos;

public class AddressRepoDto
{
	public int Id { get; set; }

	public string? Province { get; set; }

	public string? City { get; set; }

	public string? Street { get; set; }

	public int? No { get; set; }

	public int? Phone { get; set; }

	public int? PostalCode { get; set; }

	public int? BuyerId { get; set; }

	public int? SellerId { get; set; }

	public virtual Buyer? Buyer { get; set; }

	public virtual Seller? Seller { get; set; }
}
