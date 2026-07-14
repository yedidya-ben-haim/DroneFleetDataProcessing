using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using DroneFleetDataProcessing.Models.Sensors;
using DroneFleetDataProcessing.Exceptions;

namespace DroneFleetDataProcessing.FileHandling;

public class LoadFromJson
{
    public static List<Drone> loadFromJson(string path)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"Erorr: the file in {path} was not found");
        }

        string fileContent = File.ReadAllText(path);

        if (string.IsNullOrWhiteSpace(fileContent))
        {
            throw new FileIsEmptyOrWhiteSpace("Error: JSON file is empty or contains only whitespace.");
        }

        return JsonSerializer.Deserialize<List<Drone>>(fileContent) ?? new List<Drone>();
    }
}
