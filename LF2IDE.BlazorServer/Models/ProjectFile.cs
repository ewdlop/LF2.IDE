namespace LF2IDE.BlazorServer.Models;

public class ProjectFile
{
    public string Name { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
    public string RootDirectory { get; set; } = string.Empty;
    public List<string> OpenFiles { get; set; } = new();
    public string? ActiveFile { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime LastModifiedDate { get; set; } = DateTime.Now;
}

