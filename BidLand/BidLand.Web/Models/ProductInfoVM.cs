using App.Domin.Core._01_Purchause.Contracts.Repositories.Dtos;

namespace BidLand.Web.Models
{
    public class ProductInfoVM
    {
        public ProductRepoDto Product { get; set; }
        public List<StockRepoDto> Stocks { get; set; }
    }
}
