
using System;
namespace DroneFleetDataProcessing.Models.Sensors;
public class Drone
{
    public int id { get; }
    public string? serialNumber { get; }
    public string? model { get; }
    public string? category { get; }
    public string? base_location { get; }
    public double flightHours { get; }
    public int batteryHealth { get; }
    public double maxRangeKm { get; }
    public int missionsCompleted { get; }
    public string? status { get; }

    public Drone() { }

    // ????? ??? ????? (_) ???? ?? ?????? ???? ?? ????? ?-this
    public Drone(
        int _id,
        string? _serialNumber,
        string? _model,
        string? _category,
        string? _baseLocation,
        double _flightHours,
        int _batteryHealth,
        double _maxRangeKm,
        int _missionsCompleted,
        string? _status)
    {
        id = _id;
        serialNumber = _serialNumber;
        model = _model;
        category = _category;
        base_location = _baseLocation;
        flightHours = _flightHours;
        batteryHealth = _batteryHealth;
        maxRangeKm = _maxRangeKm;
        missionsCompleted = _missionsCompleted;
        status = _status;
    }
}