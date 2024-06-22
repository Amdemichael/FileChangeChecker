using System;
using System.IO;
using System.Threading;

public class FileChecker
{
    private string targetFile;
    private FileChangeDetector changeDetector;
    private string lastReadContent;

    public FileChecker(string filePath)
    {
        targetFile = filePath;
        changeDetector = new FileChangeDetector(targetFile);
        lastReadContent = File.ReadAllText(targetFile);
    }

    public void StartMonitoring()
    {
        if (!File.Exists(targetFile))
        {
            Console.WriteLine("File does not exist.");
            return;
        }

        Timer timer = new Timer(CheckFileChanges, null, 0, 15000);

        Console.WriteLine($"Monitoring changes to '{Path.GetFileName(targetFile)}'.");
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }

    private void CheckFileChanges(object state)
    {
        try
        {
            if (changeDetector.HasFileChanged(lastReadContent))
            {
                Console.WriteLine($"File changed at {DateTime.Now}:");
                Console.WriteLine("Changes:");
                changeDetector.DisplayChanges(lastReadContent);
                lastReadContent = File.ReadAllText(targetFile); // Update last read content
            }
            else
            {
                Console.WriteLine($"No changes detected at {DateTime.Now}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading file: {ex.Message}");
        }
    }
}
