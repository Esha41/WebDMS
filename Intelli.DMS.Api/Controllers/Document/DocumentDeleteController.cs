using AutoMapper;
using Intelli.DMS.Api.DTO;
using Intelli.DMS.Api.Services.DocumentDelete;
using Intelli.DMS.Api.Services.DocumentUpload;
using Intelli.DMS.Domain.Core.Repository;
using Intelli.DMS.Domain.Database;
using Intelli.DMS.Domain.Model;
using Intelli.DMS.Shared.Mvc.Controllers;
using Intelli.DMS.Shared.Mvc.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Intelli.DMS.Api.Controllers.Document
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DocumentDeleteController : BaseController
    {
        private readonly IDocumentDeleteService _documentDeleteService;
        private readonly ILogger _logger;


        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentUploadController"/> class.
        /// </summary>
        /// <param name="context">Instance of <see cref="DMSContext"/> will be injected</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/> will be injected</param>
        /// <param name="logger">Instance of <see cref="ILogger"/> will be injected</param>
        public DocumentDeleteController(DMSContext context,
                                        ILogger<Batch> logger,
                                        IDocumentDeleteService documentDeleteService)
        {
            _documentDeleteService = documentDeleteService;
            _logger = logger;
        }

        /// <summary>
        /// Delete document
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteDocument([FromQuery] int documentId)
        {

            try
            {
                await _documentDeleteService.DeleteDocumentWithAssociatedData(documentId);
            }
            catch (DirectoryNotFoundException e)
            {
                _logger.LogError("{0}: {1}", e.Message, e);

                return BadRequest(new
                {
                    Errors = e,
                    Message = MsgKeys.DirectortoryFileNotExist
                });
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

            return Ok(MsgKeys.DocumentDeletedSuccessfully);
        }

    }
}