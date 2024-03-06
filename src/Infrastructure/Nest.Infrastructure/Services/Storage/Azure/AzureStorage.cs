namespace Nest.Infrastructure.Services.Storage.Azure;

public class AzureStorage : IAzureStorage
{
    private readonly BlobServiceClient _blobServiceClient;
    private BlobContainerClient _blobContainerClient;

    public AzureStorage(BlobServiceClient blobServiceClient, BlobContainerClient blobContainerClient)
    {
        _blobServiceClient = blobServiceClient;
        _blobContainerClient = blobContainerClient;
    }

    public async Task DeleteAsync(string fileName)
    {
        // _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        BlobClient blobClient = _blobContainerClient.GetBlobClient(fileName);
        await blobClient.DeleteAsync();
    }

    public List<string> GetFiles(string containerName)
    {
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        return _blobContainerClient.GetBlobs().Select(b => b.Name).ToList();
    }

    public bool HasFile(string containerName, string fileName)
    {
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        return _blobContainerClient.GetBlobs().Any(b => b.Name == fileName);
    }

    public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string containerName, IFormFileCollection files)
    {
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        await _blobContainerClient.CreateIfNotExistsAsync();
        await _blobContainerClient.SetAccessPolicyAsync(PublicAccessType.BlobContainer);

        List<(string fileName, string pathOrContainerName)> datas = new();
        foreach (IFormFile file in files)
        {
            string fileNewName = Guid.NewGuid().ToString() + Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            BlobClient blobClient = _blobContainerClient.GetBlobClient(fileNewName);
            await blobClient.UploadAsync(file.OpenReadStream());
            datas.Add((fileNewName, $"{containerName}/{fileNewName}"));
        }
        return datas;
    }

    public async Task<(string fileName, string pathOrContainerName)> UploadAsync(string containerName, IFormFile file)
    {
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        await _blobContainerClient.CreateIfNotExistsAsync();
        await _blobContainerClient.SetAccessPolicyAsync(PublicAccessType.BlobContainer);

        (string fileName, string pathOrContainerName) data = new();

        string fileNewName = Guid.NewGuid().ToString() + Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

        BlobClient blobClient = _blobContainerClient.GetBlobClient(fileNewName);
        await blobClient.UploadAsync(file.OpenReadStream());
        data.fileName = fileNewName;
        data.pathOrContainerName = $"{containerName}/{fileNewName}";

        return data;
    }
}