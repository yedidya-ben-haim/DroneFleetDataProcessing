namespace DroneFleetDataProcessing.Validators
{
    public class DroneValidator
    {
        public bool Validate(Drone drone)
        {
            return IsIdValid(drone.id)
                && IsSerialNumberValid
        }

        private bool IsIdValid(int id)
        {
            return id > 0;
        }
        
        private bool IsSerialNumberValid(string serialNumber)
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
    }

}