

using App.Domin.Core._01_Purchause.Entities;
using App.Domin.Core._02_Users.Entities;
using App.Domin.Core._03_Extras.Entities;

namespace App.Domin.Core._02_Users.Contracts.Repositories.Dtos;

public class SellerRepoDto
{
	public int Id { get; set; }
    
    public int UserId { get; set; }
	public bool IsActive { get; set; } = true;

	public bool IsBan { get; set; }

	public bool IsDelete { get; set; }

	public double? CommissionPercentage { get; set; }

	public int? CommissionsAmount { get; set; }

	public int? SalesAmount { get; set; }

	public DateTime? InsertionDate { get; set; }

	public virtual ICollection<Address>? Addresses { get; set; } 

	public virtual Booth? Booth { get; set; }

    public int? MedalId { get; set; }
    public virtual Medal? Medal { get; set; } 

	public User User { get; set; }

}
