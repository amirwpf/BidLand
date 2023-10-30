

namespace App.Domin.Core._01_Purchause.Contracts.Repositories.Dtos;

public class CategoryRepoDto
{
	public int Id { get; set; }

	public string Name { get; set; } = null!;

	public string? Description { get; set; }

	public int? ParentId { get; set; }

	public DateTime? InsertionDate { get; set; }
}
