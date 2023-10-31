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
		public async Task<BoothRepoDto> GetByIdAsync(int id, CancellationToken cancellationToken)
		{
			var booth =  _dbSet.AsNoTracking().Include(x => x.Seller).Include(x => x.Stocks).Where(x => x.Id == id);
			var result = await booth.Select(b => new BoothRepoDto
			{
				Id = b.Id,
				Name = b.Name,
				Description = b.Description,
				Seller = b.Seller,
				SellerId = b.SellerId,
				SellerName = b.Seller.FullName,
				Stocks = b.Stocks,
				IsDelete = b.IsDelete,
				InsertionDate = b.InsertionDate
			}).FirstOrDefaultAsync(cancellationToken);
			return result;
		}
		public async Task<List<BoothRepoDto>> GetAllAsync(CancellationToken cancellationToken)
		{
			var booth = await _dbSet.AsNoTracking().Include(x => x.Seller).Include(x=>x.Stocks).ToListAsync(cancellationToken);
			var result = booth.Select(b => new BoothRepoDto
			{
				Id = b.Id,
				Name = b.Name,
				Description = b.Description,
				Seller = b.Seller,
				SellerId = b.SellerId,
				SellerName = b.Seller.FullName,
				Stocks= b.Stocks,
				IsDelete=b.IsDelete,
				InsertionDate=b.InsertionDate
			}).ToList();
			return result;
		}
		public async Task<List<BoothRepoDto>> GetByCategoryIdAsync(int categoryId, CancellationToken cancellationToken)
		{
			var booth = await _dbSet
				 .Where(b => b.Stocks.Any(p => p.Product.Categories.Any(c => c.Id == categoryId)))
				 .Include(s => s.Seller)
				 .ToListAsync(cancellationToken);
			var result = booth.Select(b => new BoothRepoDto
			{
				Id = b.Id,
				Name = b.Name,
				Description = b.Description,
				Seller = b.Seller,
				SellerId = b.SellerId,
				SellerName = b.Seller.FullName,
				Stocks = b.Stocks,
				IsDelete = b.IsDelete,
				InsertionDate = b.InsertionDate
			}).ToList();
			return result;
		}
		public async Task AddAsync(BoothRepoDto boothRepDto, CancellationToken cancellationToken)
		{
			var booth = new Booth()
			{
				Name = boothRepDto.Name,
				Description = boothRepDto.Description,
				SellerId = boothRepDto.SellerId,
			};
			await _dbSet.AddAsync(booth, cancellationToken);
			await _context.SaveChangesAsync(cancellationToken);
		}
		public async Task<bool> UpdateBoothAsync(BoothRepoDto boothRepDto, CancellationToken cancellationToken)
		{
			var booth = await _dbSet.FirstOrDefaultAsync(x => x.Id == boothRepDto.Id);
			if (booth == null)
			{
				return false;
			}
			booth.Name = boothRepDto.Name;
			booth.Description = boothRepDto.Description;
			_context.Entry(booth).State = EntityState.Modified;
			await _context.SaveChangesAsync(cancellationToken);
			return true;
		}
		public async Task<bool> DeleteAsync(BoothRepoDto boothRepo, CancellationToken cancellationToken)
		{
			var booth = await _dbSet.Where(x=>x.Id== boothRepo.Id).FirstOrDefaultAsync(cancellationToken);
			booth.IsDelete = true;
			_context.Entry(booth).State = EntityState.Modified;
			var result = await _context.SaveChangesAsync(cancellationToken);
			if (result == 0)
				return false;
			return true;
		}
	}
}
