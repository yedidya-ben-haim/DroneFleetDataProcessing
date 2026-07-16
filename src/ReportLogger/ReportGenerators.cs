using DroneFleetDataProcessing.Models.Sensors;
using DroneFleetDataProcessing.Queries;
using DroneFleetDataProcessing.ReportLogger;
using DroneFleetDataProcessing.Validators;
using System;
using System.Collections.Generic;

namespace DroneFleetDataProcessing.ReportLogger;
public static class ReportGenerator
{
    public static void ShowNonOpertionalDrones(ICommandLogger logger, List<Drone> drones, DroneAnalyzer analysReport)
    {
        List<Drone> result = analysReport.GetNonOpertionalDrone(drones);
        foreach (var drone in result)
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

    public static void AnalysisReport(List<Drone> drones, ICommandLogger logger)
    {
        ValidationResult validResult = DroneCollectionValidator.ValidateAll(drones);

        logger.log($"Read {validResult.ValidDrones.Count} records from raw file");

        DroneAnalyzer analysReport = new DroneAnalyzer();

        //logger.log("PROCESSING SUMMARY");
        //logger.log($"Total raw records: {drones.Count}");
        //logger.log($"Valid records: {validResult.ValidDrones.Count}");
        //logger.log($"Rejected records: {validResult.RejectedCount}");
        //logger.log("");

        //logger.log("NON-OPERATIONAL DRONES");
        //ShowNonOpertionalDrones(logger, drones, analysReport);
        //logger.log("");

        //logger.log("TOP 5 DRONES BY FLIGHT HOURS");
        //ShowTopFiveDronesFlightByHours(logger, drones, analysReport);
        //logger.log("");

        //logger.log("AVAILABLE DRONE MODELS");
        //ShowAvailableDroneModels(logger, drones, analysReport);
        //logger.log("");

        //logger.log("DRONES BY BASE");
        //ShowDronesByBase(logger, drones, analysReport);
        //logger.log("");

        //logger.log("AVERAGE BATTERY HEALTH BY MODEL");
        //ShowAverageBatteryHealthByModel(logger, drones, analysReport);
        //logger.log("");

        //logger.log("MODEL WITH HIGHEST TOTAL COMPLETED MISSIONS");
        //ShowModelWithHighestCompletedMissions(logger, drones, analysReport);

    }
}