using DroneFleetDataProcessing.Models.Sensors;

namespace DroneFleetDataProcessing.validation
{
    public class DroneValidator
    {
        public ValidationResult ValidateAll(List<Drone> drones)
        {

            List<Drone> validDrones = new();
            int rejectedCount = 0;
            HashSet<int> seenIds = new();
            HashSet<string> seenSerialNumbers = new();


            foreach (Drone drone in drones)
            {
                bool fieldsAreValid = DroneValidator.Validate(drone);

                bool idIsUnique = seenIds.Add(drone.id);

                bool serialIsUnique = seenSerialNumbers.Add(drone.SerialNumber);

                bool isValid =
                    fieldsAreValid &&
                    idIsUnique &&
                    serialIsUnique;

                if (isValid)
                {
                    validDrones.Add(drone);
                }
                else
                {
                    rejectedCount++;
                }
            }




        public static bool Validate(Drone drone)
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
                && IsOperationalValid(drone.status, drone.batteryHealth);
        }

        private static bool IsIdValid(int id)
        {
            return id > 0;
        }
        
        private static bool IsSerialNumberValid(string? serialNumber)
        {
            const int validSerialNumberLength = 7;

            if (string.IsNullOrWhiteSpace(serialNumber))
                return false;
            if (serialNumber.Length != validSerialNumberLength)
                return false;

            string firstPart = serialNumber[..3];
            string secondPart = serialNumber[^4..];
            if (firstPart != "DR-")
                return false;
            if (!int.TryParse(secondPart, out _))
            {
                return false;
            }

            return true;
        }

        private static bool IsModelValid(string? model)
        {
            string[] validModels = { "Falcon-X", "Raven-M", "SkyEye-2", "CargoBee", "Storm-4", "Scout-Lite" };

             return !string.IsNullOrWhiteSpace(model) && ValidModels.Contains(model);
        }
        
        private static bool IsCategoryValid(string? category)
        {
            string[] validCategories = { "Recon", "Patrol", "Mapping", "Delivery", "Search" };

             return !string.IsNullOrWhiteSpace(category) && ValidCategory.Contains(category);
        }
        
        private static bool IsBaseLocationValid(string? baseLocation)
        {
            string[] validBaseLocations = { "North", "South", "Central", "East", "West" };

             return !string.IsNullOrWhiteSpace(baseLocation) && ValidBaseLocation.Contains(baseLocation);
        }

        private static bool IsFlightHoursValid(double flightHours)
        {
            const int MaxFlightHours = 2500;
            const int MinFlightHours = 0;

            return flightHours <= MaxFlightHours && flightHours >= MinFlightHours; 
        }

        private static bool IsBatteryHealthValid(int batteryHealth)
        {
            const int MaxBatteryHealth = 100;
            const int MinBatteryHealth = 0;

            return batteryHealth <= MaxBatteryHealth && batteryHealth >= MinBatteryHealth;
        }

        private static bool IsMaxRangeKmValid(double rangeKm)
        {
            const int MaxRangeKm = 150;
            const int MinRangeKm = 1;

            return rangeKm <= MaxRangeKm && rangeKm >= MinRangeKm;
        }

        private static bool IsMissionsCompletedValid(int missionsCompleted)
        {
            const int MaxMissionsCompleted = 5000;
            const int MinMissionsCompleted = 0;

            return missionsCompleted <= MaxMissionsCompleted && missionsCompleted >= MinMissionsCompleted;
        }

        private static bool IsStatusValid(string? status)
        {
            string[] validStatuses = { "Operational", "Maintenance", "Grounded", "Training" };

            return !string.IsNullOrWhiteSpace(status) && ValidStatus.Contains(status);
        }

        private static bool IsOperationalValid(string? status, int batteryHealth)
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