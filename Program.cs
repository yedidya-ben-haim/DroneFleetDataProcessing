using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using DroneFleetDataProcessing.FileHandling;
using DroneFleetDataProcessing.Models.Sensors;
using DroneFleetDataProcessing.Exceptions;
using DroneFleetDataProcessing.ReportLogger;
using DroneFleetDataProcessing.Queries;
using DroneFleetDataProcessing.Validators;


namespace DroneFleetDataProcessing.Pipeline;

class Program
{
    public static void ShowNonOpertionalDrones(ICommandLogger logger, List<Drone> drones, DroneAnalyzer analysReport)
    {
        List<Drone> result = analysReport.GetNonOpertionalDrone(drones);
        foreach(var drone in result)
        {
            logger.log($"{drone.serialNumber} | {drone.model} | {drone.base_location} | {drone.status}");
        }
    }
    public static void ShowTopFiveDronesFlightByHours(ICommandLogger logger, List<Drone> drones, DroneAnalyzer analyzer)
    {
        List<Drone> result = analyzer.GetTopFiveFlightHouers(drones);
        int index = 1;
        foreach (var drone in result)
        {
            logger.log($"{index}. {drone.serialNumber} | {drone.model} | {drone.flightHours}");
            index++;
        }
    }
    public static void ShowAvailableDroneModels(ICommandLogger logger, List<Drone> drones, DroneAnalyzer analyzer)
    {
        List<string> models = analyzer.GetAvailableDroneModels(drones);
        foreach (var model in models)
        {
            logger.log(model);
        }
    }
    public static void ShowDronesByBase(ICommandLogger logger, List<Drone> drones, DroneAnalyzer analyzer)
    {
        Dictionary<string, int> bases = analyzer.GetDroneCountInEachBase(drones);
        string[] requiredBases = { "North", "South", "Central", "East", "West" };

        foreach (var baseName in requiredBases)
        {
            int count = bases.ContainsKey(baseName) ? bases[baseName] : 0;
            logger.log($"{baseName}: {count}");
        }
    }
    public static void ShowAverageBatteryHealthByModel(ICommandLogger logger, List<Drone> drones, DroneAnalyzer analyzer)
    {
        Dictionary<string, double> healths = analyzer.GetAverageBatteryHealthPerModel(drones);
        foreach (var pair in healths)
        {
            logger.log($"{pair.Key}: {pair.Value}");
        }
    }
    public static void ShowModelWithHighestCompletedMissions(ICommandLogger logger, List<Drone> drones, DroneAnalyzer analyzer)
    {
        string? topModel = analyzer.GetModelWithMostCompletedMissions(drones);

        if (topModel != null)
        {
            int totalMissions = 0;
            foreach (var d in drones)
            {
                if (d.model == topModel)
                {
                    totalMissions += d.missionsCompleted;
                }
            }

            logger.log($"Model: {topModel}");
            logger.log($"Total completed missions: {totalMissions}");
        }
    }

    public static void AnalysisReport(List<Drone> drones)
    {

        string reportFileOutput = Path.Combine("input", "output", "analysis_report.txt");

        string? directory = Path.GetDirectoryName(reportFileOutput);
        if (directory != null && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        ICommandLogger logger = new FileLogger(reportFileOutput);
        ValidationResult validResult = new ValidationResult(drones, 8);
        DroneAnalyzer analysReport = new DroneAnalyzer();


        logger.log("DRONE FLEET ANALYSIS REPORT");
        logger.log("");
        logger.log("PROCESSING SUMMARY");
        logger.log($"Total raw records: {drones.Count}");
        logger.log($"Valid records: {validResult.ValidDrones.Count}");
        logger.log($"Rejected records: {validResult.RejectedCount}");
        logger.log("");

        logger.log("NON-OPERATIONAL DRONES");
        ShowNonOpertionalDrones(logger, drones, analysReport);
        logger.log("");

        logger.log("TOP 5 DRONES BY FLIGHT HOURS");
        ShowTopFiveDronesFlightByHours(logger, drones, analysReport);
        logger.log("");

        logger.log("AVAILABLE DRONE MODELS");
        ShowAvailableDroneModels(logger, drones, analysReport);
        logger.log("");

        logger.log("DRONES BY BASE");
        ShowDronesByBase(logger, drones, analysReport);
        logger.log("");

        logger.log("AVERAGE BATTERY HEALTH BY MODEL");
        ShowAverageBatteryHealthByModel(logger, drones, analysReport);
        logger.log("");

        logger.log("MODEL WITH HIGHEST TOTAL COMPLETED MISSIONS");
        ShowModelWithHighestCompletedMissions(logger, drones, analysReport);

    }

    static void Main()
    {
        string filePath = Path.Combine("input", "raw", "drones_raw.json");

        try
        {
            List<Drone> drones = LoadFromJson.loadFromJson(filePath);

            Console.WriteLine($"load succeeded {drones.Count} was loaded");

            //AnalysisReport(drones);
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"Error file in path {filePath} was not found: {ex.Message}");
        }
        catch (FileIsEmptyOrWhiteSpace ex)
        {
            Console.WriteLine($"Error file is empty: {ex.Message}");
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Error file has some problems: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error occurred: {ex.Message}");
        }
    }
}