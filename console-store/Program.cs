using ConsoleStore.Clases;

Console.WriteLine("Hello, World!");

Store store = new Store();

var programWork = true;

while (programWork)
{
    string inputKey = store.SelectAction();

    switch (inputKey)
    {
        case "1":
            {
                store.PrintListOfProducts();
            }
            break;
        case "2":
            {
                store.CreateNewProduct();
            }
            break;
        default:
            {
                programWork = false;
            }
            break;
    }

    Console.WriteLine("==========================");
}
