

using App.Domin.Core._01_Purchause.Entities;
using App.Domin.Core._03_Extras.Entities;
using Microsoft.AspNetCore.Http;

namespace App.Domin.Core._01_Purchause.Contracts.Repositories.Dtos;

public class ProductRepoDto
{
	//public ProductRepoDto()
	//{
	//	this.IsConfirm = false;
	//}
	public int Id { get; set; }

	public string? Name { get; set; }

	public int? BasePrice { get; set; }

	public bool? IsConfirm { get; set; } = false;

	public string? Description { get; set; }

	public bool IsActive { get; set; }

	public bool IsDelete { get; set; }

	public DateTime? InsertionDate { get; set; } 

	public int? CategoryId { get; set; }

	public virtual ICollection<Image>? Images { get; set; }

	public virtual ICollection<Stock>? Stocks { get; set; } 

	public virtual Category? Category { get; set; }

	public virtual ICollection<IFormFile>? ImageFile { get; set; }
}
