using ConsoleStore.Classes.Dto;

namespace ConsoleStore.Classes
{
    public class EShop
    {
        private FileManager FileManager = new FileManager();

        private List<Product> _products = new List<Product>();

        public List<Category> GetListOfCategories()
        {
            return this.FileManager.GetListOfCategories();
        }

        public List<Product> GetListOfProducts()
        {
            return this._products;
        }

        private int GetNewProductId()
        {
            return this._products.Count + 1;
        }

        public Product CreateNewProduct(NewProduct body)
        {
            int productId = this.GetNewProductId();

            Product partialProduct = new Product { Id = productId, Name = body.Name, CategoryId = null };

            this._products.Add(partialProduct);


            return partialProduct;
        }


        public OperationResult AddCategoryToProduct(int productId, int categoryId)
        {
            Product? product = _products.Find(p => p.Id == productId);
            Category? category = this.GetListOfCategories().Find(c => c.Id == categoryId);

            if (product != null && category != null)
            {
                product.CategoryId = categoryId;
                product.Category = category;

                return new OperationResult
                {
                    Product = product,
                    Message = $"Product {product.Name}'s category updated to {categoryId}"
                };
            }


            return new OperationResult
            {
                Product = null,
                Message = $"Product with ID {productId} not found."
            };
        }
    }
}
