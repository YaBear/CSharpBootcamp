using d05.Nasa;
using d05.Nasa.Apod;
using Microsoft.Extensions.Configuration;

namespace d05.Host
{
    class Program
    {
        INasaClient<string, int>? nasaClientOne;
        INasaClient<int, int>? nasaClientTwo;
        INasaClient<bool, double>? nasaClientThree;

        static async Task Main(string[] args)
        {
            if (args.Length == 2 && args[0] == "apod" && Int32.TryParse(args[1], out int count))
            {
                try
                {
                    IConfiguration configuration = new ConfigurationBuilder()
                         .SetBasePath(Directory.GetCurrentDirectory())
                         .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                         .Build();
                    ApodClient apodClient = new()
                    {
                        ApiKey = configuration["ApiKey"]
                    };
                    var result = await apodClient.GetAsync(count);
                    foreach (var item in result)
                    {
                        Console.WriteLine(item);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}