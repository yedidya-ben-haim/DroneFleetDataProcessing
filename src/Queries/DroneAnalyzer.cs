using System;
using System.Collections.Generic;
using System.Text;
using DroneFleetDataProcessing.Models.Sensors;

namespace DroneFleetDataProcessing.Queries;
    public class DroneAnalyzer
    {
        public List<Drone> GetNonOpertionalDrone(List<Drone> drones)
        {
            if (drones == null) return [];
            return drones.Where(r => r.status != "Oprational").ToList();
        }
        public List<Drone> GetTopFiveFlightHouers(List<Drone> drones) 
        {
            if (drones == null) return [];
            return drones.OrderByDescending(r => r.flightHours).Take(5).ToList();
        }
        public List<string?> GetAvailableDroneModels(List<Drone> drones)
        {
            if (drones == null) return [];
            return drones.Select(r=>r.model).Where(m => m != null).Distinct().ToList();
        }
        public Dictionary<string,int> GetDroneCountInEachBase(List<Drone> drones) 
        {
        if (drones == null) return [];
        return drones.Where(r=>r.base_location!=null).GroupBy(r=>r.base_location).ToDictionary(g=>g.Key!,g=>g.Count());
        }
        public Dictionary<string, double> GetAverageBatteryHealthPerModel(List<Drone> drones)
        {
            if (drones == null) return [];
            return drones.Where(r => r.model != null).GroupBy(r => r.model).ToDictionary(group => group.Key!,group => group.Average(r => r.batteryHealth));
        }
        public string? GetModelWithMostCompletedMissions(List<Drone> drones)
        {
            if (drones == null || drones.Count == 0) return null;
            return drones.Where(r => r.model != null).GroupBy(r => r.model).OrderByDescending(group => group.Sum(r => r.missionsCompleted)).Select(g => g.Key).FirstOrDefault();
        }
        public List<string> GetTopThreeModelsByAverageFlightHours(List<Drone> drones)
        {
            if (drones == null) return [];

            return drones.Where(r => r.model != null).GroupBy(r => r.model).Select(
                g => new{Model = g.Key!, AverageHours = g.Average(r => r.flightHours)})
                .OrderByDescending(x => x.AverageHours).Take(3).Select(x => x.Model).ToList();
        }
}

