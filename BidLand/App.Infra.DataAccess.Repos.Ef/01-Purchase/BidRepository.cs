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
				.Select(b => new BidRepoDto
				{
					Id = b.Id,
					Price = b.Price,
					BidDate = b.BidDate,
					HasWon= b.HasWon,
					AuctionId= b.AuctionId,
					Auction = b.Auction,
					Buyer= b.Buyer,
					BuyerId= b.BuyerId
				}).ToListAsync(cancellationToken);
			return customerBids;
		}

		public async Task<BidRepoDto> GetByIdAsync(int id, CancellationToken cancellationToken)
		{
			var result = await _dbSet.Where(a=>a.Id==id)
						.Select(b => new BidRepoDto
						{
							Id = b.Id,
							Price = b.Price,
							BidDate = b.BidDate,
							HasWon = b.HasWon,
							AuctionId = b.AuctionId,
							Auction = b.Auction,
							Buyer = b.Buyer,
							BuyerId = b.BuyerId
						}).FirstOrDefaultAsync(cancellationToken);
			return result;
		}

		public async Task<List<BidRepoDto>> GetAllAsync(CancellationToken cancellationToken)
		{
			var result = await _dbSet
						.Select(b => new BidRepoDto
						{
							Id = b.Id,
							Price = b.Price,
							BidDate = b.BidDate,
							HasWon = b.HasWon,
							AuctionId = b.AuctionId,
							Auction = b.Auction,
							Buyer = b.Buyer,
							BuyerId = b.BuyerId
						}).ToListAsync(cancellationToken);
			return result;
		}
	

		public async Task<int> AddAsync(BidRepoDto dto, CancellationToken cancellationToken)
		{
			var bid = new Bid
			{
				Id = dto.Id,
				Price = dto.Price,
				BidDate = dto.BidDate,
				HasWon = dto.HasWon,
				AuctionId = dto.AuctionId,
				Auction = dto.Auction,
				Buyer = dto.Buyer,
				BuyerId = dto.BuyerId
			};
			await _dbSet.AddAsync(bid);
			var result = await _context.SaveChangesAsync(cancellationToken);
			return result;
		}

		public async Task UpdateAsync(BidRepoDto bid, CancellationToken cancellationToken)
		{
			var result = await _dbSet.Where(x => x.Id == bid.Id).FirstOrDefaultAsync(cancellationToken);
			_context.Entry(result).State = EntityState.Modified;
			await _context.SaveChangesAsync(cancellationToken);
		}

		public async Task DeleteAsync(BidRepoDto bid, CancellationToken cancellationToken)
		{
			var result = await _dbSet.Where(x=>x.Id==bid.Id).FirstOrDefaultAsync(cancellationToken);
			_dbSet.Remove(result);
			await _context.SaveChangesAsync(cancellationToken);
		}
	}
}
