using Azure.Storage.Blobs;
using Microsoft.Extensions.Azure;

namespace Film.Integration.API.Providers
{
    public class AzureBlobStorageProvider : IBlobStorageProvider
    {
        private readonly ILogger<AzureBlobStorageProvider> logger;
        private readonly BlobServiceClient filmBlobServiceClient;

        public AzureBlobStorageProvider(ILogger<AzureBlobStorageProvider> logger, IAzureClientFactory<BlobServiceClient> clientFactory)
        {
            this.logger = logger;
            this.filmBlobServiceClient = clientFactory.CreateClient("filmBlobStorage");
        }

        public async Task<Stream> GetBlobContent(string containerName, string blobName)
        {
            try
            {
                BlobContainerClient containerClient = filmBlobServiceClient.GetBlobContainerClient(containerName);
                BlobClient blobClient = containerClient.GetBlobClient(blobName);

                Stream? content = null;
                if (await blobClient.ExistsAsync())
                {
                    logger.LogInformation("Blob exists");
                    content = await blobClient.OpenReadAsync();
                }
                else
                {
                    logger.LogCritical($"Unable to find the blob in container {containerName} reference={blobName}");
                    throw new FileNotFoundException("Blob doesn't exists", blobName);
                }

                return content;
            }
            catch (Exception ex)
            {
                logger.LogCritical("Failed to get blob content.", ex);
                throw;
            }
        }
    }
}
