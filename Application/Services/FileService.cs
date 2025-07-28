using System;
using System.IO;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IFileService
    {
        Task<string> UploadFileAsync(Stream fileStream, string fileName);
        Task<Stream> DownloadFileAsync(string fileUrl);
    }

    public class FileService : IFileService
    {
        private readonly string _baseDirectory = @"D:/fileADN";
        private readonly string _baseUrl = "https://yourdomain.com/files/"; // Adjust as needed

        public async Task<string> UploadFileAsync(Stream fileStream, string fileName)
        {
            if (!Directory.Exists(_baseDirectory))
                Directory.CreateDirectory(_baseDirectory);

            var filePath = Path.Combine(_baseDirectory, fileName);
            using (var fileStreamOut = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                await fileStream.CopyToAsync(fileStreamOut);
            }
            // Return a URL for downloading
            return _baseUrl + Uri.EscapeDataString(fileName);
        }

        public async Task<Stream> DownloadFileAsync(string fileUrl)
        {
            // Extract file name from URL
            var fileName = Path.GetFileName(new Uri(fileUrl).LocalPath);
            var filePath = Path.Combine(_baseDirectory, fileName);
            if (!File.Exists(filePath))
                throw new FileNotFoundException("File not found", fileName);
            // Return file stream
            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return memory;
        }
    }
} 