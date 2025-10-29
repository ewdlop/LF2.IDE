namespace LF2IDE.BlazorServer.Services;

public class FileService(IWebHostEnvironment environment, ILogger<FileService> logger) : IFileService
{
    private readonly IWebHostEnvironment _environment = environment;
    private readonly ILogger<FileService> _logger = logger;

	public async Task<string> ReadFileAsync(string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
            {
                _logger.LogWarning($"File not found: {filePath}");
                return string.Empty;
            }

            return await File.ReadAllTextAsync(filePath);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error reading file: {filePath}");
            throw;
        }
    }

    public async Task WriteFileAsync(string filePath, string content)
    {
        try
        {
            var directory = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            await File.WriteAllTextAsync(filePath, content);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error writing file: {filePath}");
            throw;
        }
    }

    public Task<bool> FileExistsAsync(string filePath)
    {
        return Task.FromResult(File.Exists(filePath));
    }

    public Task<IEnumerable<string>> GetFilesAsync(string directory, string searchPattern = "*.*")
    {
        try
        {
            if (!Directory.Exists(directory))
            {
                return Task.FromResult(Enumerable.Empty<string>());
            }

            var files = Directory.GetFiles(directory, searchPattern);
            return Task.FromResult(files.AsEnumerable());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting files from directory: {directory}");
            return Task.FromResult(Enumerable.Empty<string>());
        }
    }

    public Task<IEnumerable<string>> GetDirectoriesAsync(string directory)
    {
        try
        {
            if (!Directory.Exists(directory))
            {
                return Task.FromResult(Enumerable.Empty<string>());
            }

            var directories = Directory.GetDirectories(directory);
            return Task.FromResult(directories.AsEnumerable());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting directories from: {directory}");
            return Task.FromResult(Enumerable.Empty<string>());
        }
    }
}

