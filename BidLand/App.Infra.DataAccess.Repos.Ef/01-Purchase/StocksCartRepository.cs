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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace App.Infra.DataAccess.Repos.Ef._01_Purchase;

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

			var productsCart = new StocksCart()
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


	public async Task<StocksCartRepoDto?> GetByCartIdAsync(int cartId, CancellationToken cancellationToken)
	{
		var result = await _dbSet
			.Where(a=>a.CartId== cartId)
			.Include(x => x.Cart)
			.Include(x => x.Stock)
			.Select(a => ConvertToStocksCartRepoDto(a))
			.FirstOrDefaultAsync(cancellationToken);
		if (result == null) return null;

		return result;
	}

	public async Task<List<StocksCartRepoDto>> GetAllAsync(CancellationToken cancellationToken)
	{
		var result = await _dbSet
			.Include(x=>x.Cart)
			.Include(x=>x.Stock)
			.Select(a=> ConvertToStocksCartRepoDto(a))
			.ToListAsync(cancellationToken);

		return result;
	}

	public async Task AddAsync(StocksCartRepoDto dto, CancellationToken cancellationToken)
	{
		var StocksCart = new StocksCart();
		UpdaterEq(dto, ref StocksCart);
		await _dbSet.AddAsync(StocksCart);
		await _context.SaveChangesAsync(cancellationToken);
	}

	public async Task<bool> UpdateAsync(StocksCartRepoDto stocksCart, CancellationToken cancellationToken)
	{
		var res = await _dbSet.Where(x => x.CartId == stocksCart.CartId).FirstOrDefaultAsync(cancellationToken);
		if(res != null)
		{
			UpdaterEq(stocksCart, ref res);
			_context.Entry(res).State = EntityState.Modified;
			await _context.SaveChangesAsync(cancellationToken);
			return true;
		}
		return false;
	}

	public async Task<bool> HardDeleteAsync(StocksCartRepoDto stocksCart, CancellationToken cancellationToken)
	{
		var res =await _dbSet.Where(x => x.CartId == stocksCart.CartId ).FirstOrDefaultAsync(cancellationToken);
		if(res != null)
		{
			_dbSet.Remove(res);
			await _context.SaveChangesAsync(cancellationToken);
			return true;
		}
		return false;
	}

	private StocksCart ConvertToStocksCart(StocksCartRepoDto stocksCartRepoDto)
	{
		return new StocksCart()
		{
			CartId = stocksCartRepoDto.CartId,
			StockId = stocksCartRepoDto.StockId,
			Cart = stocksCartRepoDto.Cart,
			InsertionDate = stocksCartRepoDto.InsertionDate,
			Quantity = stocksCartRepoDto.Quantity,
			Stock = stocksCartRepoDto.Stock
		};
	}

	private StocksCartRepoDto ConvertToStocksCartRepoDto(StocksCart stocksCart)
	{
		return new StocksCartRepoDto()
		{
			CartId = stocksCart.CartId,
			StockId = stocksCart.StockId,
			Cart = stocksCart.Cart,
			InsertionDate = stocksCart.InsertionDate,
			Quantity = stocksCart.Quantity,
			Stock = stocksCart.Stock
		};
	}

	private void UpdaterEq(StocksCartRepoDto dto, ref StocksCart stocksCart)
	{
		stocksCart.CartId = dto.CartId;
		stocksCart.Quantity = dto.Quantity;
		stocksCart.Stock = dto.Stock;
		stocksCart.InsertionDate = dto.InsertionDate;
		stocksCart.Cart = dto.Cart;
		stocksCart.StockId = dto.StockId;
	}
}
