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

public class AuctionRepository : IAuctionRepository
{
	private readonly AppDbContext _context;
	private readonly DbSet<Auction> _dbSet;
	public AuctionRepository(AppDbContext context)
	{
		_context = context;
		_dbSet = _context.Set<Auction>();
	}

	public async Task<List<AuctionRepoDto>> GetCompletedsAsync(CancellationToken cancellationToken)
	{
		var completedAuctions = await _dbSet
			.Where(a => a.EndDate <= DateTime.Now)
			.Select(a => ConvertToAuctionRepoDto(a))
			.ToListAsync(cancellationToken);
		return completedAuctions;
	}

	public async Task<AuctionRepoDto?> GetByIdAsync(int id, CancellationToken cancellationToken)
	{
		var action = await _dbSet.Include(p => p.Stock)
				.Select(a => ConvertToAuctionRepoDto(a))
			   .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
		if (action == null) return null;
		return action;
	}
	public async Task<List<AuctionRepoDto>> GetAllAsync(CancellationToken cancellationToken)
	{
		var result = await _dbSet.AsNoTracking()
			.Select(a => ConvertToAuctionRepoDto(a)).ToListAsync(cancellationToken);
		return result;
	}
	public async Task<List<AuctionRepoDto>> GetAllTrueAsync(CancellationToken cancellationToken)
	{
		var result = await _dbSet.AsNoTracking()
			.Where(x => x.Stock.IsAuction)
			.Select(a => ConvertToAuctionRepoDto(a)).ToListAsync(cancellationToken);
		return result;
	}
	public async Task AddAsync(AuctionRepoDto auction, CancellationToken cancellationToken)
	{
		var result = new Auction();
		Equaler(auction, ref result);
		_context.Entry(result).State = EntityState.Added;
		await _context.SaveChangesAsync(cancellationToken);
	}
	public async Task<bool> UpdateAsync(AuctionRepoDto auction, CancellationToken cancellationToken)
	{
		var result = await _dbSet.Where(x => x.Id == auction.Id).FirstOrDefaultAsync(cancellationToken);
		if (result != null)
		{
			Equaler(auction, ref result);
			_context.Entry(result).State = EntityState.Modified;
			await _context.SaveChangesAsync(cancellationToken);
			return true;
		}
		return false;
	}
	public async Task<bool> SoftDeleteAsync(AuctionRepoDto auction, CancellationToken cancellationToken)
	{
		var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == auction.Id,cancellationToken);
		if (result != null)
		{
			result.IsDelete = true;
			_context.Entry(result).State = EntityState.Modified;
			await _context.SaveChangesAsync(cancellationToken);
			return true;
		}
		return false;	
	}

	public async Task<bool> SoftRecoverAsync(AuctionRepoDto auction, CancellationToken cancellationToken)
	{
		var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == auction.Id, cancellationToken);
		if (result != null)
		{
			result.IsDelete = false;
			_context.Entry(result).State = EntityState.Modified;
			await _context.SaveChangesAsync(cancellationToken);
			return true;
		}
		return false;
	}

	public async Task<bool> HardDeleteAsync(AuctionRepoDto auction, CancellationToken cancellationToken)
	{
		var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == auction.Id, cancellationToken);
		if (result != null)
		{
			_dbSet.Remove(result);
			_context.Entry(result).State = EntityState.Modified;
			await _context.SaveChangesAsync(cancellationToken);
			return true;
		}
		return false;
	}

	//public async Task<int> UpdateWithBidAsync(Auction auction, BidRepoDto bidDto, CancellationToken cancellationToken)
	//{
	//	var bid = new Bid
	//	{
	//		Price = bidDto.Price,
	//		BidDate = bidDto.BidDate,
	//		AuctionId = bidDto.AuctionId,
	//		BuyerId = bidDto.BuyerId
	//	};
	//	auction.Bids.Add(bid);
	//	_context.Entry(auction).State = EntityState.Modified;
	//	var result = await _context.SaveChangesAsync(cancellationToken);
	//	return result;
	//}

	//public async Task<bool> HasOwnedAction(int userId, int auctionId)
	//{
	//	var action = await _dbSet.Where(s => s.Stock.Booth.SellerId == userId)
	//		.FirstOrDefaultAsync(a => a.Id == auctionId);
	//	if (action == null)
	//		return false;
	//	return true;
	//}

	private AuctionRepoDto ConvertToAuctionRepoDto(Auction auction)
	{
		return new AuctionRepoDto()
		{
			Id = auction.Id,
			StartDate = auction.StartDate,
			EndDate = auction.EndDate,
			IsActive = auction.IsActive,
			IsDelete = auction.IsDelete,
			CurrentHighestPrice = auction.CurrentHighestPrice,
			MinimumFinalPrice = auction.MinimumFinalPrice,
			StockId = auction.StockId,
			InsertionDate = auction.InsertionDate
		};
	}

	private void Equaler(AuctionRepoDto auctionRepoDto, ref Auction auction)
	{
		auction.Id = auctionRepoDto.Id;
		auction.StartDate = auctionRepoDto.StartDate;
		auction.EndDate = auctionRepoDto.EndDate;
		auction.IsActive = auctionRepoDto.IsActive;
		auction.IsDelete = auctionRepoDto.IsDelete;
		auction.CurrentHighestPrice = auctionRepoDto.CurrentHighestPrice;
		auction.MinimumFinalPrice = auctionRepoDto.MinimumFinalPrice;
		auction.StockId = auctionRepoDto.StockId;
		auction.InsertionDate = auctionRepoDto.InsertionDate;
	}
}
