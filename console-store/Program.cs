// See https://aka.ms/new-console-template for more information
using ConsoleStore.Clases;
using System.Collections.Generic;

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

                foreach(Product product in products)
                {
                    Console.WriteLine("Id: " + product.Id + " name: " + product.Name);
                }

                Console.WriteLine("Number of procuts " + products.Count);
            } break;
        case "2":
            {
                Console.WriteLine("Enter name of product");

                string nameOfProduct = Console.ReadLine();

                products.Add(new Product { Id = products.Count + 1, Name = nameOfProduct });
            } break;
        default:
            {
                programWork = false;
            } break;
    }

    Console.WriteLine("==========================");
}
