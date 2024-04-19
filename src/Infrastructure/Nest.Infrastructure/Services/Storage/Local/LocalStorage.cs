namespace Nest.Infrastructure.Services.Storage.Local;

public class LocalStorage : ILocalStorage
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public LocalStorage(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    public List<string> GetFiles(string path)
    {
        DirectoryInfo directory = new(path);
        return directory.GetFiles().Select(f => f.Name).ToList();
    }

    public async Task DeleteAsync(string fileName)
    {
        File.Delete(Path.Combine(_webHostEnvironment.WebRootPath, fileName));
    }

    public bool HasFile(string path, string fileName)
    {
        return File.Exists(Path.Combine(_webHostEnvironment.WebRootPath, path, fileName));
    }

    public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string path, IFormFileCollection files)
    {
        string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, FileContainerNameConsts.Uploads, path);
        if (!Directory.Exists(uploadPath))
            Directory.CreateDirectory(uploadPath);

        List<(string fileName, string path)> datas = new();
        foreach (IFormFile file in files)
        {
            string fileNewName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            await CopyFileAsync($"{uploadPath}\\{fileNewName}", file);
            datas.Add((fileNewName, $"{FileContainerNameConsts.Uploads}\\{path}\\{fileNewName}"));
        }

        return datas;
    }

    public async Task<(string fileName, string pathOrContainerName)> UploadAsync(string path, IFormFile file)
    {
        string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, FileContainerNameConsts.Uploads, path);
        if (!Directory.Exists(uploadPath))
            Directory.CreateDirectory(uploadPath);

        (string fileName, string path) data = new();

        string fileNewName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

        await CopyFileAsync($"{uploadPath}\\{fileNewName}", file);
        data.fileName = fileNewName;
        data.path = $"{FileContainerNameConsts.Uploads}\\{path}\\{fileNewName}";

        return data;
    }

    private async Task<bool> CopyFileAsync(string path, IFormFile file)
    {
        await using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);

        await file.CopyToAsync(fileStream);
        await fileStream.FlushAsync();
        return true;
    }
}