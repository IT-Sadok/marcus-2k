using System.Text.Json;

namespace FileManager.Classes
{
    public class ExplorerManager
    {
        public List<string> GetAllFilePaths(string folderPath)
        {

            List<string> filePaths = new List<string>();
            try
            {
                string[] files = Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories);

                filePaths = files
                    .Where(file => Path.GetExtension(file).Equals(".json", StringComparison.OrdinalIgnoreCase))
                    .OrderBy(file => file)
                    .ToList();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);

            }

            return filePaths;
        }
        public async Task<object?> ParseJsonFileAsync(string filePath, HashSet<string> uniqueKeys)
        {
            try
            {
                using FileStream openStream = File.OpenRead(filePath);
                var jsonSerializerOptions = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var parsedData = await JsonSerializer.DeserializeAsync<object>(openStream, jsonSerializerOptions);

                if (parsedData is JsonElement jsonElement)
                {
                    if (jsonElement.ValueKind == JsonValueKind.Object)
                    {
                        foreach (JsonProperty property in jsonElement.EnumerateObject())
                        {
                            uniqueKeys.Add(property.Name);
                        }
                    }
                }

                return parsedData;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while parsing {filePath}: {ex.Message}");

                return null;
            }
        }
    }
}
