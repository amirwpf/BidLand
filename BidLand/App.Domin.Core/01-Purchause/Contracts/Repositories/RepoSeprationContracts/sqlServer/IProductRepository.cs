using App.Domin.Core._01_Purchause.Contracts.Repositories.Dtos;
using App.Domin.Core._01_Purchause.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domin.Core._01_Purchause.Contracts.Repositories.RepoSeprationContracts.sqlServer
{
	public interface IProductRepository
	{
		Task<string> RemoveFromCartByProductId(int productId, int customerId);
		Task<List<ProductRepoDto>> GetAllProductsForView();
		Task UpdateAsync(ProductRepoDto productDto, List<int> categoryIds);
		Task<List<ProductRepoDto>> GetProductByBoothIdAsync(int boothId);
		Task<List<ProductRepoDto>> GetProductsWithTrueAuctions(int sellerId);
		Task<List<Product>> GetProductsWithSellerNameConfirmAsync();
		Task<List<ProductRepoDto>> GetAllWithNavigationsAsync(int? boothId);
		Task<ProductRepoDto> GetWithAllNavigationsByIdSellerAsync(int id);
		Task<Product> GetByIdAsync(int id);
		Task<List<Product>> GetAllAsync();
		Task<int> AddAsync(ProductRepoDto product);
		Task UpdateAsync(ProductRepoDto product);
		Task DeleteAsync(int id);
		Task UpdateAsync(Product product);
	}
}
