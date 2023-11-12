

using App.Domin.Core._01_Purchause.Entities;
using App.Domin.Core._02_Users.Entities;
using App.Domin.Core._03_Extras.Entities;

namespace App.Domin.Core._02_Users.Contracts.Repositories.Dtos;

public class BuyerRepoDto
{
	public int Id { get; set; }

	public int UserId { get; set; }

	public int? Credit { get; set; }

	public int? TotalPurchaseAmount { get; set; }

	public bool IsBan { get; set; }
	public bool IsDelete { get; set; }


	public DateTime? InsertionDate { get; set; }

	public virtual ICollection<Address> Addresses { get; set; } 

	public virtual ICollection<Bid> Bids { get; set; }

	public virtual ICollection<Cart> Carts { get; set; } 

	public virtual ICollection<Comment> Comments { get; set; } 
	public User User { get; set; }
}
