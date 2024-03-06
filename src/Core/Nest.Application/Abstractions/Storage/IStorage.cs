namespace Nest.Application.Abstractions.Storage;

public interface IStorage
{
    Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string pathOrContainerName, IFormFileCollection files);

    Task<(string fileName, string pathOrContainerName)> UploadAsync(string pathOrContainerName, IFormFile file);

    Task DeleteAsync(string fileName);

    List<string> GetFiles(string pathOrContainerName);

    bool HasFile(string pathOrContainerName, string fileName);
}