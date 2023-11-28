using App.Domin.Core._01_Purchause.Contracts.Repositories.Dtos;

namespace BidLand.Web.Models
{
	public class ProductIndexVM
	{
		public int? CategoryId { get; set; }
		public string? SortBy { get; set; }
		public int? PageSize { get; set; }
		public int? CurrentPage { get; set; }

		public List<ProductRepoDto>? Products{ get; set; }

	}
}
