using App.Domin.Core._03_Extras.Contracts.Repositories.Dtos;
using App.Domin.Core._03_Extras.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domin.Core._03_Extras.Contracts.Repositories.RepoSeprationContracts.sqlServer
{
	public interface IAddressRepository
	{
		Task<Address> GetByIdAsync(int id);
		Task<List<Address>> GetAllAsync();
		Task AddAsync(AddressRepoDto address);
		Task UpdateAsync(AddressRepoDto addressdto);
		Task DeleteAsync(Address address);
	}
}
