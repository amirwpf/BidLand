

using App.Domin.Core._01_Purchause.Entities;
using App.Domin.Core._03_Extras.Entities;

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

	public DateTime? InsertionDate { get; set; }

	public virtual ICollection<Image> Images { get; set; } = new List<Image>();

	public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();

	public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
}
