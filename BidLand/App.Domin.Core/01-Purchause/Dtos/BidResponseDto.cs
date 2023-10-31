using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domin.Core._01_Purchause.Dtos;

public class BidResponseDto
{
	public int Id { get; set; }
	public int? BuyerId { get; set; }
	public float? TotalPrices { get; set; }
	public bool? PurchaseCompeleted { get; set; }
	public List<string>? ProductsNames { get; set; }
	public int? QuantityFromOne { get; set; }
}
