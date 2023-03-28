using AutoMapper;
using Intelli.DMS.Api.DTO;
using Intelli.DMS.Api.Services.DocumentUpload;
using Intelli.DMS.Domain.Core.Repository;
using Intelli.DMS.Domain.Database;
using Intelli.DMS.Domain.Model;
using Intelli.DMS.Domain.Repository;
using Intelli.DMS.Domain.Repository.Impl;
using Intelli.DMS.Shared.Mvc.Controllers;
using Intelli.DMS.Shared.Mvc.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;

namespace Intelli.DMS.Api.Controllers.Document
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DocumentUploadController : BaseController
    {
        private readonly IDocumentUploadService _documentUploadService;
        private readonly ILogger _logger;
        private readonly IRepository<Batch> _repositoryBatch;
        private readonly IRepository<DocumentsCheckedOut> _repositoryDocumentsCheckedOut;
        private readonly IRepository<Client> _repositoryClient;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentUploadController"/> class.
        /// </summary>
        /// <param name="context">Instance of <see cref="DMSContext"/> will be injected</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/> will be injected</param>
        /// <param name="logger">Instance of <see cref="ILogger"/> will be injected</param>
        public DocumentUploadController(DMSContext context,
                                        ILogger<Batch> logger,
                                        IMapper mapper,
                                        IDocumentUploadService documentUploadService)
        {
            _documentUploadService = documentUploadService;
            _logger = logger;
            _mapper = mapper;
            _repositoryBatch = new GenericRepository<Batch>(context);
            _repositoryDocumentsCheckedOut = new GenericRepository<DocumentsCheckedOut>(context);
            _repositoryClient = new GenericRepository<Client>(context);
        }

        /// <summary>
        /// Upload contract
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("UploadContract")]
        public async Task<IActionResult> UploadContract(UploadContractDTO dto)
        {
            try
            {
                // Checking if the passed DTO is valid
                if (!ModelState.IsValid || dto == null)
                    return BadRequest(MsgKeys.InvalidInputParameters);

                //get client
                var getClient= _repositoryClient.Query(x=>x.JMBG==dto.ClientInfo.JMBG)
                                    .FirstOrDefault();

                //add client if not exist already
                if (getClient == null)
                {
                    getClient = _mapper.Map<Client>(dto.ClientInfo);
                    getClient.CompanyId = Company.FixedId;
                    getClient.FirstName =string.Empty;
                    getClient.LastName = string.Empty;

                    _repositoryClient.Insert(getClient);
                    _repositoryClient.SaveChanges(UserName, null, null);
                }
                dto.ClientInfo.Id = getClient.Id;
                dto.ClientInfo.CompanyId = getClient.CompanyId;
                
                await _documentUploadService.UploadContract(dto,
                                                            UserName,
                                                            UserId);
            }
            catch (Exception e)
            {
                //log error
                _logger.LogError("{0}: {1}", e.Message, e);

                return BadRequest(e.Message);
            }

            return Ok();
        }

        /// <summary>
        /// Upload document
        /// </summary>
        /// <param name="file"></param>
        /// <param name="documentClassId"></param>
        /// <param name="clientId"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        [HttpPost("UploadDocument/{documentClassId}/{clientId}/{companyId}")]
        public async Task<IActionResult> UploadDocument(IFormFile file,
                                                        int documentClassId,
                                                        int clientId,
                                                        int companyId)
        {
            // StringValues object to store lua string as "batchMetaList" from http
            StringValues httpBatchMetaList = Request.Form["batchMetaList"];
            try
            {
                await _documentUploadService.DocumentUpload(file,
                                                             documentClassId,
                                                             clientId,
                                                             companyId,
                                                             httpBatchMetaList,
                                                             UserName,
                                                             UserId);
            }
            catch (Exception e)
            {

                //log error
                _logger.LogError("{0}: {1}", e.Message, e);

                return BadRequest(new
                {
                    Errors = e,
                    e.Message
                });
            }

            return Ok(MsgKeys.DocumentUploadedSuccessMsg);
        }

        /// <summary>
        /// Edit document
        /// </summary>
        /// <param name="file"></param>
        /// <param name="documentClassId"></param>
        /// <param name="clientId"></param>
        /// <returns></returns>
        [HttpPost("EditUploadDocument/{documentClassId}/{clientId}")]
        public async Task<IActionResult> EditUploadDocument(int documentClassId,
                                                            int clientId,
                                                            [FromBody] EditUploadedDocumentDTO editUploadedDocumentDTO)
        {
            try
            {
                var checkDocumentInCheckout = _repositoryDocumentsCheckedOut.Query(z => z.BatchItemId == editUploadedDocumentDTO.DocumentId)
                                                                              .FirstOrDefault();

                if (checkDocumentInCheckout != null)
                {
                    if (checkDocumentInCheckout.SystemUserId == UserId)
                    {

                        await _documentUploadService.DocumentEdit(documentClassId,
                                                                   clientId,
                                                                   editUploadedDocumentDTO,
                                                                   UserName,
                                                                   UserId);
                    }
                    else
                    {
                        return BadRequest(new
                        {
                            Errors = checkDocumentInCheckout,
                            Message = MsgKeys.DocumentAccessErrorMsg
                        });
                    }
                }
                else
                {
                    return BadRequest(new
                    {
                        Errors = checkDocumentInCheckout,
                        Message = MsgKeys.DocumentNotInCheckOutDocumentsErrorMsg
                    });
                }
            }
            catch (Exception e)
            {

                //log error
                _logger.LogError("{0}: {1}", e.Message, e);

                return BadRequest(new
                {
                    Errors = e,
                    e.Message
                });
            }
            if (editUploadedDocumentDTO.IsRejected)
            {
                return Ok(MsgKeys.DocumentRejectSuccessMsg);
            }
            else
            {
                return Ok(MsgKeys.DocumentEditSuccessMsg);
            }
        }

    }
}