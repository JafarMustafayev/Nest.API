namespace Nest.Application.Helper.FileDatasChecker;

public static class ImageDatasChecker
{
    public static bool CheckType(IFormFile file)
    {
        return file.ContentType.Contains("image");
    }

    public static bool CheckSize(IFormFile file)
    {
        return file.Length <= 5000000;
    }
}