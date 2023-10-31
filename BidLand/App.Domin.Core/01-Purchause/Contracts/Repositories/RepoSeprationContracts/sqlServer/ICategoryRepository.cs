using App.Domin.Core._01_Purchause.Contracts.Repositories.Dtos;
using App.Domin.Core._01_Purchause.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domin.Core._01_Purchause.Contracts.Repositories.RepoSeprationContracts.sqlServer
{
	public interface ICategoryRepository
	{
		Task<CategoryRepoDto> GetByIdAsync(int id, CancellationToken cancellationToken);
		Task<CategoryRepoDto> GetByIdOrginalAsync(int id, CancellationToken cancellationToken);
		Task<List<CategoryRepoDto>> GetAllAsync(CancellationToken cancellationToken);
		Task AddAsync(CategoryRepoDto category, CancellationToken cancellationToken);
		Task UpdateAsync(CategoryRepoDto category, CancellationToken cancellationToken);
		Task DeleteAsync(CategoryRepoDto category, CancellationToken cancellationToken);
		Task AddProductToCategoryAsync(int productId, int categoryId, CancellationToken cancellationToken);
		Task DeleteProductFromCategoryAsync(CategoryRepoDto category, ProductRepoDto product, CancellationToken cancellationToken);
	}
}
