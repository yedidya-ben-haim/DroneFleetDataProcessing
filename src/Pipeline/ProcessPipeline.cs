//using System.Text.Json;
//using DroneFleetDataProcessing.Exceptions;
//using DroneFleetDataProcessing.FileHandling;
//using DroneFleetDataProcessing.Models.Sensors;
//using DroneFleetDataProcessing.ReportLogger;
//using DroneFleetDataProcessing.Validators;

//namespace DroneFleetDataProcessing.Pipeline;

//public class ProcessPipeline
//{
//    private readonly ICommandLogger _logger;

//    public ProcessPipeline(ICommandLogger logger)
//    {
//        _logger = logger;
//    }

//    public void Run(string rawFilePath,string pathOfCleanJson,string reportFilePath)
//    {

//        _logger.log("=== Drone Fleet Data Processing System ===");
//        _logger.log("Step 1: Reading raw data...");

//        try
//        {
//            List<Drone> drones = LoadFromJson.LoadJson(rawFilePath);

//            _logger.log($"Read {drones.Count} records from raw file");
//            ValidationResult validReports = DroneCollectionValidator.ValidateAll(drones);
//            LoadFromJson.SaveToJson(pathOfCleanJson,validReports.ValidDrones);
//        }
//        catch (FileNotFoundException ex)
//        {
//            _logger.log($"Error: File not found - {ex.Message}");
//        }
//        catch (UnauthorizedAccessException ex)
//        {
//            _logger.log($"Error: Read permission denied - {ex.Message}");
//        }
//        catch (FileIsEmptyOrWhiteSpace ex)
//        {
//            _logger.log($"Error: Empty file - {ex.Message}");
//        }
//        catch (JsonException ex)
//        {
//            _logger.log($"Error: Invalid JSON - {ex.Message}");
//        }
//        catch (InvalidDataException ex)
//        {
//            _logger.log($"Error: Invalid data - {ex.Message}");
//        }
//        catch (IOException ex)
//        {
//            _logger.log($"Error: File reading failed - {ex.Message}");
//        }
//    }
//}




using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using DroneFleetDataProcessing.Exceptions;
using DroneFleetDataProcessing.FileHandling;
using DroneFleetDataProcessing.Models.Sensors;
using DroneFleetDataProcessing.ReportLogger;
using DroneFleetDataProcessing.Validators;
using DroneFleetDataProcessing.Queries;

namespace DroneFleetDataProcessing.Pipeline;

public class ProcessPipeline
{
    private readonly ICommandLogger _consoleLogger;

    public ProcessPipeline(ICommandLogger consoleLogger)
    {
        _consoleLogger = consoleLogger;
    }

    public void Run(string rawFilePath, string pathOfCleanJson, string reportFilePath)
    {
        _consoleLogger.log("=== Drone Fleet Data Processing System ===");

        _consoleLogger.log("Step 1: Reading raw data...");
        List<Drone> rawDrones;
        try
        {
            rawDrones = LoadFromJson.LoadJson(rawFilePath);
            _consoleLogger.log($"Read {rawDrones.Count} records from raw file");
        }
        catch (Exception ex)
        {
            _consoleLogger.log($"Error: {ex.GetType().Name} {ex.Message}");
            return;
        }

        _consoleLogger.log("Step 2: Validating data and creating clean dataset...");
        
        ValidationResult validResult = DroneCollectionValidator.ValidateAll(rawDrones);
        
        _consoleLogger.log($"Valid records: {validResult.ValidDrones.Count}");
        _consoleLogger.log($"Rejected records: {validResult.RejectedCount}");

        if (validResult.ValidDrones.Count == 0)
        {
            _consoleLogger.log("Error: No valid records found for analysis!");
            return;
        }

        _consoleLogger.log("Step 3: Saving clean data...");
        try
        {
            LoadFromJson.SaveToJson(pathOfCleanJson, validResult.ValidDrones);
            string fullCleanPath = Path.GetFullPath(pathOfCleanJson);
            _consoleLogger.log($"Clean data saved to: <{fullCleanPath}>");
        }
        catch (Exception ex)
        {
            _consoleLogger.log($"Error: Failed to write output clean JSON file - {ex.Message}");
            return;
        }

        _consoleLogger.log("Step 4: Reloading clean data...");
        List<Drone> cleanDrones;
        try
        {
            cleanDrones = LoadFromJson.LoadJson(pathOfCleanJson);
            _consoleLogger.log($"Loaded {cleanDrones.Count} records from clean dataset");
        }
        catch (Exception ex)
        {
            _consoleLogger.log($"Error: Reload failed - {ex.Message}");
            return;
        }

        _consoleLogger.log("Step 5: Performing analysis...");
        
        DroneAnalyzer analyzer = new DroneAnalyzer();
        
        _consoleLogger.log("Analysis completed successfully");

        _consoleLogger.log("Step 6: Generating report...");
        try
        {
            string? outputDir = Path.GetDirectoryName(reportFilePath);
            if (outputDir != null && !Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }

            ICommandLogger fileLogger = new FileLogger(reportFilePath);

            GenerateFileReport(fileLogger, rawDrones.Count, validResult, cleanDrones, analyzer);

            string fullReportPath = Path.GetFullPath(reportFilePath);
            _consoleLogger.log($"Report generated successfully: {fullReportPath}");
        }
        catch (Exception ex)
        {
            _consoleLogger.log($"Error: Report generation failed  {ex.Message}");
            return;
        }

        _consoleLogger.log("=== Process completed successfully! ===");
    }

    private void GenerateFileReport(ICommandLogger fileLogger, int totalRawCount, ValidationResult validResult, List<Drone> cleanDrones, DroneAnalyzer analyzer)
    {
        fileLogger.log("DRONE FLEET ANALYSIS REPORT");
        fileLogger.log("");
        fileLogger.log("PROCESSING SUMMARY");
        fileLogger.log($"Total raw records: {totalRawCount}");
        fileLogger.log($"Valid records: {validResult.ValidDrones.Count}");
        fileLogger.log($"Rejected records: {validResult.RejectedCount}");
        fileLogger.log("");

        fileLogger.log("NON-OPERATIONAL DRONES");
        Program.ShowNonOpertionalDrones(fileLogger, cleanDrones, analyzer);
        fileLogger.log("");

        fileLogger.log("TOP 5 DRONES BY FLIGHT HOURS");
        Program.ShowTopFiveDronesFlightByHours(fileLogger, cleanDrones, analyzer);
        fileLogger.log("");

        fileLogger.log("AVAILABLE DRONE MODELS");
        Program.ShowAvailableDroneModels(fileLogger, cleanDrones, analyzer);
        fileLogger.log("");

        fileLogger.log("DRONES BY BASE");
        Program.ShowDronesByBase(fileLogger, cleanDrones, analyzer);
        fileLogger.log("");

        fileLogger.log("AVERAGE BATTERY HEALTH BY MODEL");
        Program.ShowAverageBatteryHealthByModel(fileLogger, cleanDrones, analyzer);
        fileLogger.log("");

        fileLogger.log("MODEL WITH HIGHEST TOTAL COMPLETED MISSIONS");
        Program.ShowModelWithHighestCompletedMissions(fileLogger, cleanDrones, analyzer);
    }
}
