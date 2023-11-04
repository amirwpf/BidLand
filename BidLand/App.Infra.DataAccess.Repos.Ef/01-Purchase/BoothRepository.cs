using App.Domin.Core._01_Purchause.Contracts.Repositories.Dtos;
using App.Domin.Core._01_Purchause.Contracts.Repositories.RepoSeprationContracts.sqlServer;
using App.Domin.Core._01_Purchause.Entities;
using App.Infra.Db.sqlServer.Ef.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Infra.DataAccess.Repos.Ef._01_Purchase;

public class BoothRepository : IBoothRepository
{
	private readonly AppDbContext _context;
	private readonly DbSet<Booth> _dbSet;
	public BoothRepository(AppDbContext context)
	{
		_context = context;
		_dbSet = _context.Set<Booth>();
	}
	public async Task<BoothRepoDto?> GetByIdAsync(int id, CancellationToken cancellationToken)
	{
		var booth = await _dbSet.AsNoTracking()
			.Include(x => x.Seller)
			.Include(x => x.Stocks)
			.Select(b => ConvertToBoothRepoDto(b)).FirstOrDefaultAsync(x => x.Id == id,cancellationToken);
		if(booth == null)
			return null;
		return booth;
	}
	public async Task<List<BoothRepoDto>> GetAllAsync(CancellationToken cancellationToken)
	{
		var booth = await _dbSet.AsNoTracking().Include(x => x.Seller).Include(x=>x.Stocks).ToListAsync(cancellationToken);
		var result = booth.Select(b => ConvertToBoothRepoDto(b)).ToList();
		return result;
	}
	public async Task<List<BoothRepoDto>> GetByCategoryIdAsync(int categoryId, CancellationToken cancellationToken)
	{
		var booth = await _dbSet
			 .Where(b => b.Stocks.Any(p => p.Product.Category.Id == categoryId))
			 .Include(s => s.Seller)
			 .Select(b => ConvertToBoothRepoDto(b))
			 .ToListAsync(cancellationToken);
		return booth;
	}
	public async Task AddAsync(BoothRepoDto boothRepDto, CancellationToken cancellationToken)
	{
		var booth = new Booth();
		Equaler(boothRepDto, ref booth);
		await _dbSet.AddAsync(booth, cancellationToken);
		await _context.SaveChangesAsync(cancellationToken);
	}
	public async Task<bool> UpdateBoothAsync(BoothRepoDto boothRepDto, CancellationToken cancellationToken)
	{
		var booth = await _dbSet.FirstOrDefaultAsync(x => x.Id == boothRepDto.Id);
		if (booth != null)
		{
			Equaler(boothRepDto, ref booth);
			_context.Entry(booth).State = EntityState.Modified;
			await _context.SaveChangesAsync(cancellationToken);
			return true;
		}
		return false;
	}
	public async Task<bool> SoftDeleteAsync(BoothRepoDto boothRepDto, CancellationToken cancellationToken)
	{
		var booth = await _dbSet.FirstOrDefaultAsync(x => x.Id == boothRepDto.Id);
		if (booth != null)
		{
			booth.IsDelete=true;
			_context.Entry(booth).State = EntityState.Modified;
			await _context.SaveChangesAsync(cancellationToken);
			return true;
		}
		return false;
	}

	public async Task<bool> SoftRecoverAsync(BoothRepoDto boothRepDto, CancellationToken cancellationToken)
	{
		var booth = await _dbSet.FirstOrDefaultAsync(x => x.Id == boothRepDto.Id);
		if (booth != null)
		{
			booth.IsDelete = false;
			_context.Entry(booth).State = EntityState.Modified;
			await _context.SaveChangesAsync(cancellationToken);
			return true;
		}
		return false;
	}

	public async Task<bool> HardDeleteAsync(BoothRepoDto boothRepDto, CancellationToken cancellationToken)
	{
		var booth = await _dbSet.FirstOrDefaultAsync(x => x.Id == boothRepDto.Id);
		if (booth != null)
		{
			_dbSet.Remove(booth);
			_context.Entry(booth).State = EntityState.Modified;
			await _context.SaveChangesAsync(cancellationToken);
			return true;
		}
		return false;
	}

	private BoothRepoDto ConvertToBoothRepoDto(Booth booth)
	{
		return new BoothRepoDto()
		{
			Id = booth.Id,
			Name = booth.Name,
			Description = booth.Description,
			Seller = booth.Seller,
			SellerId = booth.SellerId,
			SellerName = booth.Seller.FullName,
			Stocks = booth.Stocks,
			IsDelete = booth.IsDelete,
			InsertionDate = booth.InsertionDate
		};
	}
	private void Equaler(BoothRepoDto boothRepoDto ,  ref Booth booth)
	{
		booth.Id = boothRepoDto.Id;
		booth.Name = boothRepoDto.Name;
		booth.Description = boothRepoDto.Description;
		booth.Seller = boothRepoDto.Seller;
		booth.SellerId = boothRepoDto.SellerId;
		booth.Name = boothRepoDto.Name;
		booth.Stocks = boothRepoDto.Stocks;
		booth.IsDelete = boothRepoDto.IsDelete;
		booth.InsertionDate = boothRepoDto.InsertionDate;
	}
}
