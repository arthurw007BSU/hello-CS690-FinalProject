using System.Text.Json;
using System.IO;

namespace MaintenanceTracker
{
    public static class FileManager
    {
        public static void SaveToFile<T>(string filePath, List<T> data)
        {
            string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
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