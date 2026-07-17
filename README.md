# Drone Fleet Data Processing System

A system for processing and analyzing drone fleet data

---

## Main requirements



- Load data from a bad JSON file
- Check field validity
- Save valid objects to a dedicated file
- Load objects from a valid JSON file
- Run queries on the objects
- Return a report

---

## Business laws



- id - must be greater than zero, must be unique in the entire file.
- serialNumber - cannot be empty or contain only spaces, must be unique, must be in exact format: DR-XXXX
- model - Must be of legal values ​​only.
- category - Must be of legal values ​​only.
- base_location - Must be of legal values ​​only.
- flightHours - Must be a number in the range 0 - 2500.
- batteryHealth - Must be a number in the range 0- 100.


---

## Project structure


```text
DroneFleetDataProcessing/
│
├── input/
│   ├── raw/
│   │   └── drones_raw.json
│   │
│   └── test_scenarios/
│       ├── drones_malformed.json
│       ├── drones_empty.json
│       ├── drones_null.json
│       └── drones_all_invalid.json
│
├── output/
│   ├── drones_clean.json
│   └── analysis_report.txt
│
└── src/
    ├── Exceptions/
    │   └── ExceptionsMessage.cs
    │
    ├── FileHandling/
    │   └── JsonHandle.cs
    │
    ├── Models/
    │   └── Sensors/
    │       └── Drone.cs
    │
    ├── Pipeline/
    │   └── ProcessPipeline.cs
    │
    ├── Queries/
    │   └── DroneAnalyzer.cs
    │
    ├── ReportLogger/
    │   ├── ICommandLogger.cs
    │   ├── ConsoleLogger.cs
    │   └── FileLogger.cs
    │
    ├── Storage/
    │   └── DroneRepository.cs
    │
    └── Validators/
        ├── IValidator.cs
        ├── DroneValidator.cs
        ├── DroneCollectionValidator.cs
        └── ValidationResult.cs
```


---

## Clasess

| Class | Responsibility |
| :--- | ----- |
| `Drone` | Represents a single drone and holds its sensor data. |
| `JsonHandle` | Manages loading raw data from JSON and saving clean data to JSON |
| `ProcessPipeline` | Orchestrates the entire workflow steps (Load -> Validate -> Save -> Analyze -> Report) |
| `DroneValidator` | Validates a single drone's fields and checks all business laws |
| `DroneCollectionValidator` | Validates the entire collection and filters out duplicate IDs or serial numbers |
| `ValidationResult` | Stores the final lists of valid and rejected drones after the validation process |
| `DroneRepository` | Acts as an in-memory database to store and provide access to the clean drones |
| `DroneAnalyzer` | Runs all advanced LINQ queries and statistical calculations on the clean dataset |
| `FileLogger` / `ConsoleLogger` | Handles logging messages and errors to a text file or directly to the screen |

---

## Workflow

```text
Reading a raw file
↓
Converting JSON to objects
↓
Checking the integrity of records
↓
Separating into valid and invalid records
↓
Saving the valid records in a clean file
↓
Rereading the clean file
↓
Performing LINQ analyses
↓
Generating a text report
```


---

## Example of use

```csharp
## Example of use

```csharp
using System;
using System.IO;
using DroneFleetDataProcessing.Pipeline;
using DroneFleetDataProcessing.ReportLogger;

class Program
{
    static void Main()
    {
        // Define paths relative to the root folder
        string inputPath = Path.Combine("input", "raw", "drones_raw.json");
        string outputPath = Path.Combine("input", "output", "drones_clean.json");
        string reportPath = Path.Combine("input", "output", "analysis_report.txt");

        // Initialize components
        ICommandLogger logger = new ConsoleLogger();
        ProcessPipeline pipeline = new ProcessPipeline(inputPath, outputPath, reportPath, logger);

        // Run the automated pipeline sequence
        pipeline.Start();
    }
}

```

**Possible output:**

```text
=== Drone Fleet Data Processing System ===
Step 1: Reading raw data... Read 102 records from raw file
Step 2: Validating data and creating clean dataset... Valid records: 84 | Rejected records: 18
Step 3: Saving clean data... Clean data saved to: C:\Users\Pesach\Projects\DroneFleetDataProcessing\input\output\drones_clean.json
Step 4: Reloading clean data... Loaded 84 records from clean dataset
Step 5: Performing analysis... Analysis completed successfully
Step 6: Generating report... Report generated successfully: C:\Users\Pesach\Projects\DroneFleetDataProcessing\input\output\analysis_report.txt
=== Process completed successfully! ===

```

---

## Technologies

- C#
- .NET



---

## How to run the project

1. Make sure you have .NET 8 or newer installed.
2. Open a terminal in the root folder of the project (DroneFleetDataProcessing/).
3. Run these simple commands:
# To compile the code
dotnet build

# To run the pipeline and generate the reports
dotnet run

```bash
dotnet run
```

---

## Tests performed

| scenario | Expected result |
|---|---|

---

## Planning Decisions

- **Centralized vs. Row-Level Error Handling:** We divided how the system handles runtime problems into two layers. Structural errors that break the entire process (like a completely malformed JSON
- file or a missing file) are caught up front in the main pipeline execution loop to shut down the app cleanly. On the other hand, validation errors inside individual drone records are handled safely row-by-row;
- they are tracked in logs and simply filtered out so the system can continue processing the rest of the valid data
- **Relative Path Routing for Portability:** To make sure our program works seamlessly on any host machine without hardcoded folder structures, we avoided absolute file paths. 
- We used C#'s native `Path.Combine` relative to the application's runtime executing environment to cleanly route files inside the designated `input/` and `output/` directories.
- **Decoupled Architecture for Future Changes (OCP):** Following the Open/Closed Principle, we completely isolated disk I/O operations inside `JsonHandle` and decoupled our collection cache inside
- `DroneRepository` f the underlying database source changes in the future (for example, switching from JSON to a SQL server database or a flat CSV file), we only need to write a brand new handler class. 
- The core data pipeline, property validators, and complex LINQ queries will remain completely untouched
---


## Division of responsibility

### Pesach: FileHandling, Models, Exceptions, ReportLogger, Storage, 

### Yedidya: Validators, README, GitHub

</div>
