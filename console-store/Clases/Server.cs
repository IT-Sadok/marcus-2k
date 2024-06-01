using System.Text.Json;
using ConsoleStore.Clases.Dto;

namespace ConsoleStore.Clases
{
    public class Server
    {

        private List<Category> _categories = new List<Category>(JsonSerializer.Deserialize<List<Category>>(File.ReadAllText("../../../categories.json")));
        private List<Product> _products = new List<Product>();

        public List<Category> GetListOfCategories()
        {
            return this._categories;
        }

        public List<Product> GetListOfProducts()
        {
            return this._products;
        }

        private int GetNewProductId()
        {
            return this._products.Count + 1;
        }

        public void CreateNewProduct(NewProduct body)
        {
            int productId = this.GetNewProductId();

            this._products.Add(new Product { Id = productId, Name = body.Name, CategoryId = null });

            //this.CallMenuSelectCategoryForProduct(productId);
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
