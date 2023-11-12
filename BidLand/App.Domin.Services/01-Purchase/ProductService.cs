using App.Domin.Core._01_Purchause.Contracts.Repositories.Dtos;
using App.Domin.Core._01_Purchause.Contracts.Repositories.RepoSeprationContracts.sqlServer;
using App.Domin.Core._01_Purchause.Contracts.Services;
using App.Domin.Core._01_Purchause.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domin.Services._01_Purchase;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepo;

    public ProductService(IProductRepository productRepo)
    {
        _productRepo = productRepo;
    }

    public async Task CreateAsync(ProductRepoDto input, CancellationToken cancellationToken)
    {
        await _productRepo.AddAsync(input, cancellationToken);
    }

    //public async Task<int> CreateAsync(ProductAddDto input, CancellationToken cancellationToken)
    //{
    //	return await _productRepo.AddAsync(input, cancellationToken);
    //}

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
      return  await _productRepo.HardDeleteAsync(id, cancellationToken);
    }

    public async Task<List<ProductRepoDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _productRepo.GetAllProductsWithNavAsync(cancellationToken);
    }

    public async Task<ProductRepoDto> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _productRepo.GetByIdAsync(id, cancellationToken);
    }

    public async Task<bool> UpdateAsync(ProductRepoDto input,  CancellationToken cancellationToken)
    {
    return    await _productRepo.UpdateAsync(input, cancellationToken);
    }

    public async Task<bool> ConfirmProductAsync(int productId, bool confirm, CancellationToken cancellationToken)
    {
        if (confirm)
         return  await  _productRepo.ConfirmProductAsync(productId, true, cancellationToken);

        else
           return await _productRepo.ConfirmProductAsync(productId, false, cancellationToken);

    }

    public async Task<List<ProductRepoDto>> GetAllConfirmedProductsAsync(CancellationToken cancellationToken)
    {
        return await _productRepo.GetAllConfirmProductsWithNavAsync(true, cancellationToken);
    }

    public async Task<List<ProductRepoDto>> GetAllPendingProductsAsync(CancellationToken cancellationToken)
    {
        return await _productRepo.GetAllConfirmProductsWithNavAsync(false, cancellationToken);
        
    }
}
