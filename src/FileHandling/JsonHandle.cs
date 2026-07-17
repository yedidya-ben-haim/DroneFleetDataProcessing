using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using DroneFleetDataProcessing.Models.Sensors;
using DroneFleetDataProcessing.Exceptions;

namespace DroneFleetDataProcessing.FileHandling;

// JSON file loading class
public class LoadFromJson : IDroneDataLoader
{
    private readonly string Path;
    public LoadFromJson(string path)
    {
        Path = path;
    }
    public List<Drone> LoadData()
    {
        if (!File.Exists(Path))
        {
            throw new FileNotFoundException($"The file was not found: {Path}");
        }

        string fileContent = File.ReadAllText(Path);

        if (string.IsNullOrWhiteSpace(fileContent))
        {
            throw new FileIsEmptyOrWhiteSpace("The JSON file is empty or contains only whitespace");
        }

        List<Drone>? drones = JsonSerializer.Deserialize<List<Drone>>(fileContent);

        if (drones is null)
        {
            throw new InvalidDataException("Deserialization returned null");
        }

        if (drones.Count == 0)
        {
            throw new InvalidDataException("The JSON file contains an empty array");
        }
        return drones;
    }

    // Saving objects to a file method
    public static void SaveToJson(string path, List<Drone> drones) 
    {
        if(drones == null)
        {
            throw new ArgumentNullException(nameof(drones), "Drone list cannot be empty");
        }
        var options = new JsonSerializerOptions{ WriteIndented = true };

        string jsonString = JsonSerializer.Serialize(drones, options);

        File.WriteAllText(path, jsonString);
    }
}
