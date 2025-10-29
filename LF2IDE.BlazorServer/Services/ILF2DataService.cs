using LF2IDE.BlazorServer.Models;

namespace LF2IDE.BlazorServer.Services;

public interface ILF2DataService
{
    string Decrypt(byte[] data, string? password = null);
    byte[] Encrypt(string text, string? password = null);
    Task<LF2DataFile> LoadDataFileAsync(string filePath);
    Task SaveDataFileAsync(LF2DataFile dataFile);
    List<Frame> ParseFrames(string content);
    List<SpriteSheet> ParseSprites(string content);
}

