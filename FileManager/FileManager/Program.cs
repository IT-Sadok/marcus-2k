using FileManager.Classes;
using System.Collections.Concurrent;

const string FolderPath = "./database";

var manager = new ExplorerManager();

List<string> filePaths = manager.GetAllFilePaths(FolderPath);

List<Task> parserTasks = new List<Task>();

HashSet<string> uniqueKeys = new HashSet<string>();

ConcurrentDictionary<string, int> keyCounts = new ConcurrentDictionary<string, int>();

foreach (string filePath in filePaths)
{
    parserTasks.Add(manager.ParseJsonFileAsync(filePath, uniqueKeys, keyCounts));
}

await Task.WhenAll(parserTasks);

Console.WriteLine($"Number of unique keys: {uniqueKeys.Count}");

foreach (var kvp in keyCounts)
{
    Console.WriteLine($"{kvp.Key}: {kvp.Value}");
}
