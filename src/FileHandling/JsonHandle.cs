using System;
//using FileHandling;
using System.Text.Json;
using ExceptionNassege;
using DroneSpace;

namespace Sensors;

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
