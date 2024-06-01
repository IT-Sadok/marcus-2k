using ConsoleStore.Clases;

Console.WriteLine("Hello, World!");

Web web = new Web();

var programWork = true;

while (programWork)
{
    string inputKey = web.SelectAction();

    switch (inputKey)
    {
        case "1":
            {
                web.ShowListOfProducts();
            }
            break;
        case "2":
            {
                web.CreateNewProduct();
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
