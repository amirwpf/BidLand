using App.Domin.Core._03_Extras.Contracts.Repositories.Dtos;
using App.Domin.Core._03_Extras.Contracts.Repositories.RepoSeprationContracts.sqlServer;
using App.Domin.Core._03_Extras.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Domin.Services._03_Extras;

public class ImageService: IImageService
{
	private readonly IImageRepository _repo;

	public ImageService(IImageRepository repo)
	{
		_repo = repo;
	}
	public async Task CreateAsync(ImageRepoDto input, CancellationToken cancellationToken)
	{
		await _repo.AddAsync(input, cancellationToken);
	}

	public async Task DeleteAsync(int id, CancellationToken cancellationToken)
	{
		await _repo.DeleteAsync(id, cancellationToken);
	}
	public async Task DeleteAsync(string url, CancellationToken cancellationToken)
	{
		await _repo.DeleteAsync(url, cancellationToken);
	}

	public async Task<List<ImageRepoDto>> GetAllImageForProductByIdAsync(int productId,CancellationToken cancellationToken)
	{
		return await _repo.GetAllImageForProductByIdAsync(productId,cancellationToken);
	}

	public async Task<ImageRepoDto> GetByIdAsync(int id, CancellationToken cancellationToken)
	{
		return await _repo.GetByIdAsync(id, cancellationToken);
	}

	public async Task UpdateAsync(ImageRepoDto input, CancellationToken cancellationToken)
	{
		await _repo.UpdateAsync(input, cancellationToken);
	}
}
