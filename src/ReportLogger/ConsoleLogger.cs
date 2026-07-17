namespace DroneFleetDataProcessing.ReportLogger;

// Console writing Logger
public class ConsoleLogger : ICommandLogger
{
    public void log(string message)
    {
        Console.WriteLine(message);
    }
}