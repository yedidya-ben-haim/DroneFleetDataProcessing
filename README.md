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
?
??? input/
?   ??? raw/
?   ?   ??? drones_raw.json
?   ?
?   ??? test_scenarios/
?       ??? drones_malformed.json
?       ??? drones_empty.json
?       ??? drones_null.json
?       ??? drones_all_invalid.json
?
??? output/
?   ??? drones_clean.json
?   ??? analysis_report.txt
?
??? src/
?   ??? Pipeline/
?   ?   ??? ProcessPipeline.cs
?   ?
?   ??? FileHandling/
?   ?   ??? JsonHandle.cs
?   ?
?   ??? Models/
?   ?   ??? Enums/
?   ?   ??? Sensors/
?   ?       ??? Drone.cs
?   ?
?   ??? Validators/
?   ?   ??? SensorsValidator.cs
?   ?   ??? DroneFieldValidation.cs
?   ?   ??? DroneBusinessValidation.cs
?   ?   ??? ValidationResult.cs
?   ?   ??? IValidator.cs
?   ?
?   ??? Exceptions/
?   ?   ??? ExceptionsMessage.cs
?   ?
?   ??? Storage/
?       ??? DroneRepository.cs
?
??? Program.cs
??? README.md

```


---

## Clasess

| Clases | Responsibility |
|---------|------|
| `Drone`   | Represents a drone and includes the report data |
|  | Manages loading and saving to JSON |
|  |  |
|  |  |

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

```

**Possible output:**

```text

```

---

## Technologies

- C#
- .NET



---

## How to run the project

1.
2.
3.

```bash
dotnet run
```

---

## Tests performed

| scenario | Expected result |
|---|---|

---

## Planning decisions



- 
- 
- 
---


## Division of responsibility

### Pesach: FileHandling, Models, Exceptions, ReportLogger, Storage, 

### Yedidya: Validators, README, GitHub

</div>
