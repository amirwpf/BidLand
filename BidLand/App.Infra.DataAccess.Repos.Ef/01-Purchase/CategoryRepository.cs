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

namespace App.Infra.DataAccess.Repos.Ef._01_Purchase;

public class CategoryRepository : ICategoryRepository
{
	private readonly AppDbContext _context;
	private readonly DbSet<Category> _dbSet;

	public CategoryRepository(AppDbContext context)
	{
		_context = context;
		_dbSet = _context.Set<Category>();
	}

	public async Task<CategoryRepoDto?> GetByIdAsync(int id, CancellationToken cancellationToken)
	{
		var category = await _dbSet.AsNoTracking()
			.Include(x => x.Products)
			.Select(p => ConvertToCategoryRepoDto(p))
			.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
		if (category != null) return category;
		return null;

	}

	public async Task<List<CategoryRepoDto>> GetAllAsync(CancellationToken cancellationToken)
	{
		var category = await _dbSet.AsNoTracking()
			.Include(x => x.Products)
			.Select(p => ConvertToCategoryRepoDto(p))
			.ToListAsync(cancellationToken);
		return category;
	}

	public async Task AddAsync(CategoryRepoDto category, CancellationToken cancellationToken)
	{
		var result = new Category();
		Equaler(category, ref result);
		await _dbSet.AddAsync(result, cancellationToken);
		await _context.SaveChangesAsync(cancellationToken);
	}

	public async Task<bool> UpdateAsync(CategoryRepoDto category, CancellationToken cancellationToken)
	{
		var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == category.Id, cancellationToken);
		if (result != null)
		{
			Equaler(category, ref result);
			_context.Entry(result).State = EntityState.Modified;
			await _context.SaveChangesAsync(cancellationToken);
			return true;
		}
		return false;
	}

	public async Task<bool> DeleteAsync(CategoryRepoDto category, CancellationToken cancellationToken)
	{
		var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == category.Id, cancellationToken);
		if (result != null)
		{
			_dbSet.Remove(result);
			await _context.SaveChangesAsync(cancellationToken);
			return true;
		}
		return false;
	}


	private Category ConvertToCategory(CategoryRepoDto categoryRepoDto)
	{
		return new Category()
		{
			Id = categoryRepoDto.Id,
			Name = categoryRepoDto.Name,
			Description = categoryRepoDto.Description,
			Products = categoryRepoDto.Products,
			ParentId = categoryRepoDto.ParentId,
			Parent = categoryRepoDto.Parent,
			InsertionDate = categoryRepoDto.InsertionDate,
			InverseParent = categoryRepoDto.InverseParent

		};
	}

	private CategoryRepoDto ConvertToCategoryRepoDto(Category category)
	{
		return new CategoryRepoDto()
		{
			Id = category.Id,
			Name = category.Name,
			Description = category.Description,
			Products = category.Products,
			ParentId = category.ParentId,
			Parent = category.Parent,
			InsertionDate = category.InsertionDate,
			InverseParent = category.InverseParent

		};
	}

	private void Equaler(CategoryRepoDto categoryRepoDto, ref Category category)
	{
		category.Id = categoryRepoDto.Id;
		category.Name = categoryRepoDto.Name;
		category.Description = categoryRepoDto.Description;
		category.Products = categoryRepoDto.Products;
		category.ParentId = categoryRepoDto.ParentId;
		category.Parent = categoryRepoDto.Parent;
		category.InsertionDate = categoryRepoDto.InsertionDate;
		category.InverseParent = categoryRepoDto.InverseParent;
	}
}
