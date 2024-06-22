using System;
using System.IO;

public class FileChangeDetector
{
    private string filePath;

    public FileChangeDetector(string filePath)
    {
        this.filePath = filePath;
    }

    public bool HasFileChanged(string lastReadContent)
    {
        string currentContent = File.ReadAllText(filePath);
        return currentContent != lastReadContent;
    }

    public void DisplayChanges(string lastReadContent)
    {
        string[] oldLines = lastReadContent.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        string[] newLines = File.ReadAllText(filePath).Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < Math.Max(oldLines.Length, newLines.Length); i++)
        {
            if (i >= oldLines.Length)
            {
                PrintColoredLine($"Added: {newLines[i]}", ConsoleColor.Green);
            }
            else if (i >= newLines.Length)
            {
                PrintColoredLine($"Removed: {oldLines[i]}", ConsoleColor.Red);
            }
            else if (oldLines[i] != newLines[i])
            {
                PrintColoredLine($"Changed: {oldLines[i]} -> {newLines[i]}", ConsoleColor.Yellow);
            }
        }
    }

    private void PrintColoredLine(string message, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}
