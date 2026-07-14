using System;
namespace Sensors;

using System.Text.Json;
using Exceptions.ExceptionsMessage;

public class LoadFromJson
{
    public void loadFromJson(string path)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException();
        }
        using (FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read))
        {
            using (StreamReader reader = new StreamReader(fs))
            {
                string fileContent = reader.ReadToEnd();

                if (string.IsNullOrWhiteSpace(fileContent))
                {
                    throw new FileIsEmptyOrWhiteSpace("Error Json file is empty");
                }
                using (JsonDocument document = JsonDocument.Parse(fileContent))
                {
                    return true;
                }
            }
        }
    }try
        {
            File.openRead(path)
}
        catch
        {
            UnauthorizedAccessException(Exsception ex);
        }
    }
}