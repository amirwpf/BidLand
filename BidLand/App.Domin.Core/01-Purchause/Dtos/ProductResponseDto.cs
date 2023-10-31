using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domin.Core._01_Purchause.Dtos;

public class ProductResponseDto
{
	public string? Name {get; set;}
	public int? BasePrice { get; set;}
	public string? Description { get; set;}
}
