using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using DroneFleetDataProcessing.FileHandling;
using DroneFleetDataProcessing.Models.Sensors;
using DroneFleetDataProcessing.Exceptions;
//using DroneFleetDataProcessing.ReportLogger;

namespace DroneFleetDataProcessing.Pipeline;

class Program
{
    public static void AnalysisReport(List<Drone> drones)
    {

        private readonly IcommandLogger _logger;
        string reportFileOutput = Path.Combine();
        _logger = new FileLogger("output","analysis_report.txt");
        
    }
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
            //Validateresult validateresult = vaildateall(drons)
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