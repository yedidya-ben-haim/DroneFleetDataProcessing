using DroneFleetDataProcessing.Models.Sensors;

namespace DroneFleetDataProcessing.Validators
{
    public static class DroneCollectionValidator
    {
        public static ValidationResult ValidateAll(List<Drone> drones)
        {

            List<Drone> validDrones = new();
            int rejectedCount = 0;
            HashSet<int> seenIds = new();
            HashSet<string> seenSerialNumbers = new();


            foreach (Drone drone in drones)
            {
                bool fieldsAreValid = DroneValidator.ValidateDrone(drone);

                bool idIsUnique = seenIds.Add(drone.id);

                bool serialIsUnique = !string.IsNullOrWhiteSpace(drone.serialNumber) &&
                                            seenSerialNumbers.Add(drone.serialNumber);

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
            return new ValidationResult(validDrones, rejectedCount);
        }
    }
}