using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleStore.Classes
{
    public class FileManager
    {
        private static string PathToFile { get; } = "categories.json";

        private List<Category> _categories = new List<Category>(JsonSerializer.Deserialize<List<Category>>(File.ReadAllText(PathToFile)))!;

        public List<Category> GetListOfCategories()
        {
            return this._categories;
        }
    }
}
