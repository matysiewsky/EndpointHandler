using System.Net.Http;
using System.Threading.Tasks;

namespace EndpointHandler.Domain.Interfaces.Services
{
    public interface IHttpService
    {
        Task<string> GetResponseAsync();
    }
}