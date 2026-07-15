using DroneFleetDataProcessing.Models.Sensors;

namespace DroneFleetDataProcessing.Validators
{
    public class ValidationResult
    {
        public List<Drone> ValidDrones { get; }
        public int RejectedCount { get; }

        public ValidationResult(List<Drone> validDrones, int rejectedCount)
        {
            ValidDrones = validDrones;
            RejectedCount = rejectedCount;
        }
    }
}