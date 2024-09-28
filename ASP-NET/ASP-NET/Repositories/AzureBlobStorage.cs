using ASP_NET.Dto;
using Azure;
using Azure.Storage.Blobs;

namespace ASP_NET.Repositories;

public class AzureBlobStorage : IAzureBlobStorage
{

    private readonly BlobContainerClient _blobContainerClient;

    public AzureBlobStorage(BlobContainerClient blobContainerClient)
    {
        _blobContainerClient = blobContainerClient;
    }

    public async Task<BlobResponseDto> UploadFileToBlobStorageAsync(IFormFile blob)
    {
        BlobResponseDto response = new();

        var uniqueFileName = Guid.NewGuid().ToString() + "." + blob.FileName.Split(".").Last();

        BlobClient client = _blobContainerClient.GetBlobClient(uniqueFileName);

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
