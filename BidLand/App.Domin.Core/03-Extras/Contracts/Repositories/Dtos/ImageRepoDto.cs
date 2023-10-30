

namespace App.Domin.Core._03_Extras.Contracts.Repositories.Dtos;

public class ImageRepoDto
{
	public int Id { get; set; }

	public int? ProductId { get; set; }

	public string? Url { get; set; }

	public DateTime? InsertionDate { get; set; }
}
