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

        string rootDirectory = Directory.GetCurrentDirectory();

        string pathOfCleanJson = Path.Combine(rootDirectory, "output","drones_clean.json");
        string rawFilePath = Path.Combine(rootDirectory, "input", "raw","drones_raw.json");
        
        string allInvalidPath = Path.Combine(rootDirectory, "input", "test_scenarios", "drones_all_invalid.json");
        string dronesEmptyPath = Path.Combine(rootDirectory, "input", "test_scenarios", "drones_empty.json");
        string malformedPath = Path.Combine(rootDirectory, "input", "test_scenarios", "drones_malformed.json");
        string nullPath = Path.Combine(rootDirectory, "input", "test_scenarios", "drones_null.json");
        string nonExistsPath = Path.Combine(rootDirectory, "input", "test_scenarios", "non_exists.json");

        string reportFilePath = Path.Combine(rootDirectory, "output", "analysis_report.txt");

        ICommandLogger consoleLogger = new ConsoleLogger();
        //IDroneDataLoader dataLoader = new LoadFromJson(rawFilePath);

        IDroneDataLoader dataLoader = new LoadFromJson(allInvalidPath);
        //IDroneDataLoader dataLoader = new LoadFromJson(nonExistsPath);
        //IDroneDataLoader dataLoader = new LoadFromJson(dronesEmptyPath);
        //IDroneDataLoader dataLoader = new LoadFromJson(malformedPath);
        //IDroneDataLoader dataLoader = new LoadFromJson(nullPath);

        ProcessPipeline pipeline = new ProcessPipeline(consoleLogger, dataLoader);

        //pipeline.Run(rawFilePath, pathOfCleanJson,reportFilePath);

        pipeline.Run(allInvalidPath, pathOfCleanJson, reportFilePath);
        //pipeline.Run(dronesEmptyPath, pathOfCleanJson, reportFilePath);
        //pipeline.Run(malformedPath, pathOfCleanJson, reportFilePath);
        //pipeline.Run(nullPath, pathOfCleanJson, reportFilePath);
        //pipeline.Run(nonExistsPath, pathOfCleanJson, reportFilePath);



    }
}