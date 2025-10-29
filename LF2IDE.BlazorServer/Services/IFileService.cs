namespace LF2IDE.BlazorServer.Services;

public interface IFileService
{
    Task<string> ReadFileAsync(string filePath);
    Task WriteFileAsync(string filePath, string content);
    Task<bool> FileExistsAsync(string filePath);
    Task<IEnumerable<string>> GetFilesAsync(string directory, string searchPattern = "*.*");
    Task<IEnumerable<string>> GetDirectoriesAsync(string directory);
}

