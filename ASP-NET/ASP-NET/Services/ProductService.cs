using ASP_NET.Context;
using ASP_NET.Dto;
using ASP_NET.Entities;
using ASP_NET.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ASP_NET.Services;

public class ProductService : IProductService
{
    private readonly ApplicationDbContext _context;
    private readonly IAzureBlobStorage _azureBlobStorage;

    public ProductService(ApplicationDbContext context, IAzureBlobStorage azureBlobStorage)
    {
        this._context = context;
        this._azureBlobStorage = azureBlobStorage;
    }

    public async Task<ResultResponseDto> CreateProductAsync(CreateProductDto body)
    {

        if (body.Photo == null)
        {
            return new ResultResponseDto { Message = "Photo is required", Success = false };
        }

        string? uriToFile = null;

        var responseUpload = await this._azureBlobStorage.UploadFileToBlobStorageAsync(body.Photo);

        if (responseUpload.Success == true && responseUpload.Uri != null)
        {
            uriToFile = responseUpload.Uri;
        }
        else
        {
            return new ResultResponseDto { Success = false, Message = responseUpload.Message };
        }

        int Id = await _context.Products.CountAsync() + 1;
        var product = new Product { Id = Id, Name = body.Title, Picture = uriToFile, Price = body.Price, StoreId = body.StoreId };

        var result = await this.SaveProductToDatabaseAsync(product);

        if (result)
        {
            return new ResultResponseDto { Message = "Succes saved", Success = true };
        }

        return new ResultResponseDto { Message = "Something went wrong", Success = false };
    }

    public async Task<Boolean> SaveProductToDatabaseAsync(Product body)
    {
        try
        {
            this._context.Products.Add(body);

            await this._context.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

}

public interface IProductService
{
    Task<ResultResponseDto> CreateProductAsync(CreateProductDto body);
    Task<Boolean> SaveProductToDatabaseAsync(Product body);
}