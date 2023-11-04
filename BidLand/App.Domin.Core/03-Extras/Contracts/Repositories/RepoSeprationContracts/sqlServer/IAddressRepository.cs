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
		Task<AddressRepoDto?> GetByIdAsync(int id,CancellationToken cancellationToken);
		Task<List<AddressRepoDto>> GetAllAsync(CancellationToken cancellationToken);
		Task AddAsync(AddressRepoDto address, CancellationToken cancellationToken);
		Task<bool> UpdateAsync(AddressRepoDto addressdto, CancellationToken cancellationToken);
		Task<bool> DeleteAsync(AddressRepoDto address, CancellationToken cancellationToken);
	}
}
