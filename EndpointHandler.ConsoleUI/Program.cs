using System;
using System.Net.Http;
using System.Threading.Tasks;
using EndpointHandler.Domain;
using EndpointHandler.Domain.Interfaces.Services;
using EndpointHandler.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace EndpointHandler.ConsoleUI
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            IConfiguration configuration = ReadConfiguration();

            (IHttpService httpService, IFileService fileService) = ConfigureServices(configuration);

            await RunProgramFlow(httpService, fileService);
        }

        private static IConfiguration ReadConfiguration()
            => new ConfigurationBuilder()
                    .AddJsonFile(path: "appsettings.Development.json", optional: false, reloadOnChange: false)
                    .Build();

        private static (IHttpService, IFileService) ConfigureServices(IConfiguration configuration)
            => (new HttpService(new HttpClient(), configuration["EndpointToHandle"]),
                new FileService(configuration["FilePath"]));
        private static async Task RunProgramFlow(IHttpService httpService, IFileService fileService)
        {
            try
            {
                string response = await GetResponseAsString(httpService);
                ApiRecord apiRecord = DeserializeJsonStringIntoApiRecord(response);
                SaveRecordToFile(fileService, apiRecord);
            }
            catch (Exception ex)
            {
                Log($"Error: {ex.Message}");
            }
            finally
            {
                Log("The program flow ended. Press any key to close the application.");
                Console.ReadLine();
            }
        }
        private static async Task<string> GetResponseAsString(IHttpService httpService)
            => await httpService.GetResponseAsync();
        private static ApiRecord DeserializeJsonStringIntoApiRecord(string response)
            => ApiRecordFactory.DeserializeJsonIntoRecord(response);
        private static void SaveRecordToFile(IFileService fileService, ApiRecord apiRecord)
        {
            fileService.AppendLineToFile(apiRecord.ToString());

            Log(@$"The following result is saved to file:
'{apiRecord}'");
        }
        private static void Log(string messageToLog)
            => Console.WriteLine($"INFO: {messageToLog}");
    }
}
