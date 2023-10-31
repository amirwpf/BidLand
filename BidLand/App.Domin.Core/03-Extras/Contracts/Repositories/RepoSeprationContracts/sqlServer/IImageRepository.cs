using App.Domin.Core._03_Extras.Contracts.Repositories.Dtos;
using App.Domin.Core._03_Extras.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domin.Core._03_Extras.Contracts.Repositories.RepoSeprationContracts.sqlServer
{
	public interface IImageRepository
	{
		Task<ImageRepoDto> GetByIdAsync(int imageId,CancellationToken cancellationToken);
		Task<List<ImageRepoDto>> GetAllImageForProductByIdAsync(int productId, CancellationToken cancellationToken);
		Task<ImageRepoDto> GetByUrlAsync(string url, CancellationToken cancellationToken);
		Task<int> AddAsync(ImageRepoDto imageDto, CancellationToken cancellationToken);
		Task DeleteAsync(int id, CancellationToken cancellationToken);
		Task DeleteAsync(string url, CancellationToken cancellationToken);
		Task UpdateAsync(ImageRepoDto imageRepoDto, CancellationToken cancellationToken);

	}
}
