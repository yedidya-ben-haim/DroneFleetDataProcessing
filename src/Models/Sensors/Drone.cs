namespace DroneFleetDataProcessing.Models.Sensors;

// Represents a drone-type sensor
public class Drone
{
    public int id { get; set; }
    public string? serialNumber { get; set; }
    public string? model { get; set; }
    public string? category { get; set; }
    public string? base_location { get; set; }
    public double flightHours { get; set; }
    public int batteryHealth { get; set; }
    public double maxRangeKm { get; set; }
    public int missionsCompleted { get; set; }
    public string? status { get; set; }
}