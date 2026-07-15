namespace DroneFleetDataProcessing.ReportLogger;

public class ConsoleLogger : ICommandLogger
{
    public void log(string message)
    {
        Console.WriteLine(message);
    }
}