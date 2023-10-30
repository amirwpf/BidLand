

namespace App.Domin.Core._02_Users.Contracts.Repositories.Dtos;

public class SellerRepoDto
{
	public int Id { get; set; }

	public string? FullName { get; set; }

	public bool IsActive { get; set; }

	public bool IsBan { get; set; }

	public bool IsDelete { get; set; }

	public double? CommissionPercentage { get; set; }

	public int? CommissionsAmount { get; set; }

	public int? SalesAmount { get; set; }

	public DateTime? InsertionDate { get; set; }
}
