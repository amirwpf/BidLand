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
		Task AddProductToCategoryAsync(int productId, int categoryId);
		Task DeleteProductFromCategoryAsync(Category category, Product product);
		Task<Category> GetByIdOrginalAsync(int id);
		Task<CategoryRepoDto> GetByIdAsync(int id);
		Task<List<Category>> GetAllAsync();
		Task AddAsync(Category category);
		Task UpdateAsync(Category category);
		Task DeleteAsync(Category category);
	}
}
