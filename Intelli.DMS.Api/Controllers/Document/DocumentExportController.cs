using AutoMapper;
using Intelli.DMS.Api.Helpers;
using Intelli.DMS.Domain.Core.Helpers;
using Intelli.DMS.Domain.Core.Repository;
using Intelli.DMS.Domain.Database;
using Intelli.DMS.Domain.Model;
using Intelli.DMS.EventBus.RabbitMQ.Event;
using Intelli.DMS.EventBus.RabbitMQ.Sender;
using Intelli.DMS.Shared.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Intelli.DMS.Api.Controllers.Document
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DocumentExportController : BaseController
    {
        private readonly string _repositoryRoot;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private readonly IRepository<Batch> _repositoryBatch;
        private readonly IRepository<BatchMetum> _repositoryBatchMeta;
        private readonly IRepository<BatchItem> _repositoryBatchItem;

        private readonly ZipHelper _zipHelper;
        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentExportController"/> class.
        /// </summary>
        /// <param name="context">Instance of <see cref="DMSContext"/> will be injected</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/> will be injected</param>
        /// <param name="logger">Instance of <see cref="ILogger"/> will be injected</param>
        public DocumentExportController(DMSContext context,
                                        ILogger<Batch> logger,
                                        IEventSender sender,
                                        IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _repositoryRoot = _configuration.GetSection("DocumentUploadPath").Value;
            _repositoryBatch = new GenericRepository<Batch>(context);
            _repositoryBatchMeta = new GenericRepository<BatchMetum>(context);
            _repositoryBatchItem = new GenericRepository<BatchItem>(context);
            _zipHelper = new ZipHelper(context);

            ((GenericRepository<Batch>)_repositoryBatch).AfterSave = (logs) =>
            ((GenericRepository<BatchMetum>)_repositoryBatchMeta).AfterSave = (logs) =>
            ((GenericRepository<BatchItem>)_repositoryBatchItem).AfterSave = (logs) =>
             sender.SendEvent(new MQEvent<List<AuditEntry>>(AUDIT_EVENT_NAME, (List<AuditEntry>)logs));

        }

        /// <summary>
        /// Export Single Document
        /// </summary>
        /// <param name="batchItemId"></param>
        /// <returns>Zip File</returns>
        [HttpGet("{batchItemId}")]
        public IActionResult ExportSingleDocument(int batchItemId)
        {
            try
            {

                var batchItem = _repositoryBatchItem.Query(x => x.Id == batchItemId).FirstOrDefault();
                var batch = _repositoryBatch.Query(x => x.Id == batchItem.BatchId).FirstOrDefault();

                // get latest batch meta version
                //var batchMetaVersion = _repositoryDocumentVersion.Query(x => x.BatchId == batchItem.BatchId && x.FileName == batchItem.FileName)
                //                                                 .OrderByDescending(x=>x.Version).FirstOrDefault();

                // get the values of batch meta based upon the batch meta version id
                var batchMeta = _repositoryBatchMeta.Query(x => x.BatchId == batchItem.BatchId && x.DocumentVersionId == batchItem.DocumentVersionId && x.BatchItemReference == batchItem.BatchItemReference)
                    .Include(x => x.DocumentClassField).ToList();


                String filename = batchItem.FileName.Split('.')[0] + ".json";
                Byte[] jsonFile = null;
                if (batchMeta != null && batchMeta.Any())
                {
                    /// convert the batch meta values in the form of the json
                    jsonFile = _zipHelper.ConvertBatchMetaResponseToJsonFile(batchMeta, filename);
                }

                // get repository data path 
                string batchFilePath = RepositoryHelper.BuildRepositoryFilePath(_repositoryRoot,
                                                                          batch.Id, batchItem.Id, batchItem.FileName, batch.CreatedDate);


                Dictionary<string, byte[]> fileList = new();
                fileList.Add(filename, jsonFile);
                if (System.IO.File.Exists(batchFilePath))
                {
                    fileList.Add(batchItem.FileName, System.IO.File.ReadAllBytes(batchFilePath));
                }

                // compress list of files in one zip
                var zipFile = _zipHelper.CompressToZip(fileList);
                // return file
                return File(zipFile, "application/zip", "batchMeta.zip");


            }
            catch (Exception e)
            {
                // Log error message
                _logger.LogError("{0}: {1}", e.Message, e);
                return BadRequest(new
                {
                    Errors = e,
                    e.Message
                }); ;
            }

        }




        /// <summary>
        /// Export Client Repository
        /// </summary>
        /// <param name="batchItemId"></param>
        /// <returns>Zip File</returns>
        [HttpGet("ExportFolder/batchId")]
        public IActionResult ExportRepository(int batchId)
        {
            try
            {

                Dictionary<string, byte[]> fileList = new();
                var batch = _repositoryBatch.Query(x => x.Id == batchId)
                                            .Include(x => x.Customer)
                                            .FirstOrDefault();
                var batchItems = _repositoryBatchItem.Query(x => x.BatchId == batchId).ToList();
                batchItems = FilterBatchMetaBasedUponDocumentVersion(batchItems);
                if (batchItems != null && batchItems.Any())
                {
                    foreach (var batchItem in batchItems)
                    {
                        // get latest batch meta version
                        //var batchMetaVersion = _repositoryDocumentVersion.Query(x => x.BatchId == batchItem.BatchId && x.FileName == batchItem.FileName)
                        //                                                 .OrderByDescending(x => x.Version).FirstOrDefault();
                        // get the values of batch meta based upon the batch meta version id
                        var batchMeta = _repositoryBatchMeta.Query(x => x.BatchId == batchItem.BatchId && x.DocumentVersionId == batchItem.DocumentVersionId && x.BatchItemReference == batchItem.BatchItemReference)
                            .Include(x => x.DocumentClassField).ToList();

                        String filename = batchItem.FileName.Split('.')[0] + ".json";
                        Byte[] jsonFile = null;
                        if (batchMeta != null && batchMeta.Any())
                        {
                            /// convert the batch meta values in the form of the json
                            jsonFile = _zipHelper.ConvertBatchMetaResponseToJsonFile(batchMeta, filename);
                            fileList.Add(filename, jsonFile);
                        }

                        // get repository data path 
                        string batchFilePath = RepositoryHelper.BuildRepositoryFilePath(_repositoryRoot,
                                                                                  batch.Id, batchItem.Id, batchItem.FileName, batch.CreatedDate);

                        if (System.IO.File.Exists(batchFilePath))
                        {
                            fileList.Add(batchItem.FileName, System.IO.File.ReadAllBytes(batchFilePath));
                        }
                    }

                }


                // compress list of files in one zip
                var zipFile = _zipHelper.CompressToZip(fileList);
                // return file
                return File(zipFile, "application/zip", $"{batch.Customer.FirstName} {batch.Customer.LastName}.zip");

            }
            catch (Exception e)
            {
                // Log error message
                _logger.LogError("{0}: {1}", e.Message, e);
                return BadRequest(new
                {
                    Errors = e,
                    e.Message
                });
            }

        }

        /// <summary>
        /// Filter BatchMeta Based Upon Document Version
        /// </summary>
        /// <param name="batchItem"></param>
        /// <returns></returns>
        private static List<BatchItem> FilterBatchMetaBasedUponDocumentVersion(List<BatchItem> batchItem)
        {
            List<BatchItem> filteredList = new();
            foreach (var meta in batchItem)
            {
                var isFileExitInFilterList = filteredList.Find(x => x.FileName == meta.FileName);
                if (isFileExitInFilterList == null)
                {
                    var filteredBatch = batchItem.FindAll(x => x.FileName == meta.FileName).OrderByDescending(x => x.DocumentVersion).FirstOrDefault();
                    filteredList.Add(filteredBatch);
                }
            }
            return filteredList;
        }
    }
}
