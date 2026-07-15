using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using DroneFleetDataProcessing.FileHandling;    // מייבא את מחלקת הטעינה
using DroneFleetDataProcessing.Models.Sensors; // מייבא את הרחפן
using DroneFleetDataProcessing.Exceptions;     // מייבא את השגיאות שלך

namespace DroneFleetDataProcessing.Pipeline;

class Program
{
    static void Main()
    {
        //string filePath = Path.Combine("input", "raw", "drones_raw.json");
        //PipeLine pipe = new PipeLine();
        //List<Drone> reports = pipe.GetData(filePath);
        string filePath = Path.Combine("input", "raw", "drones_raw.json");

        try
        {
            List<Drone> drones = LoadFromJson.loadFromJson(filePath);

            Console.WriteLine($"load succeseded {drones.Count} was loaded");
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"Error file in path {filePath} was not found {ex.Message}");
        }
        catch (FileIsEmptyOrWhiteSpace ex)
        {

            Console.WriteLine($"Error file is empty {ex.Message}");
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Error file has some problems: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error accured: {ex.Message}");
        }


    }
}