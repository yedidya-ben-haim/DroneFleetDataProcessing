using System;
using System.IO;
namespace DroneFleetDataProcessing.ReportLogger;
public class FileLogger : ICommandLogger
{
    private readonly string filePath;

    public FileLogger(string path)
    {
        filePath = path;
    }

    public void log(string str)
    { 
        File.AppendAllText(filePath, $"{str}{Environment.NewLine}");
    }
}