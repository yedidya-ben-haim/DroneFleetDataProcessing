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

    public static void ShowTopThreeModelsByAverageFlightHours(ICommandLogger logger, List<Drone> drones, DroneAnalyzer analyzer)
    {
        List<string> topThreeModel = analyzer.GetTopThreeModelsByAverageFlightHours(drones);
        
        foreach (string model in topThreeModel)
        {
            logger.log(model);
        }
    }

}