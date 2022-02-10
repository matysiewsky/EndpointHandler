using System.Threading.Tasks;

namespace EndpointHandler.Domain.Interfaces.Services
{
    public interface IHttpService
    {
        Task GetResponseAsync();
    }
}