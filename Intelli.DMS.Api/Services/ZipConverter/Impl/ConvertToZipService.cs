using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Intelli.DMS.Api.Services.ConvertToZip.Impl
{
    /// <summary>
    /// The Convert To Zip service implementation
    /// </summary>
    public class ConvertToZipService : IConvertToZipService
    {
        
        public ConvertToZipService()
        {
            
        }

        /// <summary>
        /// Convert Single File To Zip.
        /// <param name="pathToZip">The Path Of file.</param>
        /// </summary>
        /// <returns>A Zip file.</returns>
        public byte[] ConvertSingleFileToZip( string pathToZip)
        {
            string zipFileName = "MyFile.zip";
            var zipFilePath = Path.Combine(Path.GetDirectoryName(pathToZip), zipFileName);

            using (ZipOutputStream zipOutputStream = new ZipOutputStream(System.IO.File.Create(zipFilePath)))
            {
                zipOutputStream.SetLevel(9);
                byte[] buffer = new byte[4096];

                ZipEntry entry = new ZipEntry(Path.GetFileName(pathToZip));

                entry.DateTime = DateTime.Now;
                entry.IsUnicodeText = true;
                zipOutputStream.PutNextEntry(entry);

                using (FileStream fileStream = File.OpenRead(pathToZip))
                {
                    int sourceByte;
                    do
                    {
                        sourceByte = fileStream.Read(buffer, 0, buffer.Length);
                        zipOutputStream.Write(buffer, 0, sourceByte);
                    }
                    while (sourceByte > 0);
                }
                zipOutputStream.Finish();
                zipOutputStream.Flush();
                zipOutputStream.Close();


            }
            byte[] finalFile = File.ReadAllBytes(zipFilePath);

            if (File.Exists(zipFilePath))
            {
                File.Delete(zipFilePath);
            }

            return finalFile;
        }

        /// <summary>
        /// Convert Folder File To Zip.
        /// <param name="pathToZip">The Path Of file.</param>
        /// </summary>
        /// <returns>A Zip file.</returns>
        public byte[] ConvertFolderToZip( string pathToZip)
        {
            string zipFileName = "MyFile.zip";
            var zipFilePath = Path.Combine(Path.GetDirectoryName(pathToZip), zipFileName);
            using (ZipOutputStream zipOutputStream = new ZipOutputStream(System.IO.File.Create(zipFilePath)))
            {
                zipOutputStream.SetLevel(9);
                byte[] buffer = new byte[4096];


                string[] fileInDirectory = Directory.GetFiles(pathToZip);

                foreach (string files in fileInDirectory)
                {
                    ZipEntry entry = new ZipEntry(Path.GetFileName(files));
                    entry.DateTime = DateTime.Now;
                    entry.IsUnicodeText = true;
                    zipOutputStream.PutNextEntry(entry);

                    using (FileStream fileStream = File.OpenRead(files))
                    {
                        int sourceByte;
                        do
                        {
                            sourceByte = fileStream.Read(buffer, 0, buffer.Length);
                            zipOutputStream.Write(buffer, 0, sourceByte);
                        }
                        while (sourceByte > 0);
                    }
                }
                zipOutputStream.Finish();
                zipOutputStream.Flush();
                zipOutputStream.Close();
            }

            byte[] finalFile = File.ReadAllBytes(zipFilePath);

            if (File.Exists(zipFilePath))
            {
                File.Delete(zipFilePath);
            }

            return finalFile;

        }

        

       
    }
}
