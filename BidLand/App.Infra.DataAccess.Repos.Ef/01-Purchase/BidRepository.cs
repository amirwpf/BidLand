using App.Domin.Core._01_Purchause.Contracts.Repositories.Dtos;
using App.Domin.Core._01_Purchause.Contracts.Repositories.RepoSeprationContracts.sqlServer;
using App.Domin.Core._01_Purchause.Entities;
using App.Infra.Db.sqlServer.Ef.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.DataAccess.Repos.Ef._01_Purchase;

public class BidRepository : IBidRepository
{
	private readonly AppDbContext _context;
	private readonly DbSet<Bid> _dbSet;

	public BidRepository(AppDbContext context)
	{
		_context = context;
		_dbSet = _context.Set<Bid>();
	}

	public async Task<bool> HasPlacedBid(int buyerId, int auctionId, CancellationToken cancellationToken)
	{
		var bid = await _dbSet.Where(b => b.Buyer.Id == buyerId)
			.FirstOrDefaultAsync(b => b.AuctionId == auctionId, cancellationToken);
		if (bid == null)
		{
			return false;
		}

		return true;
	}


	public async Task<List<BidRepoDto>> GetBidsByCustomerId(int buyerId, CancellationToken cancellationToken)
	{
		var customerBids = await _dbSet
			.Where(b => b.Buyer.Id == buyerId)
			.Select(b => ConvertToBidRepoDto(b)).ToListAsync(cancellationToken);
		return customerBids;
	}

	public async Task<BidRepoDto?> GetByIdAsync(int id, CancellationToken cancellationToken)
	{
		var result = await _dbSet.Where(a=>a.Id==id)
					.Select(b => ConvertToBidRepoDto(b)).FirstOrDefaultAsync(cancellationToken);
		if (result == null) return null;
		return result;
	}

	public async Task<List<BidRepoDto>> GetAllAsync(CancellationToken cancellationToken)
	{
		var result = await _dbSet
					.Select(b => ConvertToBidRepoDto(b)).ToListAsync(cancellationToken);
		return result;
	}


	public async Task<int> AddAsync(BidRepoDto dto, CancellationToken cancellationToken)
	{
		var bid = new Bid();
		Equaler(dto,ref bid);
		await _dbSet.AddAsync(bid);
		var result = await _context.SaveChangesAsync(cancellationToken);
		return result;
	}

	public async Task<bool> UpdateAsync(BidRepoDto bid, CancellationToken cancellationToken)
	{
		var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == bid.Id,cancellationToken);
		if (result !=null)
		{
			Equaler(bid,ref result);
			_context.Entry(result).State = EntityState.Modified;
			await _context.SaveChangesAsync(cancellationToken);
			return true;
		}
		return false;
	}

	public async Task<bool> HardDeleteAsync(BidRepoDto bid, CancellationToken cancellationToken)
	{
		var result = await _dbSet.Where(x=>x.Id==bid.Id).FirstOrDefaultAsync(cancellationToken);
		if (result != null)
		{
			_dbSet.Remove(result);
			await _context.SaveChangesAsync(cancellationToken);
			return true;
		}
		return false;
	}

	private BidRepoDto ConvertToBidRepoDto(Bid bid)
	{
		return new BidRepoDto()
		{
			Id = bid.Id,
			Price = bid.Price,
			BidDate = bid.BidDate,
			HasWon = bid.HasWon,
			AuctionId = bid.AuctionId,
			Auction = bid.Auction,
			Buyer = bid.Buyer,
			BuyerId = bid.BuyerId
		};
	}
	private void Equaler(BidRepoDto bidRepoDto , ref Bid bid)
	{
		bid.Id = bidRepoDto.Id;
		bid.Price = bidRepoDto.Price;
		bid.BidDate = bidRepoDto.BidDate;
		bid.HasWon = bidRepoDto.HasWon;
		bid.AuctionId = bidRepoDto.AuctionId;
		bid.Auction = bidRepoDto.Auction;
		bid.Buyer = bidRepoDto.Buyer;
		bid.BuyerId = bidRepoDto.BuyerId;
	}
}
