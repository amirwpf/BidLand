using App.Domin.Core._03_Extras.Contracts.Repositories.Dtos;
using App.Domin.Core._03_Extras.Contracts.Repositories.RepoSeprationContracts.sqlServer;
using App.Domin.Core._03_Extras.Entities;
using App.Infra.Db.sqlServer.Ef.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
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

	public async Task<AddressRepoDto?> GetByIdAsync(int id, CancellationToken cancellationToken)
	{
		var result = await _dbSet
			.AsNoTracking()
			.Where(predicate => predicate.Id == id)
			.Include(x => x.Buyer)
			.Include(x => x.Seller)
			.Select(a => ConvertToAddressRepoDto(a)).FirstOrDefaultAsync(cancellationToken);
		if(result==null) return null;
		return result;
	}

	public async Task<List<AddressRepoDto>> GetAllAsync(CancellationToken cancellationToken)
	{
		var result = await _dbSet
			.AsNoTracking()
			.Include(x => x.Buyer)
			.Include(x => x.Seller)
			.Select(a => ConvertToAddressRepoDto(a)).ToListAsync(cancellationToken);
		return result;
	}

	public async Task AddAsync(AddressRepoDto addressRepDto, CancellationToken cancellationToken)
	{
		var address = new Address();
		Equaler(addressRepDto, ref address);
		await _dbSet.AddAsync(address, cancellationToken);
		await _context.SaveChangesAsync(cancellationToken);
	}

	public async Task<bool> UpdateAsync(AddressRepoDto addressdto, CancellationToken cancellationToken)
	{
		var address = await _dbSet.FirstOrDefaultAsync(x => x.Id == addressdto.Id, cancellationToken);
		if (address != null)
		{
			Equaler(addressdto, ref address);
			_context.Entry(address).State = EntityState.Modified;
			await _context.SaveChangesAsync(cancellationToken);
			return true;
		}
		return false;
	}

	public async Task<bool> DeleteAsync(AddressRepoDto addressdto, CancellationToken cancellationToken)
	{
		var address = await _dbSet.FirstOrDefaultAsync(x => x.Id == addressdto.Id, cancellationToken);
		if (address != null)
		{
			_dbSet.Remove(address);
			await _context.SaveChangesAsync(cancellationToken);
			return true;
		}
		return false;
	}

	private AddressRepoDto ConvertToAddressRepoDto(Address address)
	{
		return new AddressRepoDto()
		{
			Id = address.Id,
			BuyerId = address.BuyerId,
			City = address.City,
			No = address.No,
			Phone = address.Phone,
			PostalCode = address.PostalCode,
			Province = address.Province,
			SellerId = address.SellerId,
			Street = address.Street,
			Buyer = address.Buyer,
			Seller = address.Seller
		};
	}

	private void Equaler(AddressRepoDto addressRepoDto, ref Address address)
	{
		//address.Id = addressRepoDto.Id;
		address.BuyerId = addressRepoDto.BuyerId;
		address.City = addressRepoDto.City;
		address.No = addressRepoDto.No;
		address.Phone = addressRepoDto.Phone;
		address.PostalCode = addressRepoDto.PostalCode;
		address.Province = addressRepoDto.Province;
		address.SellerId = addressRepoDto.SellerId;
		address.Street = addressRepoDto.Street;
		address.Buyer = addressRepoDto.Buyer;
		address.Seller = addressRepoDto.Seller;
	}
}
