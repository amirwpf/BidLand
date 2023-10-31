using App.Domin.Core._01_Purchause.Entities;
using App.Domin.Core._02_Users.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domin.Core._01_Purchause.Contracts.Repositories.Dtos;

public class BidRepoDto
{
	public int Id { get; set; }

	public int? Price { get; set; }

	public DateTime? BidDate { get; set; }

	public bool? HasWon { get; set; }

	public int? AuctionId { get; set; }

	public int? BuyerId { get; set; }
	public virtual Auction? Auction { get; set; }

	public virtual Buyer? Buyer { get; set; }
}
