using System;
using System.Net.Http;
using System.Threading.Tasks;
using EndpointHandler.Domain;
using EndpointHandler.Domain.Interfaces.Services;
using EndpointHandler.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EndpointHandler.ConsoleUI
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            IConfiguration configuration = ReadConfiguration();

            IHttpService httpService = new HttpService(new HttpClient(), configuration["EndpointToHandle"]);
            IFileService fileService = new FileService(configuration["FilePath"]);

            try
            {
                string response = await httpService.GetResponseAsync();
                ApiRecord apiRecord = ApiRecordFactory.DeserializeJsonIntoRecord(response);

                fileService.AppendLineToFile(apiRecord.ToString());
                Console.WriteLine(@$"The following result is saved to file:
'{apiRecord}'");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                Console.WriteLine($"The program flow ended. Press any key to close the application.");
                Console.ReadLine();
            }
        }

        private static IConfiguration ReadConfiguration()
            => new ConfigurationBuilder()
                    .AddJsonFile(path: "appsettings.Development.json", optional: false, reloadOnChange: false)
                    .Build();
    }
}
