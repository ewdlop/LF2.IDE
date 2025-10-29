namespace LF2IDE.BlazorServer.Models;

public class LF2DataFile
{
    public string FilePath { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public DataType DataType { get; set; }
    public string Content { get; set; } = string.Empty;
    public bool IsEncrypted { get; set; }
    public bool IsModified { get; set; }
    
    // Metadata
    public string Name { get; set; } = string.Empty;
    public List<SpriteSheet> Sprites { get; set; } = new();
    public List<Frame> Frames { get; set; } = new();
    
    // Header info
    public string? Head { get; set; }
    public string? Small { get; set; }
    public int WalkingFrameRate { get; set; }
    public double WalkingSpeed { get; set; }
    public double WalkingSpeedZ { get; set; }
    public int RunningFrameRate { get; set; }
    public double RunningSpeed { get; set; }
    public double RunningSpeedZ { get; set; }
}

