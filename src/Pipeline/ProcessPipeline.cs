using System.Text.Json;
using DroneFleetDataProcessing.Exceptions;
using DroneFleetDataProcessing.FileHandling;
using DroneFleetDataProcessing.Models.Sensors;
using DroneFleetDataProcessing.ReportLogger;

namespace DroneFleetDataProcessing.Pipeline;

public class ProcessPipeline
{
    private readonly ICommandLogger _logger;

    public ProcessPipeline(ICommandLogger logger)
    {
        _logger = logger;
    }

    public void Run(string rawFilePath)
    {
        _logger.log("=== Drone Fleet Data Processing System ===");
        _logger.log("Step 1: Reading raw data...");

        try
        {
            List<Drone> drones = LoadFromJson.LoadJson(rawFilePath);

            _logger.log($"Read {drones.Count} records from raw file");

        }
        catch (FileNotFoundException ex)
        {
            _logger.log($"Error: File not found - {ex.Message}");
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.log($"Error: Read permission denied - {ex.Message}");
        }
        catch (FileIsEmptyOrWhiteSpace ex)
        {
            _logger.log($"Error: Empty file - {ex.Message}");
        }
        catch (JsonException ex)
        {
            _logger.log($"Error: Invalid JSON - {ex.Message}");
        }
        catch (InvalidDataException ex)
        {
            _logger.log($"Error: Invalid data - {ex.Message}");
        }
        catch (IOException ex)
        {
            _logger.log($"Error: File reading failed - {ex.Message}");
        }
    }
}