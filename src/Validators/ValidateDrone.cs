namespace DroneFleetDataProcessing.Validators
{
    public class ValidateDrone
    {
        public bool DroneValidator(Drone drone)
        {
            return true;
        }

        private bool IsIdValid(int id)
        {
            return id > 0;
        }
        
        private bool IsSerialNumberValid(string serialNumber)
        {
            if (string.IsNullOrWhiteSpace(serialNumber))
            {
                return false;
            }
            if(serialNumber.)
        }
    }
}