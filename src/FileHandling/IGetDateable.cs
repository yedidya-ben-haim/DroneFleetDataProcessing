using DroneFleetDataProcessing.Models.Sensors;
using System;
using System.Collections.Generic;
using System.Text;

namespace DroneFleetDataProcessing.FileHandling
{
    public interface IDroneDataLoader
    {
        List<Drone> LoadData();
    }
}
