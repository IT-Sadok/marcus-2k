using FileManager.Classes;

const string FolderPath = "./database";

var manager = new ExplorerManager();

List<string> filePaths = manager.GetAllFilePaths(FolderPath);

List<Task<object?>> parserTasks = new List<Task<object?>>();

HashSet<string> uniqueKeys = new HashSet<string>();

foreach (string filePath in filePaths)
{
    parserTasks.Add(manager.ParseJsonFileAsync(filePath, uniqueKeys));
}

await Task.WhenAll(parserTasks);

Console.WriteLine($"Number of unique keys: {uniqueKeys.Count}");
