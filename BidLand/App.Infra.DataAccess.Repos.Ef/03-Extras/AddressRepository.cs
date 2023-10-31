using App.Domin.Core._03_Extras.Contracts.Repositories.Dtos;
using App.Domin.Core._03_Extras.Contracts.Repositories.RepoSeprationContracts.sqlServer;
using App.Domin.Core._03_Extras.Entities;
using App.Infra.Db.sqlServer.Ef.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.DataAccess.Repos.Ef._03_Extres;

public class AddressRepository : IAddressRepository
{
	private readonly AppDbContext _context;
	private readonly DbSet<Address> _dbSet;

	public AddressRepository(AppDbContext context)
	{
		_context = context;
		_dbSet = _context.Set<Address>();
	}

	public async Task<AddressRepoDto> GetByIdAsync(int id, CancellationToken cancellationToken)
	{
		var result = await _dbSet.Where(predicate=> predicate.Id == id)
			.Include(x=>x.Buyer)
			.Include(x=>x.Seller)
			.Select(a=>new AddressRepoDto
			{
				Id= a.Id,
				BuyerId= a.BuyerId,
				City= a.City,
				No= a.No,
				Phone= a.Phone,
				PostalCode= a.PostalCode,
				Province= a.Province,
				SellerId= a.SellerId,
				Street= a.Street,
				Buyer = a.Buyer,
				Seller = a.Seller
			}).FirstOrDefaultAsync(cancellationToken);
		return result;
	}

	public async Task<List<AddressRepoDto>> GetAllAsync(CancellationToken cancellationToken)
	{
		var result = await _dbSet
			.Include(x => x.Buyer)
			.Include(x => x.Seller)
			.Select(a => new AddressRepoDto
			{
				Id = a.Id,
				BuyerId = a.BuyerId,
				City = a.City,
				No = a.No,
				Phone = a.Phone,
				PostalCode = a.PostalCode,
				Province = a.Province,
				SellerId = a.SellerId,
				Street = a.Street,
				Buyer=a.Buyer,
				Seller=a.Seller
			}).ToListAsync(cancellationToken);
		return result;
	}

	public async Task AddAsync(AddressRepoDto addressRepDto, CancellationToken cancellationToken)
	{
		var address = new Address
		{
			City = addressRepDto.City,
			Street = addressRepDto.Street,
			SellerId = addressRepDto.SellerId,
			No = addressRepDto.No,
			Province = addressRepDto.Province,
			BuyerId = addressRepDto.BuyerId,
			Phone = addressRepDto.Phone,
			PostalCode = addressRepDto.PostalCode,
	};
		await _dbSet.AddAsync(address, cancellationToken);
		await _context.SaveChangesAsync(cancellationToken);
	}

	public async Task UpdateAsync(AddressRepoDto addressdto, CancellationToken cancellationToken)
	{
		var address = await _dbSet.FirstOrDefaultAsync(x=>x.Id==addressdto.Id, cancellationToken);
		if (address == null)
		{
			// Handle the case when the address is not found
			return;
		}

		// Update address properties
		address.City = addressdto.City;
		address.Street = addressdto.Street;
		address.SellerId = addressdto.SellerId;
		address.No=addressdto.No;
		address.Province = addressdto.Province;
		address.BuyerId= addressdto.BuyerId;
		address.Phone = addressdto.Phone;
		address.PostalCode = addressdto.PostalCode;
		//address.Description = addressdto.Description;

		_context.Entry(address).State = EntityState.Modified;
		await _context.SaveChangesAsync(cancellationToken);
	}

	public async Task DeleteAsync(AddressRepoDto address, CancellationToken cancellationToken)
	{
		var result = await _dbSet.Where(x=>x.Id==address.Id).FirstOrDefaultAsync(cancellationToken);
		_dbSet.Remove(result);
		await _context.SaveChangesAsync(cancellationToken);
	}
}
