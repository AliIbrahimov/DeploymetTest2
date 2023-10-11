
using CoreLayer.Utilities.Results;
using Microsoft.AspNetCore.Http;

namespace CoreLayer.Utilities.Extention;

public static class FileExtension
{
    public static string UploadFile(this IFormFile file, string env, string path)
    {
        string imagename = Guid.NewGuid() + file.FileName;
        string fullPath = Path.Combine(env, path, imagename);
        using (FileStream fileStream = new FileStream(fullPath, FileMode.Create))
        {
            file.CopyTo(fileStream);
        }

        return imagename;
    }
    public static void DeleteFile(string env, string path, string fileName)
    {
        string fullPath = Path.Combine(env, path, fileName);
        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }
    }

}
