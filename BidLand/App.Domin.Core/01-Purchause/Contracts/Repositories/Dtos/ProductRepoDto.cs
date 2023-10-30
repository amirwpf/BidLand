

namespace App.Domin.Core._01_Purchause.Contracts.Repositories.Dtos;

public class ProductRepoDto
{
	public int Id { get; set; }

	public string? Name { get; set; }

	public int? BasePrice { get; set; }

	public bool? IsConfirm { get; set; }

	public string? Description { get; set; }

	public bool IsActive { get; set; }

	public bool IsDelete { get; set; }

	public int? BidId { get; set; }

	public DateTime? InsertionDate { get; set; }
}
