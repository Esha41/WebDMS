using Intelli.DMS.Api.Services.LocalStorage;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace Intelli.DMS.Api.Services
{
    public class FileSystemStorageService : IStorageManager
    {
        /// <summary>
        /// Deletes the file.
        /// </summary>
        /// <param name="filePath">The path of file including file name.</param>
        public void DeleteFile(string filePath)
        {
            File.Delete(filePath);
        }

        /// <summary>
        /// Stores the file.
        /// </summary>
        /// <param name="file">The file stream.</param>
        /// <param name="filePath">The path of file including file name.</param>
        /// <returns>A Task of boolean result containing success or failure.</returns>
        public async Task StoreFile(IFormFile file, string filePath)
        {
            var pathOnly = Path.GetDirectoryName(filePath);

            if (!Directory.Exists(pathOnly)) Directory.CreateDirectory(pathOnly);

            using var stream = new FileStream(filePath, FileMode.Create);

            await file.CopyToAsync(stream);
        }
    }
}
