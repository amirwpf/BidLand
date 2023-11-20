using System.ComponentModel.DataAnnotations;

namespace BidLand.Web.Areas.Seller.Models
{
	public class AddAuctionStockVM
	{
		[Required]
		[DataType(DataType.Date)]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
		public DateTime StartDate{get;set;}

		[Required]
		[DataType(DataType.Date)]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
		public DateTime EndDate{get;set;}
		[Required]
		public int MinimumFinalPrice { get; set; }

		public int StockId { get; set; }
	}
}
