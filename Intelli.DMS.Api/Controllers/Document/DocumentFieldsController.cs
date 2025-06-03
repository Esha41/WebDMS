using Intelli.DMS.Api.DTO;
using Intelli.DMS.Api.Services.DocumentFields;
using Intelli.DMS.Shared.Mvc.Controllers;
using Intelli.DMS.Shared.Mvc.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intelli.DMS.Api.Controllers.Document
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DocumentFieldsController : BaseController
    {

        private readonly IDocumentFieldService _documentFieldService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentFieldsController"/> class.
        /// </summary>
        /// <param name="logger">Instance of <see cref="ILogger"/> will be injected</param>
        /// <param name="documentFieldService">Instance of <see cref="IDocumentFieldService"/> will be injected</param>
        public DocumentFieldsController(IDocumentFieldService documentFieldService)
        {
            _documentFieldService = documentFieldService;
        }

        /// <summary>
        /// Get  GetPrevFieldValues of a document
        /// </summary>
        /// <returns>List of repositories against client id</returns>
        /// <param name="batchId">getting the field values relevant to batchId.</param>
        [HttpGet("Values/{batchId}")]
        public IActionResult GetPrevFieldValues(int batchId)
        {
            try
            {
                //calling service to get prev field values
                var result = _documentFieldService.GetPrevFieldValues(batchId);

                return Ok(new
                {
                    Items = result.ToList()
                });
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    Errors = e,
                    e.Message
                });
            }
        }

        /// <summary>
        /// performing changes to document i.e changes in its document class field values
        /// </summary>
        /// <param name="List<BatchMetaCheckOutDTO>"></param>
        /// 
        [HttpPut("UpdateValues")]
        public async Task<IActionResult> UpdateFieldValues(List<BatchMetaCheckOutDTO> dtoList)
        {
            // Checking if the passed DTO is valid
            if (!ModelState.IsValid || dtoList.Count == 0)
                return BadRequest(MsgKeys.InvalidInputParameters);

            try
            {
                //calling service to update field values of a document
                var result = await _documentFieldService.UpdateFieldValues(dtoList);

                return Ok(new
                {
                    Items = result.ToList()
                });
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    Errors = e,
                    e.Message
                });
            }
        }
    }
}
