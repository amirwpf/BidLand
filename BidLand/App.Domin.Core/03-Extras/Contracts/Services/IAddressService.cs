using App.Domin.Core._03_Extras.Contracts.Repositories.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domin.Core._03_Extras.Contracts.Services;

public interface IAddressService
{
	Task CreateAsync(AddressRepoDto input, CancellationToken cancellationToken);

	Task DeleteAsync(AddressRepoDto input, CancellationToken cancellationToken);

	Task<List<AddressRepoDto>> GetAllAsync(CancellationToken cancellationToken);

	Task<AddressRepoDto> GetByIdAsync(int id, CancellationToken cancellationToken);

	Task UpdateAsync(AddressRepoDto input, CancellationToken cancellationToken);
}
