using System;
namespace ExceptionNassege;
public class FileIsEmptyOrWhiteSpace : Exception
{
    public FileIsEmptyOrWhiteSpace(string massege) : base(massege) { }
}