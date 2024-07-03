using FileManager.Classes;
using System.Collections.Concurrent;

const string FolderPath = "./database";

var manager = new ExplorerManager();

List<string> filePaths = manager.GetAllFilePaths(FolderPath);

List<Task<Dictionary<string, string>>> parserTasks = new List<Task<Dictionary<string, string>>>();

HashSet<string> uniqueKeys = new HashSet<string>();

ConcurrentDictionary<string, int> keyCounts = new ConcurrentDictionary<string, int>();

foreach (string filePath in filePaths)
{
    parserTasks.Add(manager.ParseJsonFileAsync(filePath, uniqueKeys, keyCounts));
}

var result = await Task.WhenAll(parserTasks);

var allKeyValuePairs = result.SelectMany(dict => dict);

var groupedKeys = allKeyValuePairs
    .GroupBy(pair => pair.Key)
  .ToDictionary(
        group => group.Key,
        group => group.Select(pair => pair.Value).ToArray()
    );

foreach (var groupedKey in groupedKeys)
{
    Console.WriteLine($"{groupedKey.Key}: {String.Join(", ", groupedKey.Value)}");
}


Console.WriteLine($"Number of unique keys: {uniqueKeys.Count}");

foreach (var kvp in keyCounts)
{
    Console.WriteLine($"{kvp.Key}: {kvp.Value}");
}
