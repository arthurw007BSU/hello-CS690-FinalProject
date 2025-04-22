// Created by: [Arthur]
/* This class handles saving and loading data to and from JSON files.
   It provides methods to serialize objects into JSON format for saving,
*/ 
using System.IO;
using System.Text.Json;

namespace MaintenanceTracker
{
    public static class FileManager
    {
        public static void SaveToFile<T>(string filePath, List<T> data)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(data, options);
            File.WriteAllText(filePath, json);
        }

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