using System.IO;

namespace Intelli.DMS.Api.Services.ConvertToZip
{
    public interface IConvertToZipService
    {
        byte[] ConvertSingleFileToZip(string pathToZip);

        byte[] ConvertFolderToZip(string pathToZip);
    }
}
