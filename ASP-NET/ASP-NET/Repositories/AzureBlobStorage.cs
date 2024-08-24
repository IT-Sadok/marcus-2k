using ASP_NET.Dto;
using Azure;
using Azure.Storage.Blobs;

namespace ASP_NET.Repositories;

public class AzureBlobStorage : IAzureBlobStorage
{
    private readonly IConfiguration _configuration;

    private readonly string _storageConnectionString;
    private readonly string _storageContainerName;

    public AzureBlobStorage(IConfiguration configuration)
    {
        _configuration = configuration;

        this._storageConnectionString = configuration.GetValue<string>("BlobConnectionString");
        this._storageContainerName = configuration.GetValue<string>("BlobContainerName");
    }

    public async Task<BlobResponseDto> UploadFileToBlobStorageAsync(IFormFile blob)
    {
        BlobResponseDto response = new();

        BlobContainerClient container = new BlobContainerClient(_storageConnectionString, _storageContainerName);

        var uniqueFileName = Guid.NewGuid().ToString() + "." + blob.FileName.Split(".").Last();

        BlobClient client = container.GetBlobClient(uniqueFileName);

        try
        {
            await using (Stream? data = blob.OpenReadStream())
            {
                await client.UploadAsync(data);
            }

            response.Success = true;
            response.Uri = client.Uri.ToString();
            response.Message = "Successfully uploaded";

            return response;
        }
        catch (RequestFailedException ex)
        {
            response.Success = false;
            response.Uri = null;
            response.Message = ex.Message;

            return response;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Uri = null;
            response.Message = ex.Message;

            return response;
        }
    }
}

public interface IAzureBlobStorage
{
    Task<BlobResponseDto> UploadFileToBlobStorageAsync(IFormFile blob);
}
