using System.Text;
using System.Text.RegularExpressions;
using LF2IDE.BlazorServer.Models;

namespace LF2IDE.BlazorServer.Services;

public class LF2DataService : ILF2DataService
{
    private const string DefaultPassword = "odBearBecauseHeIsVeryGoodSiuHungIsAGo";
    private readonly IFileService _fileService;
    private readonly ILogger<LF2DataService> _logger;

    public LF2DataService(IFileService fileService, ILogger<LF2DataService> logger)
    {
        _fileService = fileService;
        _logger = logger;
    }

    public string Decrypt(byte[] data, string? password = null)
    {
        try
        {
            var pwd = password ?? DefaultPassword;
            if (data.Length <= 123)
            {
                return string.Empty;
            }

            var decryptedText = new byte[data.Length - 123];
            
            if (string.IsNullOrEmpty(pwd))
            {
                return Encoding.ASCII.GetString(data);
            }

            for (int i = 0, j = 123; i < decryptedText.Length; i++, j++)
            {
                decryptedText[i] = (byte)(data[j] - (byte)pwd[i % pwd.Length]);
            }

            return Encoding.ASCII.GetString(decryptedText);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error decrypting data");
            throw;
        }
    }

    public byte[] Encrypt(string text, string? password = null)
    {
        try
        {
            var pwd = password ?? DefaultPassword;
            var dat = new byte[123 + text.Length];

            // Initialize header with zeros
            for (int i = 0; i < 123; i++)
            {
                dat[i] = 0;
            }

            if (string.IsNullOrEmpty(pwd))
            {
                for (int i = 0, j = 123; i < text.Length; i++, j++)
                {
                    dat[j] = (byte)text[i];
                }
            }
            else
            {
                for (int i = 0, j = 123; i < text.Length; i++, j++)
                {
                    dat[j] = (byte)((byte)text[i] + (byte)pwd[i % pwd.Length]);
                }
            }

            return dat;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error encrypting data");
            throw;
        }
    }

    public async Task<LF2DataFile> LoadDataFileAsync(string filePath)
    {
        try
        {
            var dataFile = new LF2DataFile
            {
                FilePath = filePath,
                FileName = Path.GetFileName(filePath)
            };

            if (!await _fileService.FileExistsAsync(filePath))
            {
                _logger.LogWarning($"File not found: {filePath}");
                return dataFile;
            }

            var fileBytes = await File.ReadAllBytesAsync(filePath);
            
            // Check if file is encrypted (LF2 .dat files start with 123 bytes of header)
            if (fileBytes.Length > 123 && fileBytes[0] == 0)
            {
                dataFile.IsEncrypted = true;
                dataFile.Content = Decrypt(fileBytes);
            }
            else
            {
                dataFile.IsEncrypted = false;
                dataFile.Content = await _fileService.ReadFileAsync(filePath);
            }

            // Parse content
            dataFile.Sprites = ParseSprites(dataFile.Content);
            dataFile.Frames = ParseFrames(dataFile.Content);
            
            // Determine data type
            var extension = Path.GetExtension(filePath).ToLower();
            dataFile.DataType = extension == ".dat" ? DataType.Char : DataType.Background;

            return dataFile;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error loading data file: {filePath}");
            throw;
        }
    }

    public async Task SaveDataFileAsync(LF2DataFile dataFile)
    {
        try
        {
            if (dataFile.IsEncrypted)
            {
                var encrypted = Encrypt(dataFile.Content);
                await File.WriteAllBytesAsync(dataFile.FilePath, encrypted);
            }
            else
            {
                await _fileService.WriteFileAsync(dataFile.FilePath, dataFile.Content);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error saving data file: {dataFile.FilePath}");
            throw;
        }
    }

    public List<Frame> ParseFrames(string content)
    {
        var frames = new List<Frame>();
        
        try
        {
            var framePattern = @"<frame>\s*(\d+)\s*([^\r\n]*)\r?\n(.*?)<frame_end>";
            var matches = Regex.Matches(content, framePattern, RegexOptions.Singleline);

            foreach (Match match in matches)
            {
                var frame = new Frame
                {
                    Index = int.Parse(match.Groups[1].Value),
                    Name = match.Groups[2].Value.Trim(),
                    RawText = match.Groups[3].Value
                };

                // Parse frame properties
                ParseFrameProperties(frame, frame.RawText);
                frames.Add(frame);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error parsing frames");
        }

        return frames;
    }

    public List<SpriteSheet> ParseSprites(string content)
    {
        var sprites = new List<SpriteSheet>();

        try
        {
            var pattern = @"file\((\d+)-(\d+)\):\s*([^\s]+)\s+w:\s*(\d+)\s+h:\s*(\d+)\s+row:\s*(\d+)\s+col:\s*(\d+)";
            var matches = Regex.Matches(content, pattern);

            foreach (Match match in matches)
            {
                sprites.Add(new SpriteSheet
                {
                    StartIndex = int.Parse(match.Groups[1].Value),
                    EndIndex = int.Parse(match.Groups[2].Value),
                    File = match.Groups[3].Value,
                    Width = int.Parse(match.Groups[4].Value),
                    Height = int.Parse(match.Groups[5].Value),
                    Row = int.Parse(match.Groups[6].Value),
                    Col = int.Parse(match.Groups[7].Value)
                });
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error parsing sprites");
        }

        return sprites;
    }

    private void ParseFrameProperties(Frame frame, string content)
    {
        // Parse basic properties
        frame.Pic = ParseIntProperty(content, "pic");
        frame.State = ParseIntProperty(content, "state");
        frame.Wait = ParseIntProperty(content, "wait");
        frame.Next = ParseIntProperty(content, "next");
        frame.Dvx = ParseIntProperty(content, "dvx");
        frame.Dvy = ParseIntProperty(content, "dvy");
        frame.Dvz = ParseIntProperty(content, "dvz");
        frame.CenterX = ParseIntProperty(content, "centerx");
        frame.CenterY = ParseIntProperty(content, "centery");
        frame.HitA = ParseIntProperty(content, "hit_a");
        frame.HitD = ParseIntProperty(content, "hit_d");
        frame.HitJ = ParseIntProperty(content, "hit_j");

        // Parse sound
        var soundMatch = Regex.Match(content, @"sound:\s*([^\r\n]+)");
        if (soundMatch.Success)
        {
            frame.Sound = soundMatch.Groups[1].Value.Trim();
        }
    }

    private int ParseIntProperty(string content, string propertyName)
    {
        var pattern = $@"{propertyName}:\s*(-?\d+)";
        var match = Regex.Match(content, pattern);
        return match.Success ? int.Parse(match.Groups[1].Value) : 0;
    }
}

