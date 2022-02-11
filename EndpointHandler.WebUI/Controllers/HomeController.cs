using System.Threading.Tasks;
using EndpointHandler.Domain;
using EndpointHandler.Domain.Interfaces.Services;
using EndpointHandler.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EndpointHandler.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpService _httpService;
        private readonly IFileService _fileService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IHttpService httpService, IFileService fileService, ILogger<HomeController> logger)
        {
            _httpService = httpService;
            _fileService = fileService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            string response = await _httpService.TryGetResponseOrTimeout();
            ApiRecord apiRecord = DeserializeJsonStringIntoApiRecord(response);
            SaveRecordToFile(apiRecord);

            return View(apiRecord);
        }

        public async Task<string> GetResponseAndSave()
            => await _httpService.TryGetResponseOrTimeout();

        private ApiRecord DeserializeJsonStringIntoApiRecord(string response)
            => ApiRecordFactory.DeserializeJsonIntoRecord(response);
        private void SaveRecordToFile(ApiRecord apiRecord)
        {
            _fileService.AppendLineToFile(apiRecord.ToString());

            _logger.Log(LogLevel.Information,@$"The following result is saved to file:
'{apiRecord}'");
        }
    }
}