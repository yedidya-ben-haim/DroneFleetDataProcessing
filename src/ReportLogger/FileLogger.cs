using System;
using System.IO;
namespace DroneFleetDataProcessing.ReportLogger;

// file writing Logger
public class FileLogger : ICommandLogger
{
    private readonly string _filePath;

    public FileLogger(string path)
    {
        _filePath = path;
        File.WriteAllText(_filePath, string.Empty);
    }

    public void log(string str)
    { 
        File.AppendAllText(_filePath, $"{str}{Environment.NewLine}");
    }
}