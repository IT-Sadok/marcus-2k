using System.Collections.Concurrent;
using System.Text.Json;

namespace FileManager.Classes
{
    public class ExplorerManager
    {

        private static SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

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
        public async Task ParseJsonFileAsync(string filePath, HashSet<string> uniqueKeys, ConcurrentDictionary<string, int> keyCounts)
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

                        await _semaphore.WaitAsync();
                        try
                        {
                            foreach (JsonProperty property in jsonElement.EnumerateObject())
                            {

                                keyCounts.AddOrUpdate(property.Name, 1, (key, count) => count + 1);
                                uniqueKeys.Add(property.Name);
                            }
                        }
                        finally
                        {
                            _semaphore.Release();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while parsing {filePath}: {ex.Message}");
            }
        }
    }
}
