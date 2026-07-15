using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using DroneFleetDataProcessing.Models.Sensors;
using DroneFleetDataProcessing.Exceptions;

namespace DroneFleetDataProcessing.FileHandling;

public class LoadFromJson
{
    public static List<Drone> LoadJson(string path)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"The file was not found: {path}");
        }

        string fileContent = File.ReadAllText(path);

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
