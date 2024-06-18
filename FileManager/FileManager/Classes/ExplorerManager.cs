using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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

        public async Task<Dictionary<string, object>?> ParseJsonFileAsync(string filePath)
        {
            try
            {
                using FileStream openStream = File.OpenRead(filePath);
                var jsonSerializerOptions = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                return await JsonSerializer.DeserializeAsync<Dictionary<string, object>>(openStream, jsonSerializerOptions);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while parsing {filePath}: {ex.Message}");
                return null;
            }
        }
    }
}
