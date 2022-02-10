using System.Collections.Generic;

namespace EndpointHandler.Domain.Interfaces.Services
{
    public interface IFileService
    {
        void AppendLineToFile(string lineToWrite);
        void AppendLinesToFile(IEnumerable<string> linesToWrite);
    }
}