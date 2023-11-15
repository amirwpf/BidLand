using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domin.Core._02_Users.Dtos;

public class SellerCommissionDto
{
	public int Id { get; set; }
	public string? Firstname { get; set; }
	public string? Lastname { get; set; }
	public float? Commision { get; set; }
}

