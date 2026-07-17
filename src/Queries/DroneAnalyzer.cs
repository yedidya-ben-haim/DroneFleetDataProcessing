using DroneFleetDataProcessing.Models.Sensors;

namespace DroneFleetDataProcessing.Queries;

public class DroneAnalyzer
{
    public List<Drone> GetNonOpertionalDrone(List<Drone> drones)
    {
        if (drones == null) return [];
        return drones.Where(r => r.status != "Operational").ToList();
    }
    public List<Drone> GetTopFiveFlightHouers(List<Drone> drones)
    {
        if (drones == null) return [];
        return drones.OrderByDescending(r => r.flightHours).Take(5).ToList();
    }
    public List<string> GetAvailableDroneModels(List<Drone> drones)
    {
        if (drones == null) return [];
        return drones.Where(m => m != null).Select(r => r.model!).Distinct().ToList();
    }
    public Dictionary<string, int> GetDroneCountInEachBase(List<Drone> drones)
    {
        if (drones == null) return [];
        return drones.Where(r => r.base_location != null).GroupBy(r => r.base_location).ToDictionary(g => g.Key!, g => g.Count());
    }
    public Dictionary<string, double> GetAverageBatteryHealthPerModel(List<Drone> drones)
    {
        if (drones == null) return [];
        return drones.Where(r => r.model != null).GroupBy(r => r.model).ToDictionary(group => group.Key!, group => group.Average(r => r.batteryHealth));
    }
    public string? GetModelWithMostCompletedMissions(List<Drone> drones)
    {
        if (drones == null || drones.Count == 0) return null;
        return drones.Where(r => r.model != null).GroupBy(r => r.model).OrderByDescending(group => group.Sum(r => r.missionsCompleted)).Select(g => g.Key).FirstOrDefault();
    }
    public List<string> GetTopThreeModelsByAverageFlightHours(List<Drone> drones)
    {
        if (drones == null)
            return [];

        return drones
            .Where(drone => drone.model != null)
            .GroupBy(drone => drone.model)
            .Select(g => new
            {
                Model = g.Key!,
                AverageHours = g.Average(drone => drone.flightHours)
            })
            .OrderByDescending(result => result.AverageHours)
            .Take(3)
            .Select(result => result.Model)
            .ToList();
    }
}

