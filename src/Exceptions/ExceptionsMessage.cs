using System;
namespace DroneFleetDataProcessing.Exceptions;

public class FileIsEmptyOrWhiteSpace : Exception
{
    public FileIsEmptyOrWhiteSpace(string massege) : base(massege) { }
}