using System;
using System.Collections.Generic;
using System.IO;
using ConsoleStore.Classes;
using FileManager.Classes;

const string FolderPath = "./database";

var manager = new ExplorerManager();

List<string> filePaths = manager.GetAllFilePaths(FolderPath);


foreach (string fileName in filePaths)
{
    Console.WriteLine(fileName);
}


List<Task<Dictionary<string, object>?>> parserTasks = new List<Task<Dictionary<string, object>?>>();
foreach (string filePath in filePaths)
{
    parserTasks.Add(manager.ParseJsonFileAsync(filePath));
}

Dictionary<string, object>?[] results = await Task.WhenAll(parserTasks);


foreach (object result in results)
{
    Console.WriteLine($"{result}");
}


HashSet<string> uniqueKeys = new HashSet<string>();
foreach (var result in results)
{
    if (result != null)
    {
        foreach (var key in result.Keys)
        {
            uniqueKeys.Add(key);
        }
    }
}


Console.WriteLine($"Number of unique keys: {uniqueKeys.Count)}");