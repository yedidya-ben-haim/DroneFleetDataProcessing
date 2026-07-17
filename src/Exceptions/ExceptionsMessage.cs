using System;
namespace DroneFleetDataProcessing.Exceptions;

// File Empty Or White Space Customized Exception
public class FileIsEmptyOrWhiteSpace : Exception
{
    public FileIsEmptyOrWhiteSpace(string massege) : base(massege) { }
}