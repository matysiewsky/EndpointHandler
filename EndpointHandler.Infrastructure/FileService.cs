using System;
using System.Collections.Generic;
using System.IO;
using EndpointHandler.Domain.Interfaces.Services;

namespace EndpointHandler.Infrastructure
{
    public class FileService: IFileService
    {
        private string FilePath { get; }
        public FileService(string filePath)
        {
            if (!CheckIfPathCorrect(filePath)) throw new ArgumentException("File name can not be empty or null.");

            FilePath = filePath;
        }

        private static bool CheckIfPathCorrect(string filePath)
            => !string.IsNullOrEmpty(filePath);

        public void AppendLineToFile(string lineToWrite)
        {
            using StreamWriter writer = File.AppendText(FilePath);
            writer.WriteLine(lineToWrite);
        }

        public void AppendLinesToFile(IEnumerable<string> linesToWrite)
            => File.AppendAllLines(FilePath, linesToWrite);
    }
}