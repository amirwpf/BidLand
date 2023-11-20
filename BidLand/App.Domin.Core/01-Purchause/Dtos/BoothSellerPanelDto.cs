using App.Domin.Core._01_Purchause.Contracts.Repositories.Dtos;
using App.Domin.Core._01_Purchause.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domin.Core._01_Purchause.Dtos;

public class BoothSellerPanelDto
{
	public ICollection<Stock>? Stocks { get; set; }
	public BoothRepoDto? booth { get; set; }
}
