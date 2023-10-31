using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domin.Core._01_Purchause.Contracts.Repositories.Dtos;

public class AuctionRepoDto
{
	public int Id { get; set; }
	public DateTime? StartDate { get; set; }

	public DateTime? EndDate { get; set; }

	public bool? IsActive { get; set; }

	public bool? IsDelete { get; set; }

	public int? CurrentHighestPrice { get; set; }

	public int MinimumFinalPrice { get; set; }

	public int StockId { get; set; }

	public DateTime? InsertionDate { get; set; }

}
