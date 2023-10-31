using App.Domin.Core._01_Purchause.Contracts.Repositories.Dtos;
using App.Domin.Core._01_Purchause.Dtos;
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
		Task<List<ProductRepoDto>> GetAllProductsWithNavAsync(CancellationToken cancellationToken);
		Task<int> AddAsync(ProductAddDto productDto, CancellationToken cancellationToken);
		Task UpdateAsync(Product product, CancellationToken cancellationToken);
		Task UpdateAsync(ProductRepoDto product, CancellationToken cancellationToken);
		Task<ProductRepoDto> GetByIdAsync(int id, CancellationToken cancellationToken);
		Task DeleteAsync(int id, CancellationToken cancellationToken);
		Task UpdateAsync(ProductRepoDto productDto, List<int> categoryIds, CancellationToken cancellationToken);

		#region other
		//Task<string> RemoveFromCartByProductId(int productId, int customerId);
		//Task<List<ProductRepoDto>> GetProductByBoothIdAsync(int boothId);
		//Task<List<ProductRepoDto>> GetProductsWithTrueAuctions(int sellerId);
		//Task<List<Product>> GetProductsWithSellerNameConfirmAsync();
		//Task<List<ProductRepoDto>> GetAllWithNavigationsAsync(int? boothId);
		//Task<ProductRepoDto> GetWithAllNavigationsByIdSellerAsync(int id);
		//Task<List<Product>> GetAllAsync();
		#endregion

	}
}
