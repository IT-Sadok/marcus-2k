
namespace ConsoleStore.Clases
{
    public class Web
    {
        private Server _server = new Server();

        public string SelectAction()
        {
            Console.WriteLine("Select action");
            Console.WriteLine("1 - List of products");
            Console.WriteLine("2 - Add product");
            Console.WriteLine("Any button - Exit");

            return Console.ReadLine().ToString();
        }

        public void ShowListOfProducts()
        {
            List<Product> products = this._server.GetListOfProducts();

            foreach (Product product in products)
            {
                Console.WriteLine("Id: " + product.Id + " Name: " + product.Name + " Category name: " + product.Category?.Name);
            }

            Console.WriteLine("Number of procuts " + products.Count);
        }

        public void ShowListOfCategories()
        {

            List<Category> categories = this._server.GetListOfCategories();

            foreach (var category in categories)
            {
                Console.WriteLine("ID: " + category.Id + " Name: " + category.Name);
            }
        }

        public void CreateNewProduct()
        {
            Console.WriteLine("Enter name of product");

            string nameOfProduct = Console.ReadLine();

            this._server.CreateNewProduct(new Dto.NewProduct
            {
                Name = new string(nameOfProduct)
            });


            //this.CallMenuSelectCategoryForProduct(productId);
        }

        //public void CallMenuSelectCategoryForProduct(int productId)
        //{
        //    Console.WriteLine("Do you want to select a category for the product?");
        //    Console.WriteLine("Y - YES");
        //    Console.WriteLine("N - NO");
        //    Console.WriteLine("Any button - SKIP");

        //    string inputKey = Console.ReadLine().ToString();

        //    switch (inputKey)
        //    {
        //        case "Y":
        //            {
        //                this.ShowListOfCategories();

        //                Console.WriteLine("Enter category ID:");

        //                try
        //                {
        //                    int categoryId = int.Parse(Console.ReadLine());

        //                    this.AddCategoryToProduct(productId, categoryId);
        //                }
        //                catch (FormatException error)
        //                {
        //                    Console.WriteLine("Input was not a valid number. Please enter a valid integer.");
        //                }
        //            }
        //            break;
        //        case "N": break;
        //        default: break;
        //    }
        //}

        //public void AddCategoryToProduct(int productId, int categoryId)
        //{
        //    Product? product = _products.Find(p => p.Id == productId);
        //    Category? category = _categories.Find(c => c.Id == categoryId);

        //    if (product != null && category != null)
        //    {
        //        product.CategoryId = categoryId;
        //        product.Category = category;
        //        Console.WriteLine($"Product {product.Name}'s category updated to {categoryId}");
        //    }
        //    else
        //    {
        //        Console.WriteLine($"Product with ID {productId} not found.");
        //    }
        //}

    }
}
