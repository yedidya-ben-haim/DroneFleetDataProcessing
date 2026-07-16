using System.Collections.Generic;
using DroneFleetDataProcessing.Models.Sensors;

namespace DroneFleetDataProcessing.Storage;

public class DroneRepository
{
    private readonly List<Drone> _validDrones = new List<Drone>();

    public void Add(Drone drone)
    {
        _validDrones.Add(drone);
    }

    public List<Drone> GetAll()
    {
        return _validDrones;
    }

    public int Count => _validDrones.Count;
}