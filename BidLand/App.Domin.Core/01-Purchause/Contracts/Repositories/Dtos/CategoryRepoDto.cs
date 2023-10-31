

using App.Domin.Core._01_Purchause.Entities;

namespace App.Domin.Core._01_Purchause.Contracts.Repositories.Dtos;

public class CategoryRepoDto
{
	public int Id { get; set; }

	public string Name { get; set; } = null!;

	public string? Description { get; set; }

	public int? ParentId { get; set; }

	public DateTime? InsertionDate { get; set; }
	public virtual ICollection<Category> InverseParent { get; set; } = new List<Category>();


	public virtual Category? Parent { get; set; }

	public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
