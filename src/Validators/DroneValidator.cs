using DroneFleetDataProcessing.Models.Sensors;

namespace DroneFleetDataProcessing.validation
{
    public class DroneValidator
    {
        public bool Validate(Drone drone)
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

        private bool IsIdValid(int id)
        {
            return id > 0;
        }
        
        private bool IsSerialNumberValid(string? serialNumber)
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

        private bool IsModelValid(string? model)
        {
            string[] ValidModels = { "Falcon-X", "Raven-M", "SkyEye-2", "CargoBee", "Storm-4", "Scout-Lite" };

             return string.IsNullOrWhiteSpace(model) && ValidModels.Contains(model);
        }
        
        private bool IsCategoryValid(string? category)
        {
            string[] ValidCategory = { "Recon", "Patrol", "Mapping", "Delivery", "Search" };

             return string.IsNullOrWhiteSpace(category) && ValidCategory.Contains(category);
        }
        
        private bool IsBaseLocationValid(string? baseLocation)
        {
            string[] ValidBaseLocation = { "North", "South", "Central", "East", "West" };

             return string.IsNullOrWhiteSpace(baseLocation) && ValidBaseLocation.Contains(baseLocation);
        }

        private bool IsFlightHoursValid(double flightHours)
        {
            const int MaxFlightHours = 2500;
            const int MinFlightHours = 0;

            return flightHours <= MaxFlightHours && flightHours >= MinFlightHours; 
        }

        private bool IsBatteryHealthValid(int batteryHealth)
        {
            const int MaxBatteryHealth = 100;
            const int MinBatteryHealth = 0;

            return batteryHealth <= MaxBatteryHealth && batteryHealth >= MinBatteryHealth;
        }

        private bool IsMaxRangeKmValid(double rangeKm)
        {
            const int MaxRangeKm = 150;
            const int MinRangeKm = 1;

            return rangeKm <= MaxRangeKm && rangeKm >= MinRangeKm;
        }

        private bool IsMissionsCompletedValid(int missionsCompleted)
        {
            const int MaxMissionsCompleted = 5000;
            const int MinMissionsCompleted = 0;

            return missionsCompleted <= MaxMissionsCompleted && missionsCompleted >= MinMissionsCompleted;
        }

        private bool IsStatusValid(string? status)
        {
            string[] ValidStatus = { "Operational", "Maintenance", "Grounded", "Training" };

            return string.IsNullOrWhiteSpace(status) && ValidStatus.Contains(status);
        }

        private bool IsOperationalValid(string? status, int batteryHealth)
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