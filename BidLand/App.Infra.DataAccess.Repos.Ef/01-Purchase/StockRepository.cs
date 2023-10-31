using App.Domin.Core._01_Purchase.Contracts.Repositories.RepoSeprationContracts.sqlServer;
using App.Domin.Core._01_Purchause.Contracts.Repositories.Dtos;
using App.Domin.Core._01_Purchause.Contracts.Repositories.RepoSeprationContracts.sqlServer;
using App.Domin.Core._01_Purchause.Entities;
using App.Infra.Db.sqlServer.Ef.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.DataAccess.Repos.Ef._01_Purchase;

public class StockRepository:IStockRepository
{
	private readonly AppDbContext _context;
	private readonly DbSet<Stock> _dbSet;

	public StockRepository(AppDbContext appDbContext)
	{
		_context = appDbContext;
		_dbSet = _context.Set<Stock>();
	}
	public async Task<List<StockRepoDto>> GetAllStocks(CancellationToken cancellationToken)
	{
		var result = await _context.Stocks.Select(x => new StockRepoDto
		{
			AdditionalDescription = x.AdditionalDescription,
			AuctionId = x.AuctionId,
			AvailableNumber = x.AvailableNumber,
			BoothId = x.BoothId,
			Id = x.Id,
			InsertionDate = x.InsertionDate,
			IsActive = x.IsActive,
			IsAuction = x.IsAuction,
			IsDelete = x.IsDelete,
			Price = x.Price,
			ProductId = x.ProductId,
			Auction = x.Auction,
			Booth = x.Booth,
			Comments = x.Comments,
			Product = x.Product,
			StocksCarts = x.StocksCarts
		}).ToListAsync(cancellationToken);
		return result;
	}

	public async Task<StockRepoDto> GetStockById(int stockId, CancellationToken cancellationToken)
	{
		var result = await _context.Stocks.Select(x => new StockRepoDto
		{
			AdditionalDescription = x.AdditionalDescription,
			AuctionId = x.AuctionId,
			AvailableNumber = x.AvailableNumber,
			BoothId = x.BoothId,
			Id = x.Id,
			InsertionDate = x.InsertionDate,
			IsActive = x.IsActive,
			IsAuction = x.IsAuction,
			IsDelete = x.IsDelete,
			Price = x.Price,
			ProductId = x.ProductId,
			Auction = x.Auction,
			Booth = x.Booth,
			Comments = x.Comments,
			Product = x.Product,
			StocksCarts = x.StocksCarts
		}).FirstOrDefaultAsync(x=>x.Id== stockId, cancellationToken);
		return result;
	}

	public async Task AddAsync(StockRepoDto dto, CancellationToken cancellationToken)
	{
		var productsCart = new Stock
		{
			Id= dto.Id,
			AdditionalDescription= dto.AdditionalDescription,
			AuctionId= dto.AuctionId,
			AvailableNumber= dto.AvailableNumber,
			BoothId= dto.BoothId,
			Auction= dto.Auction,
			Booth = dto.Booth,
			Comments = dto.Comments,
			InsertionDate= dto.InsertionDate,
			IsActive= dto.IsActive,
			IsAuction = dto.IsAuction,
			IsDelete = dto.IsDelete,
			Price = dto.Price,
			ProductId = dto.ProductId,
			Product= dto.Product,
			StocksCarts = dto.StocksCarts
		};
		await _dbSet.AddAsync(productsCart, cancellationToken);
		await _context.SaveChangesAsync(cancellationToken);
	}

	public async Task UpdateAsync(StockRepoDto stock, CancellationToken cancellationToken)
	{
		var res = await _dbSet.Where(x => x.Id == stock.Id).FirstOrDefaultAsync(cancellationToken);
		_context.Entry(res).State = EntityState.Modified;
		await _context.SaveChangesAsync(cancellationToken);
	}

	public async Task DeleteAsync(StockRepoDto stock, CancellationToken cancellationToken)
	{
		var res = await _dbSet.Where(x => x.Id == stock.Id).FirstOrDefaultAsync(cancellationToken);
		if (res != null)
		{
			res.IsDelete = true;
		}
		//_dbSet.Remove(res);
		_context.Entry(res).State = EntityState.Modified;
		await _context.SaveChangesAsync(cancellationToken);
	}
}
