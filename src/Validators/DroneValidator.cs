using DroneFleetDataProcessing.Models.Sensors;

namespace DroneFleetDataProcessing.Validators
{
    // Drone Field Validation
    public static class DroneValidator
    {
        // ValidValues

        private static readonly string[] ValidModels = 
        {
                "Falcon-X",
                "Raven-M",
                "SkyEye-2",
                "CargoBee",
                "Storm-4",
                "Scout-Lite"
        };


        private static readonly string[] ValidCategories =
        {
                "Recon",
                "Patrol",
                "Mapping",
                "Delivery",
                "Search"
        };

        private static readonly string[] ValidBaseLocations =
        {
                "North",
                "South",
                "Central",
                "East",
                "West"
        };

        private static readonly string[] ValidStatuses =
        {
                "Operational",
                "Maintenance",
                "Grounded",
                "Training"
        };

        // Validation method
        public static bool ValidateDrone(Drone drone)
        {
            return IsIdValid(drone.id)
                && IsSerialNumberValid(drone.serialNumber)
                && IsModelValid(drone.model)
                && IsCategoryValid(drone.category)
                && IsBaseLocationValid(drone.base_location)
                && IsFlightHoursValid(drone.flightHours)
                && IsBatteryHealthValid(drone.batteryHealth)
                && IsMaxRangeKmValid(drone.maxRangeKm)
                && IsMissionsCompletedValid(drone.missionsCompleted)
                && IsStatusValid(drone.status)
                && IsOperationalBatteryRuleValid(drone.status, drone.batteryHealth);
        }



        // Field checking methods

        // id check
        private static bool IsIdValid(int id)
        {
            return id > 0;
        }

        // Serial Number check
        private static bool IsSerialNumberValid(string? serialNumber)
        {
            const int ValidSerialNumberLength = 7;

            if (string.IsNullOrWhiteSpace(serialNumber))
                return false;

            if (serialNumber.Length != ValidSerialNumberLength)
                return false;

            // "DR-" part
            string chars = serialNumber[..3];
            // "XXXX" part
            string digits = serialNumber[3..];

            return chars == "DR-" &&
                   digits.All(character => character is >= '0' and <= '9');
        }

        // Model Valid check
        private static bool IsModelValid(string? model)
        {
            return !string.IsNullOrWhiteSpace(model) && 
                ValidModels.Contains(model);
        }

        // Category check
        private static bool IsCategoryValid(string? category)
        {
            return !string.IsNullOrWhiteSpace(category) && 
                ValidCategories.Contains(category);
        }

        // Base Location check
        private static bool IsBaseLocationValid(string? baseLocation)
        {
            return !string.IsNullOrWhiteSpace(baseLocation) && 
                ValidBaseLocations.Contains(baseLocation);
        }

        // Flight Hours check
        private static bool IsFlightHoursValid(double flightHours)
        {
            const int MaxFlightHours = 2500;
            const int MinFlightHours = 0;

            return flightHours <= MaxFlightHours && flightHours >= MinFlightHours;
        }

        // Battery Health check
        private static bool IsBatteryHealthValid(int batteryHealth)
        {
            const int MaxBatteryHealth = 100;
            const int MinBatteryHealth = 0;

            return batteryHealth <= MaxBatteryHealth && batteryHealth >= MinBatteryHealth;
        }

        // Max Range Km check
        private static bool IsMaxRangeKmValid(double rangeKm)
        {
            const int MaxRangeKm = 150;
            const int MinRangeKm = 1;

            return rangeKm <= MaxRangeKm && rangeKm >= MinRangeKm;
        }

        // Missions Completed check
        private static bool IsMissionsCompletedValid(int missionsCompleted)
        {
            const int MaxMissionsCompleted = 5000;
            const int MinMissionsCompleted = 0;

            return missionsCompleted <= MaxMissionsCompleted && missionsCompleted >= MinMissionsCompleted;
        }

        // Status check
        private static bool IsStatusValid(string? status)
        {
            return !string.IsNullOrWhiteSpace(status) && 
                ValidStatuses.Contains(status);
        }

        // Operational Battery Rule check
        private static bool IsOperationalBatteryRuleValid(string? status, int batteryHealth)
        {
            const int MinOperationalBattery = 20;

            if (status == "Operational")
            {
                if (batteryHealth < MinOperationalBattery)
                {
                    return false;
                }
            }
            return true;
        }

    }

}