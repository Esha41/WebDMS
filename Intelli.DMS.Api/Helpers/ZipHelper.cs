using Intelli.DMS.Domain.Core.Repository;
using Intelli.DMS.Domain.Database;
using Intelli.DMS.Domain.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.DMS.Api.Helpers
{
    /// <summary>
    /// The string hasher.
    /// </summary>
    public  class ZipHelper
    {
        private readonly DMSContext _context;
        private readonly IRepository<BopDictionary> _repositoryBop;
        public ZipHelper(DMSContext context)
        {
            _context = context;
            _repositoryBop = new GenericRepository<BopDictionary>(context);
        }


        public Byte[] ConvertBatchMetaResponseToJsonFile(List<BatchMetum> batchMetas, String filename)
        {
            MemoryStream memoryStream = new MemoryStream();
            TextWriter tw = new StreamWriter(memoryStream);
            int indexNumber = 1;
            Dictionary<String, String> batchMetaDictionary = new();
            foreach (var batchMeta in batchMetas)
            {


                String fieldValue = "";
                if (batchMeta.DocumentClassField.DictionaryTypeId != null)
                {
                    var fieldvalue = _repositoryBop.Query(x => x.Id == int.Parse(batchMeta.FieldValue)).FirstOrDefault();
                    fieldValue = fieldvalue.Value;
                }
                else
                {
                    fieldValue = batchMeta.FieldValue;
                }
                batchMetaDictionary.Add($"{indexNumber}_{batchMeta.DocumentClassField.Uilabel.Replace(" ", "")}", fieldValue);
                indexNumber++;

            }
            tw.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(batchMetaDictionary));
            tw.Flush();
            tw.Close();
            tw.Dispose();
            return memoryStream.ToArray();
        }

        public byte[] CompressToZip(Dictionary<string, byte[]> fileList)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    foreach (var file in fileList)
                    {
                        var demoFile = archive.CreateEntry(file.Key);




                        using (var originalFileStream = new MemoryStream(file.Value))
                        using (var zipEntryStream = demoFile.Open())
                        {
                            //Copy the attachment stream to the zip entry stream
                            originalFileStream.CopyTo(zipEntryStream);
                        }

                    }
                    archive.Dispose();

                }



                return memoryStream.ToArray();


            }

        }
    }
}
