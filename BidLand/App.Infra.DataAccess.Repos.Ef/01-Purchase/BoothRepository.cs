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
	public class BoothRepository : IBoothRepository
	{
		private readonly AppDbContext _context;
		private readonly DbSet<Booth> _dbSet;
		public BoothRepository(AppDbContext context)
		{
			_context = context;
			_dbSet = _context.Set<Booth>();
		}
		public async Task<Booth> GetByIdAsync(int id)
		{
			return await _dbSet.FindAsync(id);
		}
		public async Task<List<BoothRepositoryDto>> GetAllAsync()
		{
			var booth = await _dbSet.AsNoTracking().Include(x => x.Seller).ToListAsync();
			var result = booth.Select(b => new BoothRepositoryDto
			{
				Id = b.Id,
				Name = b.Name,
				Description = b.Description,
				Seller = b.Seller,
				SellerId = b.SellerId,
				SellerName = b.Seller.CompanyName
			}).ToList();
			return result;
		}
		public async Task<List<Booth>> GetByCategoryIdAsync(int categoryId)
		{
			var result = await _dbSet
				 .Where(b => b.Products.Any(p => p.Categories.Any(c => c.Id == categoryId)))
				 .Include(s => s.Seller)
				 .ToListAsync();
			return result;
		}
		public async Task AddAsync(BoothRepDto boothRepDto)
		{
			var booth = new Booth()
			{
				Name = boothRepDto.Name,
				Description = boothRepDto.Description,
				SellerId = boothRepDto.SellerId,
			};
			await _dbSet.AddAsync(booth);
			await _context.SaveChangesAsync();
		}
		public async Task<bool> UpdateBoothAsync(BoothRepDto boothRepDto)
		{
			var booth = await _dbSet.FirstOrDefaultAsync(x => x.Id == boothRepDto.BoothId);
			if (booth == null)
			{
				return false;
			}
			booth.Name = boothRepDto.Name;
			booth.Description = boothRepDto.Description;
			_context.Entry(booth).State = EntityState.Modified;
			await _context.SaveChangesAsync();
			return true;
		}
		public async Task<bool> DeleteAsync(int id)
		{
			var booth = await _dbSet.FindAsync(id);
			booth.IsRemoved = true;
			_context.Entry(booth).State = EntityState.Modified;
			var result = await _context.SaveChangesAsync();
			if (result == 0)
				return false;
			return true;
		}
	}
}
