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
        if (result.Count == 0)
        {
            logger.log("No results found.");
            return;
        }
        foreach (var drone in result)
        {
            logger.log($"{drone.serialNumber} | {drone.model} | {drone.base_location} | {drone.status}");
        }
    }
    public static void ShowTopFiveDronesFlightByHours(ICommandLogger logger, List<Drone> drones, DroneAnalyzer analyzer)
    {
        List<Drone> result = analyzer.GetTopFiveFlightHouers(drones);
        if (result.Count == 0)
        {
            logger.log("No results found.");
            return;
        }

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
        if (models.Count == 0)
        {
            logger.log("No results found.");
            return;
        }
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
        string[] requiredModels = { "Falcon-X", "Raven-M", "SkyEye-2", "CargoBee", "Storm-4", "Scout-Lite" };

        foreach (var model in requiredModels)
        {
            if (healths.ContainsKey(model))
            {
                logger.log($"{model}: {Math.Round(healths[model], 2)}");
            }
            else
            {
                logger.log($"{model}: N/A");
            }
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

    public static void ShowTopThreeModelsByAverageFlightHours(ICommandLogger logger, List<Drone> drones, DroneAnalyzer analyzer)
    {
        List<string> topThreeModel = analyzer.GetTopThreeModelsByAverageFlightHours(drones);
        
        foreach (string model in topThreeModel)
        {
            logger.log(model);
        }
    }

}