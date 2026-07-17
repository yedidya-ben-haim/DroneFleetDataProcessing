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
    private readonly IDroneDataLoader _dataLoader;

    public ProcessPipeline(ICommandLogger consoleLogger, IDroneDataLoader dataLoader)
    {
        _consoleLogger = consoleLogger;
        _dataLoader = dataLoader;
    }

    public void Run(string rawFilePath, string pathOfCleanJson, string reportFilePath)
    {
        _consoleLogger.log("=== Drone Fleet Data Processing System ===");
        _consoleLogger.log("");
        _consoleLogger.log("Step 1: Reading raw data...");
        List<Drone> rawDrones;
        try
        {
            rawDrones = _dataLoader.LoadData();
            _consoleLogger.log($"Read {rawDrones.Count} records from raw file");
        }
        catch (FileNotFoundException ex)
        {
            _consoleLogger.log($"Error: FileNotFoundException The file was not found: {ex.Message}");
            return;
        }
        catch (UnauthorizedAccessException ex)
        {
            _consoleLogger.log($"Error: UnauthorizedAccessException Read permission denied: {ex.Message}");
            return;
        }
        catch (FileIsEmptyOrWhiteSpace ex)
        {
            _consoleLogger.log($"Error: FileIsEmptyOrWhiteSpace The JSON file is empty or contains only whitespace: {ex.Message}");
            return;
        }
        catch (JsonException)
        {
            _consoleLogger.log("Error: JsonException The JSON file is malformed or has an invalid structure");
            return;
        }
        catch (InvalidDataException ex)
        {
            _consoleLogger.log($"Error: InvalidDataException Invalid data structures encountered: {ex.Message}");
            return;
        }
        catch (IOException ex)
        {
            _consoleLogger.log($"Error: IOException Failed to write clean JSON file (Disk full or file locked): {ex.Message}");
            return;
        }

        _consoleLogger.log("");
        _consoleLogger.log("Step 2: Validating data and creating clean dataset...");
        
        ValidationResult validResult = DroneCollectionValidator.ValidateAll(rawDrones);
        
        _consoleLogger.log($"Valid records: {validResult.ValidDrones.Count}");
        _consoleLogger.log($"Rejected records: {validResult.RejectedCount}");

        if (validResult.ValidDrones.Count == 0)
        {
            _consoleLogger.log("Error: No valid records found for analysis!");
            return;
        }

        _consoleLogger.log("");
        _consoleLogger.log("Step 3: Saving clean data...");
        try
        {
            LoadFromJson.SaveToJson(pathOfCleanJson, validResult.ValidDrones);
            string fullCleanPath = Path.GetFullPath(pathOfCleanJson);
            _consoleLogger.log($"Clean data saved to: {fullCleanPath}");
        }
        catch (UnauthorizedAccessException ex)
        {
            _consoleLogger.log($"Error: UnauthorizedAccessException Write permission denied to path: {ex.Message}");
            return;
        }
        catch (DirectoryNotFoundException ex)
        {
            _consoleLogger.log($"Error: DirectoryNotFoundException The output directory does not exist: {ex.Message}");
            return;
        }

        _consoleLogger.log("");
        _consoleLogger.log("Step 4: Reloading clean data...");
        List<Drone> cleanDrones;
        try
        {
            IDroneDataLoader cleanDataLoader = new LoadFromJson(pathOfCleanJson);
            cleanDrones = cleanDataLoader.LoadData();
            _consoleLogger.log($"Loaded {cleanDrones.Count} records from clean dataset");
        }
        catch (FileNotFoundException ex)
        {
            _consoleLogger.log($"Error: FileNotFoundException The file was not found: {ex.Message}");
            return;
        }
        catch (UnauthorizedAccessException ex)
        {
            _consoleLogger.log($"Error: UnauthorizedAccessException Read permission denied: {ex.Message}");
            return;
        }
        catch (FileIsEmptyOrWhiteSpace ex)
        {
            _consoleLogger.log($"Error: FileIsEmptyOrWhiteSpace The JSON file is empty or contains only whitespace: {ex.Message}");
            return;
        }
        catch (JsonException)
        {
            _consoleLogger.log("Error: JsonException The JSON file is malformed or has an invalid structure");
            return;
        }
        catch (InvalidDataException ex)
        {
            _consoleLogger.log($"Error: InvalidDataException Invalid data structures encountered: {ex.Message}");
            return;
        }
        catch (IOException ex)
        {
            _consoleLogger.log($"Error: IOException Failed to write clean JSON file (Disk full or file locked): {ex.Message}");
            return;
        }

        _consoleLogger.log("");
        _consoleLogger.log("Step 5: Performing analysis...");
        
        DroneAnalyzer analyzer = new DroneAnalyzer();
        
        _consoleLogger.log("Analysis completed successfully");

        _consoleLogger.log("");
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
        ReportGenerator.ShowNonOpertionalDrones(fileLogger, cleanDrones, analyzer);
        fileLogger.log("");

        fileLogger.log("TOP 5 DRONES BY FLIGHT HOURS");
        ReportGenerator.ShowTopFiveDronesFlightByHours(fileLogger, cleanDrones, analyzer);
        fileLogger.log("");

        fileLogger.log("AVAILABLE DRONE MODELS");
        ReportGenerator.ShowAvailableDroneModels(fileLogger, cleanDrones, analyzer);
        fileLogger.log("");

        fileLogger.log("DRONES BY BASE");
        ReportGenerator.ShowDronesByBase(fileLogger, cleanDrones, analyzer);
        fileLogger.log("");

        fileLogger.log("AVERAGE BATTERY HEALTH BY MODEL");
        ReportGenerator.ShowAverageBatteryHealthByModel(fileLogger, cleanDrones, analyzer);
        fileLogger.log("");

        fileLogger.log("MODEL WITH HIGHEST TOTAL COMPLETED MISSIONS");
        ReportGenerator.ShowModelWithHighestCompletedMissions(fileLogger, cleanDrones, analyzer);
        fileLogger.log("");

        fileLogger.log("SELECTED ADDITIONAL ANALYSIS");
        fileLogger.log($"Analysis name: Top Three Models By Average Flight Hours");
        ReportGenerator.ShowTopThreeModelsByAverageFlightHours(fileLogger, cleanDrones, analyzer);

    }
}
