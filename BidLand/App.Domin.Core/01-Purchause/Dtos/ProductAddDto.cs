using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domin.Core._01_Purchause.Dtos;

public class ProductAddDto
{
	public string? Name {get; set;}
	public int? BasePrice { get; set;}
	public string? Description { get; set;}

	public bool? IsConfirm { get; set; }

	public bool IsActive { get; set; }

	public bool IsDelete { get; set; }
}
