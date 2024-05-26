// See https://aka.ms/new-console-template for more information
using console_store.Clases;
using System.Collections.Generic;

Console.WriteLine("Hello, World!");

List<Product> products = new List<Product>();

Boolean programWork = true;

while(programWork)
{ 

    Console.WriteLine("Select action");
    Console.WriteLine("1 - List of products");
    Console.WriteLine("2 - Add product");
    Console.WriteLine("Any button - Exit");

    string inputKey = Console.ReadLine();
    string inputValue = inputKey.ToString();

   switch(inputValue)
    {
        case "1":
            { 
                for(int i = 0;i< products.Count;i++)
                {
                    Console.WriteLine("Id: " + products[i].id + " name: " + products[i].name);
                }

                Console.WriteLine("Number of procuts " + products.Count);
            } break;
        case "2":
            {
                Console.WriteLine("Enter name of product");

                string nameOfProduct = Console.ReadLine();

                products.Add(new Product { id = products.Count + 1, name = nameOfProduct });
            } break;
        default:
            {
                programWork = false;
            } break;
    }

    Console.WriteLine("==========================");
}
