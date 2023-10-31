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

		public async Task<CategoryRepoDto> GetByIdAsync(int id, CancellationToken cancellationToken)
		{
			var category = await _dbSet.AsNoTracking()
				.Include(x=>x.Products)
				.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
			return new CategoryRepoDto
			{
				Id = category.Id,
				Name = category.Name,
				Description = category.Description,
				Products = category.Products,
				ParentId = category.ParentId,
				Parent= category.Parent,
				InsertionDate = category.InsertionDate,
				InverseParent = category.InverseParent
			};

		}
		public async Task<CategoryRepoDto> GetByIdOrginalAsync(int id, CancellationToken cancellationToken)
		{
			var category = await _dbSet.AsNoTracking()
				.Include(x => x.Products)
				.Select(p => new CategoryRepoDto
				{
					Id = p.Id,
					Name = p.Name,
					Description = p.Description,
					Products = p.Products,
					ParentId = p.ParentId,
					Parent = p.Parent,
					InsertionDate = p.InsertionDate,
					InverseParent = p.InverseParent
				})
				.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
			return category;

		}
		public async Task<List<CategoryRepoDto>> GetAllAsync(CancellationToken cancellationToken)
		{
			var category = await _dbSet.AsNoTracking()
				.Include(x => x.Products)
				.Select(p => new CategoryRepoDto
				{
					Id = p.Id,
					Name = p.Name,
					Description = p.Description,
					Products = p.Products,
					ParentId = p.ParentId,
					Parent = p.Parent,
					InsertionDate = p.InsertionDate,
					InverseParent = p.InverseParent
				})
				.ToListAsync(cancellationToken);
			return category;
		}

		public async Task AddAsync(CategoryRepoDto category, CancellationToken cancellationToken)
		{
			var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == category.Id, cancellationToken);
			await _dbSet.AddAsync(result);
			await _context.SaveChangesAsync(cancellationToken);
		}

		public async Task UpdateAsync(CategoryRepoDto category, CancellationToken cancellationToken)
		{
			var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == category.Id,cancellationToken);
			_context.Entry(result).State = EntityState.Modified;
			await _context.SaveChangesAsync(cancellationToken);
		}

		public async Task DeleteAsync(CategoryRepoDto category, CancellationToken cancellationToken)
		{
			var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == category.Id, cancellationToken);
			_dbSet.Remove(result);
			await _context.SaveChangesAsync(cancellationToken);
		}

		public async Task AddProductToCategoryAsync(int productId, int categoryId, CancellationToken cancellationToken)
		{

			var trackedProduct = await _context.Products.FindAsync(productId);
			var trackedcaCategoryt = await _context.Categories.FindAsync(categoryId);
			trackedProduct.Categories.Add(trackedcaCategoryt);
			_context.Entry(trackedProduct).State = EntityState.Modified;
			await _context.SaveChangesAsync(cancellationToken);
		}


		public async Task DeleteProductFromCategoryAsync(CategoryRepoDto category, ProductRepoDto product, CancellationToken cancellationToken)
		{
			var result = await _context.Products.Where(x => x.Id == product.Id).FirstOrDefaultAsync(cancellationToken);
			category.Products.Remove(result);
			await _context.SaveChangesAsync(cancellationToken);
		}
	}
}
