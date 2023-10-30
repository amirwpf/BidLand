

namespace App.Domin.Core._01_Purchause.Contracts.Repositories.Dtos;

public class BoothRepoDto
{
	public int Id { get; set; }

	public string? Name { get; set; }

	public string? Description { get; set; }

	public bool IsDelete { get; set; }

	public int? SellerId { get; set; }

	public DateTime? InsertionDate { get; set; }
}
