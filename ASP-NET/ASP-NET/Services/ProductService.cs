using ASP_NET.Context;
using ASP_NET.Dto;
using ASP_NET.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace ASP_NET.Services;

public class ProductService : IProductService
{
    private readonly ApplicationDbContext _context;

    private const string PublicProductsPhotoFolder = "public/products";

    public ProductService(ApplicationDbContext context)
    {
        this._context = context;
    }

    public async Task<ResultResponseDto> CreateProductAsync(CreateProductDto body)
    {

        if (body.Photo == null)
        {
            return new ResultResponseDto { Message = "Photo is required", Success = false };
        }

        var uploadsFolder = Path.Combine(PublicProductsPhotoFolder);

        Directory.CreateDirectory(uploadsFolder);

        var uniqueFileName = Guid.NewGuid().ToString() + "." + body.Photo.FileName.Split(".").Last();
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await body.Photo.CopyToAsync(fileStream);
        }

        int Id = await _context.Products.CountAsync() + 1;
        var product = new Product { Id = Id, Name = body.Title, Picture = uniqueFileName, Price = body.Price, StoreId = body.StoreId };

        Console.WriteLine(product.ToString());
        Console.WriteLine(JsonSerializer.Serialize(product));

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