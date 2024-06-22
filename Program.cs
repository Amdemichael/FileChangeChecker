class Program
{
    private static string targetFile;
    private static string lastReadContent;

    static void Main(string[] args)
    {
        Console.WriteLine("Enter the path to the target text file:");
        string filePath = Console.ReadLine();

        FileChecker monitor = new FileChecker(filePath);
        monitor.StartMonitoring();
    }
}
