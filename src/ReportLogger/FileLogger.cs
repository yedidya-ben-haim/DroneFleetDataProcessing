using System;
using System.IO;
namespace DroneFleetDataProcessing.ReportLogger;
public class FileLogger : ICommandLogger
{
    private readonly string filePath;

    public FileLogger(string path)
    {
        filePath = path;
        File.WriteAllText(filePath, string.Empty);
    }

    public void log(string str)
    { 
        File.AppendAllText(filePath, $"{str}{Environment.NewLine}");
    }
}