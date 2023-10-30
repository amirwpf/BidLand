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

		public StockssCartRepository(AppDbContext context)
		{
			_context = context;
			_dbSet = _context.Set<StocksCart>();
		}
		public async Task AddProductToOldCartAsync(int cartId, int productId)
		{
			var cart = await _context.Carts
				.Include(pc => pc.ProductsCarts)
				.FirstOrDefaultAsync(c => c.Id == cartId);

			if (cart == null) return;

			var existingProductsCart = cart.ProductsCarts
				.FirstOrDefault(pc => pc.ProductId == productId);

			if (existingProductsCart != null)
			{
				existingProductsCart.Quantity++;
				existingProductsCart.Cart.TotalPrices += existingProductsCart.Product.BasePrice;
				_context.Entry(existingProductsCart).State = EntityState.Modified;
			}
			else
			{
				var product = await _context.Products.FindAsync(productId);
				if (product == null) return;

				var productsCart = new ProductsCart
				{
					CartId = cart.Id,
					ProductId = productId,
					Quantity = 1,
					OrderDate = DateTime.Now
				};

				cart.ProductsCarts.Add(productsCart);
				cart.TotalPrices += product.BasePrice;
			}

			_context.Entry(cart).State = EntityState.Modified;
			await _context.SaveChangesAsync();
		}


		public async Task<ProductsCart> GetByIdAsync(int id)
		{
			return await _dbSet.FindAsync(id);
		}

		public async Task<List<ProductsCart>> GetAllAsync()
		{
			return await _dbSet.ToListAsync();
		}

		public async Task AddAsync(ProductsCartDto dto)
		{
			var productsCart = new ProductsCart
			{
				CartId = (int)dto.CartId,
				ProductId = dto.ProductId,
				Quantity = 1
			};
			await _dbSet.AddAsync(productsCart);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(ProductsCart productsCart)
		{
			_context.Entry(productsCart).State = EntityState.Modified;
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(ProductsCart productsCart)
		{
			_dbSet.Remove(productsCart);
			await _context.SaveChangesAsync();
		}
	}
}
