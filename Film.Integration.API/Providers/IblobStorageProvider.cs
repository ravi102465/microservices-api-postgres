namespace Film.Integration.API.Providers
{
    public interface IBlobStorageProvider
    {
        Task<Stream> GetBlobContent(string containerName, string blobName);
    }
}
