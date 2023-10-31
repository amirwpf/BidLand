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
	public class StocksCartRepository : IStocksCartRepository
	{
		private readonly AppDbContext _context;
		private readonly DbSet<StocksCart> _dbSet;

		public StocksCartRepository(AppDbContext context, CancellationToken cancellationToken)
		{
			_context = context;
			_dbSet = _context.Set<StocksCart>();
		}
		public async Task AddProductToOldCartAsync(int cartId, int stockId, CancellationToken cancellationToken)
		{
			var cart = await _context.Carts
				.Include(pc => pc.StocksCarts)
				.FirstOrDefaultAsync(c => c.Id == cartId, cancellationToken);

			if (cart == null) return;

			var existingProductsCart = cart.StocksCarts
				.FirstOrDefault(pc => pc.StockId == stockId);

			if (existingProductsCart != null)
			{
				existingProductsCart.Quantity++;
				_context.Entry(existingProductsCart).State = EntityState.Modified;
			}
			else
			{
				var product = await _context.Products.FindAsync(stockId);
				if (product == null) return;

				var productsCart = new StocksCart
				{
					CartId = cart.Id,
					StockId = stockId,
					Quantity = 1,
					InsertionDate= DateTime.Now
				};

				cart.StocksCarts.Add(productsCart);
			}

			_context.Entry(cart).State = EntityState.Modified;
			await _context.SaveChangesAsync(cancellationToken);
		}


		public async Task<StocksCartRepoDto> GetByCartIdAsync(int cartId, CancellationToken cancellationToken)
		{
			var result = await _dbSet
				.Where(a=>a.CartId== cartId)
				.Select(a => new StocksCartRepoDto
				{
					CartId = a.CartId,
					StockId = a.StockId,
					Cart = a.Cart,
					InsertionDate = DateTime.Now,
					Quantity = a.Quantity,
					Stock = a.Stock
				})
				.FirstOrDefaultAsync(cancellationToken);

			return result;
		}

		public async Task<List<StocksCartRepoDto>> GetAllAsync(CancellationToken cancellationToken)
		{
			var result = await _dbSet
				.Select(a=> new StocksCartRepoDto
				{
					CartId= a.CartId,
					StockId= a.StockId,
					Cart= a.Cart,
					InsertionDate= DateTime.Now,
					Quantity= a.Quantity,
					Stock = a.Stock
				})
				.ToListAsync(cancellationToken);

			return result;
		}

		public async Task AddAsync(StocksCartRepoDto dto, CancellationToken cancellationToken)
		{
			var StocksCart = new StocksCart
			{
				CartId = dto.CartId,
				Quantity = dto.Quantity,
				Stock= dto.Stock,
				InsertionDate= DateTime.Now,
				Cart= dto.Cart,
				StockId = dto.StockId
			};
			await _dbSet.AddAsync(StocksCart);
			await _context.SaveChangesAsync(cancellationToken);
		}

		public async Task UpdateAsync(StocksCartRepoDto stocksCart, CancellationToken cancellationToken)
		{
			var res = await _dbSet.Where(x => x.CartId == stocksCart.CartId).FirstOrDefaultAsync(cancellationToken);
			_context.Entry(res).State = EntityState.Modified;
			await _context.SaveChangesAsync(cancellationToken);
		}

		public async Task DeleteAsync(StocksCartRepoDto stocksCart, CancellationToken cancellationToken)
		{
			var res =await _dbSet.Where(x => x.CartId == stocksCart.CartId ).FirstOrDefaultAsync(cancellationToken);
			_dbSet.Remove(res);
			await _context.SaveChangesAsync(cancellationToken);
		}
	}
}
