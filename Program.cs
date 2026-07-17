using DroneFleetDataProcessing.Exceptions;
using DroneFleetDataProcessing.FileHandling;
using DroneFleetDataProcessing.Models.Sensors;
using DroneFleetDataProcessing.Queries;
using DroneFleetDataProcessing.ReportLogger;
using DroneFleetDataProcessing.Validators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace DroneFleetDataProcessing.Pipeline;

class Program
{
    static void Main()
    {
        // The root folder from which the program ran
        string rootDirectory = AppContext.BaseDirectory;

        string pathOfCleanJson = Path.Combine(rootDirectory, "output","drones_clean.json");
        string rawFilePath = Path.Combine(rootDirectory, "input", "raw","drones_raw.json");
        
        string allInvalidPath = Path.Combine(rootDirectory, "input", "test_scenarios", "drones_all_invalid.json");
        string dronesEmptyPath = Path.Combine(rootDirectory, "input", "test_scenarios", "drones_empty.json");
        string malformedPath = Path.Combine(rootDirectory, "input", "test_scenarios", "drones_malformed.json");
        string nullPath = Path.Combine(rootDirectory, "input", "test_scenarios", "drones_null.json");
        string nonExistsPath = Path.Combine(rootDirectory, "input", "test_scenarios", "non_exists.json");

        string selectedPath = rawFilePath;

        string reportFilePath = Path.Combine(rootDirectory, "output", "analysis_report.txt");

        ICommandLogger consoleLogger = new ConsoleLogger();

        IDroneDataLoader dataLoader = new LoadFromJson(selectedPath);
        

        ProcessPipeline pipeline = new ProcessPipeline(consoleLogger, dataLoader);

        

        pipeline.Run(pathOfCleanJson, reportFilePath);
        



    }
}