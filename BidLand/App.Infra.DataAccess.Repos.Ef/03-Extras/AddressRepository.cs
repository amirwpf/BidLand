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

	public async Task<Address> GetByIdAsync(int id)
	{
		return await _dbSet.FindAsync(id);
	}

	public async Task<List<Address>> GetAllAsync()
	{
		return await _dbSet.ToListAsync();
	}

	public async Task AddAsync(AddressRepoDto addressRepDto)
	{
		var address = new Address
		{
			City = addressRepDto.City,
			Street = addressRepDto.Street,
			//Description = addressRepDto.Description,
			SellerId = addressRepDto.SellerId,
			//CustomerId = addressRepDto.CustomerId
		};
		await _dbSet.AddAsync(address);
		await _context.SaveChangesAsync();
	}

	public async Task UpdateAsync(AddressRepoDto addressdto)
	{
		var address = await _dbSet.FindAsync(addressdto.Id);
		if (address == null)
		{
			// Handle the case when the address is not found
			return;
		}

		// Update address properties
		address.City = addressdto.City;
		address.Street = addressdto.Street;
		//address.Description = addressdto.Description;

		_context.Entry(address).State = EntityState.Modified;
		await _context.SaveChangesAsync();
	}

	public async Task DeleteAsync(Address address)
	{
		_dbSet.Remove(address);
		await _context.SaveChangesAsync();
	}

	Task<Address> IAddressRepository.GetByIdAsync(int id)
	{
		throw new NotImplementedException();
	}

	Task<List<Address>> IAddressRepository.GetAllAsync()
	{
		throw new NotImplementedException();
	}
}
