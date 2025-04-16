using System.Text.Json;
using System.IO;

//created this helper class to read and write to the expense and tasks.json files.
namespace MaintenanceTracker
{
    public static class FileManager
    {
        public static void SaveToFile<T>(string filePath, List<T> data)
        {   //use jsonserializer to store the data in json format
            string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }
            // get information from the stored files
        public static List<T> LoadFromFile<T>(string filePath)
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
            }

            return new List<T>();
        }
    }
}