using App.Domin.Core._01_Purchause.Contracts.Repositories.Dtos;
using App.Domin.Core._01_Purchause.Contracts.Repositories.RepoSeprationContracts.sqlServer;
using App.Domin.Core._01_Purchause.Entities;
using App.Infra.Db.sqlServer.Ef.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.DataAccess.Repos.Ef._01_Purchase
{
	public class CategoryRepository : ICategoryRepository
	{
		private readonly AppDbContext _context;
		private readonly DbSet<Category> _dbSet;

		public CategoryRepository(AppDbContext context)
		{
			_context = context;
			_dbSet = _context.Set<Category>();
		}

		public async Task<CategoryRepDto> GetByIdAsync(int id)
		{
			var product = await _dbSet.AsNoTracking()
				.FirstOrDefaultAsync(c => c.Id == id);
			return new CategoryRepDto
			{
				Id = product.Id,
				Name = product.Name,
				Description = product.Description,
				Products = product.Products
			};

		}
		public async Task<Category> GetByIdOrginalAsync(int id)
		{
			var product = await _dbSet.AsNoTracking()
				.FirstOrDefaultAsync(c => c.Id == id);
			return product;

		}
		public async Task<List<Category>> GetAllAsync()
		{
			return await _dbSet.ToListAsync();
		}

		public async Task AddAsync(Category category)
		{
			await _dbSet.AddAsync(category);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(Category category)
		{
			_context.Entry(category).State = EntityState.Modified;
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(Category category)
		{
			_dbSet.Remove(category);
			await _context.SaveChangesAsync();
		}

		public async Task AddProductToCategoryAsync(int productId, int categoryId)
		{

			var trackedProduct = await _context.Products.FindAsync(productId);
			var trackedcaCategoryt = await _context.Categories.FindAsync(categoryId);
			trackedProduct.Categories.Add(trackedcaCategoryt);
			_context.Entry(trackedProduct).State = EntityState.Modified;
			await _context.SaveChangesAsync();
		}


		public async Task DeleteProductFromCategoryAsync(Category category, Product product)
		{
			category.Products.Remove(product);
			await _context.SaveChangesAsync();
		}
	}
}
