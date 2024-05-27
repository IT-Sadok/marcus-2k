using ConsoleStore.Clases;

Console.WriteLine("Hello, World!");

List<Product> products = new List<Product>();

var programWork = true;

while(programWork)
{ 
    Console.WriteLine("Select action");
    Console.WriteLine("1 - List of products");
    Console.WriteLine("2 - Add product");
    Console.WriteLine("Any button - Exit");

    string inputKey = Console.ReadLine().ToString();

   switch(inputKey)
    {
        case "1":
            {
                PrintListOfProducts();
            } break;
        case "2":
            {
                CreateNewProduct();
            } break;
        default:
            {
                programWork = false;
            } break;
    }

    Console.WriteLine("==========================");
}

void PrintListOfProducts()
{

    foreach(Product product in products)
    {
        Console.WriteLine("Id: " + product.Id + " name: " + product.Name);
    }

    Console.WriteLine("Number of procuts " + products.Count);
}

void CreateNewProduct()
{
    Console.WriteLine("Enter name of product");

    string nameOfProduct = Console.ReadLine();

    products.Add(new Product { Id = products.Count + 1, Name = nameOfProduct });
}