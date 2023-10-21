// See https://aka.ms/new-console-template for more information

using System.Configuration;
using System.Runtime.CompilerServices;
using System.Text;
using AcquireXModel;
using AquirexServices.implementation;

class AquireXProgram
{
    static void Main(string[] args)
    {
        Console.WriteLine("[+].........WORK STARTED..........");
        DowWork();
        Console.WriteLine("[+].........WORK COMPLETED..........");


    }

    private async static void DowWork()
    {

        RestProducts ProdService = new RestProducts();
        List<ProductInfo> prod = new List<ProductInfo>();
        string[] endPoints = ConfigurationManager.AppSettings["endpoints"].Split(",");
        foreach (string endpoint in endPoints)
        {
            Thread thread = new Thread(async () =>
            {
                Console.WriteLine("THREAD STARTED " + endpoint);

                Products PrdData = await ProdService.GetProducts(endpoint);

                Console.WriteLine("Received Response " + endpoint);

                foreach (ProductInfo prd in PrdData.ProductList)
                {
                    if (prd.Upc != null && !prod.Any(x => prd.Upc == x.Upc)) prod.Add(prd);

                }
            });
            thread.Name = endpoint;
            thread.Start();
            thread.Join();
        }

        foreach (var uniqueProd in prod)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("------------------------------------\n");
            stringBuilder.Append($"itemCode : {uniqueProd.ItemCode}\n");
            stringBuilder.Append($"UPC : {uniqueProd.Upc}\n");
            stringBuilder.Append("------------------------------------\n");
            Console.Write(stringBuilder.ToString());
            Console.WriteLine("\n");
        }
        Console.WriteLine("\n");
        Console.WriteLine($"Total Unique Products : " + prod.Count);
        Console.ReadKey();
    }

}