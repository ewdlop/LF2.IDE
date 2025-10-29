using LF2IDE.BlazorServer.Models;

namespace LF2IDE.BlazorServer.Services;

public class EditorStateService
{
    public event Action? OnChange;
    
    public string? CurrentFilePath { get; private set; }
    public LF2DataFile? CurrentDataFile { get; private set; }
    public List<string> OpenFiles { get; private set; } = new();
    public string? SelectedDirectory { get; private set; }

    public void SetCurrentFile(string filePath, LF2DataFile? dataFile = null)
    {
        CurrentFilePath = filePath;
        CurrentDataFile = dataFile;
        
        if (!string.IsNullOrEmpty(filePath) && !OpenFiles.Contains(filePath))
        {
            OpenFiles.Add(filePath);
        }
        
        NotifyStateChanged();
    }

    public void CloseFile(string filePath)
    {
        OpenFiles.Remove(filePath);
        
        if (CurrentFilePath == filePath)
        {
            CurrentFilePath = OpenFiles.LastOrDefault();
            CurrentDataFile = null;
        }
        
        NotifyStateChanged();
    }

    public void SetSelectedDirectory(string directory)
    {
        SelectedDirectory = directory;
        NotifyStateChanged();
    }

    public void UpdateCurrentDataFile(LF2DataFile dataFile)
    {
        CurrentDataFile = dataFile;
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}

