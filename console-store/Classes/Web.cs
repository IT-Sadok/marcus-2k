


namespace ConsoleStore.Classes
{
    public class Web
    {
        private EShop _eshop = new EShop();

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
            List<Product> products = _eshop.GetListOfProducts();

            foreach (Product product in products)
            {
                Console.WriteLine("Id: " + product.Id + " Name: " + product.Name + " Category name: " + product.Category?.Name);
            }

            Console.WriteLine("Number of procuts " + products.Count);
        }

        public void ShowListOfCategories()
        {

            List<Category> categories = _eshop.GetListOfCategories();

            if (categories.Count > 0)
            {
                foreach (var category in categories)
                {
                    Console.WriteLine("ID: " + category.Id + " Name: " + category.Name);
                }
            }
            else
            {
                Console.WriteLine("The category list is empty");
            }
        }

        public void CreateNewProduct()
        {
            Console.WriteLine("Enter name of product");

            string nameOfProduct = Console.ReadLine();

            Product product = _eshop.CreateNewProduct(new Dto.NewProduct
            {
                Name = new string(nameOfProduct)
            });


            this.CallMenuSelectCategoryForProduct(product.Id);
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

        public void AddCategoryToProduct(int productId, int categoryId)
        {
            List<Category> categories = _eshop.GetListOfCategories();
            List<Product> products = _eshop.GetListOfProducts();

            Product? product = products.Find(p => p.Id == productId);
            Category? category = categories.Find(c => c.Id == categoryId);

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
