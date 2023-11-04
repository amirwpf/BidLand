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

namespace App.Infra.DataAccess.Repos.Ef._03_Extras;

public class ImageRepository : IImageRepository
{
	private readonly AppDbContext _dbContext;
	private readonly DbSet<Image> _imageSet;

	public ImageRepository(AppDbContext dbContext)
	{
		_dbContext = dbContext;
		_imageSet = _dbContext.Set<Image>();
	}

	public async Task<ImageRepoDto?> GetByIdAsync(int imageId, CancellationToken cancellationToken)
	{
		var result = await _imageSet.Where(i => i.Id == imageId)
			.Select(i => ConvertToDto(i)).FirstOrDefaultAsync(cancellationToken);
		if (result == null) return null;
		return result;
	}
	public async Task<List<ImageRepoDto>> GetAllImageForProductByIdAsync(int productId, CancellationToken cancellationToken)
	{

		var images = await _imageSet
			.Where(i => i.Product.Id == productId)
			.Select(i => ConvertToDto(i)).ToListAsync(cancellationToken);

		return images;
	}

	public async Task<ImageRepoDto?> GetByUrlAsync(string url, CancellationToken cancellationToken)
	{
		var result = await _imageSet.Where(i => i.Url == url)
			.Select(i => ConvertToDto(i)).FirstOrDefaultAsync(cancellationToken);
		if(result == null) return null;
		return result;
	}

	public async Task AddAsync(ImageRepoDto imageDto, CancellationToken cancellationToken)
	{
		var image = new Image();
		Equaler(imageDto, ref image);
		await _imageSet.AddAsync(image, cancellationToken);
		var result = await _dbContext.SaveChangesAsync(cancellationToken);
	}

	public async Task<bool> HardDeleteAsync(int id, CancellationToken cancellationToken)
	{
		var image = await _imageSet.FirstOrDefaultAsync(i=>i.Id==id, cancellationToken);
		if (image != null)
		{
			_imageSet.Remove(image);
			await _dbContext.SaveChangesAsync(cancellationToken);
			return true;
		}
		return false;
	}

	public async Task<bool> HardDeleteAsync(string url, CancellationToken cancellationToken)
	{
		var image = await _imageSet.FirstOrDefaultAsync(image => image.Url == url);
		if (image != null)
		{
			_imageSet.Remove(image);
			await _dbContext.SaveChangesAsync(cancellationToken);
			return true;
		}
		return false;
	}

	public async Task<bool> UpdateAsync(ImageRepoDto imageRepoDto, CancellationToken cancellationToken)
	{
		var result = await _imageSet.FirstOrDefaultAsync(x => x.Id == imageRepoDto.Id,cancellationToken);
		if (result != null)
		{
			var image = _imageSet.Remove(result);
			await _dbContext.SaveChangesAsync(cancellationToken);
			return true;
		}
		return false;
	}


	private ImageRepoDto ConvertToDto(Image image)
	{
		return new ImageRepoDto()
		{
			Id = image.Id,
			Url = image.Url,
			ProductId = image.ProductId,
			Product = image.Product,
			InsertionDate = image.InsertionDate
		};
	}

	private void Equaler(ImageRepoDto imageDto, ref Image image)
	{
		image.Id = imageDto.Id;
		image.Url = imageDto.Url;
		image.ProductId = imageDto.ProductId;
		image.Product = imageDto.Product;
		image.InsertionDate = imageDto.InsertionDate;
	}
}
