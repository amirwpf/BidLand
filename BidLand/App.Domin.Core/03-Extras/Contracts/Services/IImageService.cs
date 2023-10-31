using App.Domin.Core._03_Extras.Contracts.Repositories.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domin.Core._03_Extras.Contracts.Services;

public interface IImageService
{
	Task CreateAsync(ImageRepoDto input, CancellationToken cancellationToken);

	Task DeleteAsync(int id, CancellationToken cancellationToken);
	Task DeleteAsync(string url, CancellationToken cancellationToken);

	Task<List<ImageRepoDto>> GetAllImageForProductByIdAsync(int productId, CancellationToken cancellationToken);

	Task<ImageRepoDto> GetByIdAsync(int id, CancellationToken cancellationToken);

	Task UpdateAsync(ImageRepoDto input, CancellationToken cancellationToken);
}

