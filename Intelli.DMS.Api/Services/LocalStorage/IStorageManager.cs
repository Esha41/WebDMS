using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Intelli.DMS.Api.Services.LocalStorage
{
    public interface IStorageManager
    {
        /// <summary>
        /// Stores the file.
        /// </summary>
        /// <param name="file">The file stream.</param>
        /// <param name="filePath">The path of file including file name.</param>
        /// <returns>A Task of boolean result containing success or failure.</returns>
        Task StoreFile(IFormFile file, string filePath);

        /// <summary>
        /// Deletes the file.
        /// </summary>
        /// <param name="filePath">The path of file including file name.</param>
        void DeleteFile(string filePath);
    }
}
