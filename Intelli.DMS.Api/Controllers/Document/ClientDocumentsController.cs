using Intelli.DMS.Api.Basic_Authorization;
using Intelli.DMS.Api.DTO;
using Intelli.DMS.Api.Helpers;
using Intelli.DMS.Domain.Core.Repository;
using Intelli.DMS.Domain.Database;
using Intelli.DMS.Domain.Model;
using Intelli.DMS.Shared.Mvc.Controllers;
using Intelli.DMS.Shared.Mvc.Resources;
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
    [BasicAuthentication]
    public class ClientDocumentsController : BaseController
    {
        private readonly string _repositoryRoot;
        private readonly ILogger _logger;
        private readonly DMSContext _context;
        private readonly IRepository<BatchItem> _repositoryBatchItem;
        private readonly IRepository<Batch> _repositoryBatch;
        private readonly IRepository<Client> _repositoryClient;
        private readonly IRepository<BatchItemPage> _repositoryBatchItemPage;
        private readonly IConfiguration _configuration;
        public ClientDocumentsController(DMSContext context,
           ILogger<ClientDocumentsController> logger,
           IConfiguration configuration)
        {
            _context = context;
            _repositoryClient = new GenericRepository<Client>(context);
            _repositoryBatchItem = new GenericRepository<BatchItem>(context);
            _repositoryBatchItemPage = new GenericRepository<BatchItemPage>(context);
            _repositoryBatch = new GenericRepository<Batch>(context);
            _configuration = configuration;
            _repositoryRoot = _configuration.GetSection("DocumentUploadPath").Value;
            _logger = logger;
        }

        /// <summary>
        /// Get All documents with client name.
        /// </summary>
        /// <param name="externalId">The External Id.</param>
        /// <returns>List of client documents</returns>
        [HttpGet("{externalId}")]
        public IActionResult Get(string externalId)
        {
            try
            {
                if (externalId == null)
                {
                    return BadRequest(MsgKeys.ObjectNotExists);
                }
                int companyId = BasicAuthStaticContants.CompanyId;
                var getCustomer = _repositoryClient.Query(s => s.ExternalId == externalId.ToString() && s.CompanyId == companyId)
                                                    .Include(x => x.ClientCompanyCustomFieldValues)
                                                     .ThenInclude(x => x.Field)
                                                    .FirstOrDefault();

                if (getCustomer == null)
                {
                    return BadRequest(MsgKeys.ObjectNotExists);
                }

                int clientId = getCustomer.Id;


                //Setting Response
                ClientDocumentsDTO response = new();
                List<ClientCompanyFieldValuesDTO> listClientCompanyFieldValuesDTO = new();
                response.CustomerId = clientId;
                response.CustomerName = getCustomer.FirstName + " " + getCustomer.LastName;
                response.ExternalId = getCustomer.ExternalId;

                if (getCustomer.ClientCompanyCustomFieldValues.Count > 0)
                {

                    foreach (var item in getCustomer.ClientCompanyCustomFieldValues)
                    {
                        ClientCompanyFieldValuesDTO clientCompanyFieldValuesDTO = new()
                        {
                            UiLabel = item?.Field?.Uilabel,
                            Value = item.RegisteredFieldValue
                        };
                        listClientCompanyFieldValuesDTO.Add(clientCompanyFieldValuesDTO);
                    }
                    response.ClientMetaData = listClientCompanyFieldValuesDTO;
                }
                var batchId = _repositoryBatch.Query(x => x.CustomerId == clientId).Select(x => x.Id).FirstOrDefault();
                if (batchId != 0)
                {

                    var groupDocumentClassIdBatchItemsWith = _context.BatchItems.Where(x => x.BatchId == batchId)
                                                                                .GroupBy(x => x.DocumentClassId)
                                                                                    .Select(x => x.Key.Value).ToList();
                    foreach (var item in groupDocumentClassIdBatchItemsWith)
                    {

                        var result = from batchPages in _repositoryBatchItemPage.Query(s => s.IsActive == true)
                                                                            .Include(a => a.BatchItem)
                                                                             .ThenInclude(x => x.DocumentClass)
                                                                            .Include(a => a.BatchItem)
                                                                             .ThenInclude(a => a.Batch)
                                                                              .ThenInclude(a => a.Customer)
                                     where batchPages.BatchItem.DocumentClassId == item
                                     select batchPages;


                        //generating base64string of all files
                        foreach (var items in result.ToList())
                        {
                            FileContentDTO fileContent = new();
                            fileContent.Files = new List<FileDTO>();
                            fileContent.Data = new List<FileContentMetaDataDTO>();
                            //getting full file path
                            var filePath = RepositoryHelper.BuildUrlFilePath(_repositoryRoot,
                                                                                  items.BatchItem.BatchId,
                                                                                  items.FileName,
                                                                                  items.BatchItem.Batch.CreatedDate);

                            //if file directory exist in server too then return
                            if (System.IO.File.Exists(filePath))
                            {
                                // converting to base 64 string
                                byte[] fileByte = System.IO.File.ReadAllBytes(filePath);
                                string base64String = Convert.ToBase64String(fileByte);
                                fileContent.DocumentClassCode = items.BatchItem.DocumentClass.DocumentClassCode; ;
                                fileContent.Files.Add(new FileDTO() { base64String = base64String, MimeType = GetContentType(items.FileName) });
                                var batchItem = _repositoryBatchItem.Query(x => x.BatchItemReference == items.BatchItem.BatchItemReference)
                                                                          .OrderBy(x => x.Id)
                                                                          .Include(x => x.BatchItemFields)
                                                                           .ThenInclude(x => x.DocumentClassField)
                                                                          .LastOrDefault();
                                foreach (var itemBatchItemField in batchItem.BatchItemFields)
                                {
                                    FileContentMetaDataDTO fileContentMetaDataDTO = new();
                                    fileContentMetaDataDTO.UiLabel = itemBatchItemField.DocumentClassField.Uilabel;
                                    fileContentMetaDataDTO.FieldValue = itemBatchItemField.RegisteredFieldValue;
                                    fileContent.Data.Add(fileContentMetaDataDTO);
                                }
                                if (fileContent.DocumentClassCode != null)
                                    response.Documents.Add(fileContent);
                            }
                        }

                    }
                }
                _logger.LogInformation("Get documents of client Id: {0}", clientId);

                return Ok(response);
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

        private string GetContentType(string fileName)
        {
            string contentType;
            new Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider().TryGetContentType(fileName, out contentType);
            return contentType ?? "application/octet-stream";
        }



        /// <summary>
        /// Get All documents with client External id.
        /// </summary>
        /// <param name="externalId">The External Id.</param>
        /// <returns>List of client documents</returns>
        [HttpGet("GetDocuments/{externalId}")]
        [Produces("application/json")]
        public IActionResult GetDocuments(string externalId)
        {
            try
            {
                if (externalId == null)
                {
                    return BadRequest(MsgKeys.ObjectNotExists);
                }
                int companyId = BasicAuthStaticContants.CompanyId;
                var getCustomer = _repositoryClient.Query(s => s.ExternalId == externalId.ToString() && s.CompanyId == companyId)
                                                    .Include(x => x.ClientCompanyCustomFieldValues)
                                                     .ThenInclude(x => x.Field)
                                                    .FirstOrDefault();

                if (getCustomer == null)
                {
                    return BadRequest(MsgKeys.ObjectNotExists);
                }

                int clientId = getCustomer.Id;


                //Setting Response
                ClentDocumentDetailReponseDTO response = new();
                List<ClientDocumentDetailDTO> listClientDocumentDetailDTO = new();

                var batchId = _repositoryBatch.Query(x => x.CustomerId == clientId).Select(x => x.Id).FirstOrDefault();
                if (batchId != 0)
                {

                    var groupDocumentClassIdBatchItemsWith = _context.BatchItems.Where(x => x.BatchId == batchId)
                                                                                .GroupBy(x => x.BatchItemReference)
                                                                                    .Select(x => x.Key).ToList();
                    foreach (var item in groupDocumentClassIdBatchItemsWith)
                    {

                        var result = from batchitems in _repositoryBatchItem.Query(s => s.IsActive == true)
                                                                             .Include(x => x.DocumentClass)
                                                                              .ThenInclude(x => x.DocumentType)
                                     where batchitems.BatchItemReference == item
                                     orderby batchitems.Id descending
                                     select batchitems;


                        //generating base64string of all files
                        foreach (var batchItem in result.ToList())
                        {
                            ClientDocumentDetailDTO clientDocumentDetailDTO = new()
                            {
                                DocumentId = batchItem.Id,
                                DocumentInsertDate = UnixTimeStampToDateTime(batchItem.CreatedAt),
                                DocumentCategoryId = (batchItem?.DocumentClass.Id == null) ? 0 : batchItem.DocumentClass.Id,
                                DocumentCategoryDescription = batchItem?.DocumentClass?.DocumentClassName,
                                MasterDocumentCategoryDescription = batchItem?.DocumentClass?.DocumentType?.DocumentTypeName

                            };
                            listClientDocumentDetailDTO.Add(clientDocumentDetailDTO);
                            break;
                        }
                        response.Documents = listClientDocumentDetailDTO.OrderBy(x => x.DocumentId).ToList();
                    }
                }
                _logger.LogInformation("Get documents of client Id: {0}", clientId);

                return Ok(response);
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
        private static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            // Unix time stamp is seconds past epoch
            System.DateTime dtDateTime = new(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        /// <summary>
        /// Get All documents with client name.
        /// </summary>
        /// <param name="externalId">The External Id.</param>
        /// <returns>List of client documents</returns>
        [HttpGet("GetDocument/{documentId}")]
        public IActionResult GetDocuments(int documentId)
        {
            try
            {
                if (documentId == 0)
                {
                    return BadRequest(MsgKeys.ObjectNotExists);
                }
                int companyId = BasicAuthStaticContants.CompanyId;
                var resultBatchItemRef = _repositoryBatchItem.Query(x => x.Id == documentId && x.CompanyId == companyId)?.FirstOrDefault()?.BatchItemReference;
                var result = _repositoryBatchItem.Query(x => x.BatchItemReference == resultBatchItemRef).Include(x => x.Batch).ToList().LastOrDefault();
                if (result == null)
                {
                    return BadRequest(MsgKeys.ObjectNotExists);
                }
                FileBase64ResponseDTO response = new();
                var file = result;
                //getting full file path
                var filePath = RepositoryHelper.BuildUrlFilePath(_repositoryRoot,
                                                                      file.BatchId,
                                                                      file.FileName,
                                                                      file.Batch.CreatedDate);

                //if file directory exist in server too then return
                if (System.IO.File.Exists(filePath))
                {
                    // converting to base 64 string
                    byte[] fileByte = System.IO.File.ReadAllBytes(filePath);
                    string base64String = Convert.ToBase64String(fileByte);
                    response.File = new FileDTO() { base64String = base64String, MimeType = GetContentType(file.FileName) };

                }
                else
                {
                    return BadRequest($"{MsgKeys.FileNotFoundInDirectory} Directory :: {filePath}");
                }

                //generating base64string of all files

                _logger.LogInformation("Get document base64 String and its mimeType of Document Id: {0}", documentId);

                return Ok(response);
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
    }
}
