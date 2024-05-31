
using System;
using System.Reflection.Emit;
using System.Text.Json;

namespace ConsoleStore.Clases
{
    internal class Store
    {
        private List<Category> _categories = new List<Category>(JsonSerializer.Deserialize<List<Category>>(File.ReadAllText("../../../categories.json")));
        private List<Product> _products = new List<Product>();

        public string SelectAction()
        {
            Console.WriteLine("Select action");
            Console.WriteLine("1 - List of products");
            Console.WriteLine("2 - Add product");
            Console.WriteLine("Any button - Exit");

            return Console.ReadLine().ToString();
        }

        public void PrintListOfProducts()
        {

            foreach (Product product in this._products)
            {
                Console.WriteLine("Id: " + product.Id + " Name: " + product.Name + " Category name: " + product.Category?.Name);
            }

            Console.WriteLine("Number of procuts " + this._products.Count);
        }

        public void CreateNewProduct()
        {
            Console.WriteLine("Enter name of product");

            string nameOfProduct = Console.ReadLine();

            int productId = this._products.Count + 1;

            this._products.Add(new Product { Id = productId, Name = nameOfProduct, CategoryId = 1 });

            this.CallMenuSelectCategoryForProduct(productId);
        }

        public void CallMenuSelectCategoryForProduct(int productId)
        {
            Console.WriteLine("Do you want to select a category for the product?");
            Console.WriteLine("Y - YES");
            Console.WriteLine("N - NO");
            Console.WriteLine("Any button - SKIP");

            string inputKey = Console.ReadLine().ToString();

            switch (inputKey)
            {
                case "Y":
                    {
                        this.ShowListOfCategories();

                        Console.WriteLine("Enter category ID:");

                        try
                        {
                            int categoryId = int.Parse(Console.ReadLine());

                            this.AddCategoryToProduct(productId, categoryId);
                        }
                        catch (FormatException error)
                        {
                            Console.WriteLine("Input was not a valid number. Please enter a valid integer.");
                        }
                    }
                    break;
                case "N": break;
                default: break;
            }
        }

        public void ShowListOfCategories()
        {
            foreach (var category in _categories)
            {
                Console.WriteLine("ID: " + category.Id + " Name: " + category.Name);
            }
        }

        public void AddCategoryToProduct(int productId, int categoryId)
        {
            Product? product = _products.Find(p => p.Id == productId);
            Category? category = _categories.Find(c => c.Id == categoryId);

            if (product != null && category != null)
            {
                product.CategoryId = categoryId;
                product.Category = category;
                Console.WriteLine($"Product {product.Name}'s category updated to {categoryId}");
            }
            else
            {
                Console.WriteLine($"Product with ID {productId} not found.");
            }
        }

    }
}
