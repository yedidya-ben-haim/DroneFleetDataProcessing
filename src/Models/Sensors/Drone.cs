
using System;
namespace DroneFleetDataProcessing.Models.Sensors
{

    public class RawDrone
    {
        public int Id { get; set; }
        public string? SerialNumber { get; set; }
        public string? Model { get; set; }
        public string? Category { get; set; }
        public string? BaseLocation { get; set; }
        public double FlightHours { get; set; }
        public int BatteryHealth { get; set; }
        public double MaxRangeKm { get; set; }
        public int MissionsCompleted { get; set; }
        public string? Status { get; set; }
    }
}