namespace LF2IDE.BlazorServer.Models;

public class SpriteSheet
{
    public string File { get; set; } = string.Empty;
    public int StartIndex { get; set; }
    public int EndIndex { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public int Row { get; set; }
    public int Col { get; set; }

    public override string ToString()
    {
        return $"file({StartIndex}-{EndIndex}): {File}  w: {Width}  h: {Height}  row: {Row}  col: {Col}";
    }
}

