using Intelli.DMS.Api.Services.LocalStorage;
using Intelli.DMS.Domain.Core.Repository;
using Intelli.DMS.Domain.Database;
using Intelli.DMS.Domain.Model;
using Intelli.DMS.Shared.Mvc.Controllers;
using Intelli.DMS.Shared.Mvc.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Intelli.DMS.Api.Helpers;
using Intelli.DMS.EventBus.RabbitMQ.Event;
using System.Collections.Generic;
using Intelli.DMS.Domain.Core.Helpers;
using Intelli.DMS.EventBus.RabbitMQ.Sender;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Net;
using Intelli.DMS.Shared;
using Intelli.DMS.Api.DTO;
using AutoMapper;

namespace Intelli.DMS.Api.Controllers.Document
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OutLookAddInController : BaseController
    {
        private readonly string _repositoryRoot;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IStorageManager _storageManager;
        private readonly IRepository<SystemUser> _repositorySystemUser;
        private readonly IRepository<DMSOutLookAddInTempFile> _repositoryDMSOutLookAddInTempFile;
        private readonly ILogger _logger;

        public OutLookAddInController(IStorageManager storageManager,
                                      DMSContext context,
                                      IMapper mapper,
                                      ILogger<DocumentTypeController> logger,
                                      IConfiguration configuratio,
                                      IEventSender sender)
        {
            _logger = logger;
            _mapper = mapper;
            _storageManager = storageManager;
            _repositorySystemUser = new GenericRepository<SystemUser>(context);
            _repositoryDMSOutLookAddInTempFile = new GenericRepository<DMSOutLookAddInTempFile>(context);
            _configuration = configuratio;
            _repositoryRoot = _configuration.GetSection("OutLookAddInDocumentUploadPath").Value;

            ((GenericRepository<DMSOutLookAddInTempFile>)_repositoryDMSOutLookAddInTempFile).AfterSave = (logs) =>
            {
                sender.SendEvent(new MQEvent<List<AuditEntry>>(AUDIT_EVENT_NAME, (List<AuditEntry>)logs));
            };
        }

        /// <summary>
        /// Gets the DocumentClass w.r.t query string parameters.
        /// </summary>
        /// <param name="queryStringParams">The query string params.</param>
        /// <returns>An IActionResult.</returns>
        [HttpGet]
        public IActionResult Get([FromQuery] QueryStringParams queryStringParams)
        {
            _logger.LogInformation("Get DocumentClass Called with params: {0}", queryStringParams);

            PagedResult<DMSOutLookAddInTempFileDTO> result = null;
            try
            {
                var query = _repositoryDMSOutLookAddInTempFile.Query(x => x.UserId==UserId);

                QueryResult<DMSOutLookAddInTempFile> queryResult = _repositoryDMSOutLookAddInTempFile.GetPaginatedByQuery(query,
                                                                      queryStringParams.FilterExpression,
                                                                      queryStringParams.OrderBy,
                                                                      queryStringParams.PageSize,
                                                                      queryStringParams.PageNumber);

                int total = queryResult.Count;
                IEnumerable<DMSOutLookAddInTempFile> List = queryResult.List;
                List<DMSOutLookAddInTempFileDTO> list = List.Select(x => _mapper.Map<DMSOutLookAddInTempFileDTO>(x)).ToList();
                foreach (var item in list)
                {
                    item.FilePath = RepositoryHelper.BuildRepositoryFilePathforTempFiles(_repositoryRoot,
                                                                                             item.UserId,
                                                                                             item.Id,
                                                                                             item.FileName);
                }
                result = new PagedResult<DMSOutLookAddInTempFileDTO>(
                                total,
                                queryStringParams.PageNumber,
                                list,
                                queryStringParams.PageSize
                            );
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

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("UploadTempDocument")]
        public async Task<IActionResult> UploadTempDocument(IFormFile file)
        {
            try
            {
                string emailAddress = Request.Form["batchMetaList"];
                var user = _repositorySystemUser.Query(x => x.Email == emailAddress || 
                                                            x.OutlookEmail == emailAddress)
                                                 .FirstOrDefault();
                if (user == null)
                {
                    return BadRequest($"{MsgKeys.UserNotExists} {emailAddress}");
                }
                else
                {
                    await using var trans = _repositoryDMSOutLookAddInTempFile.GetTransaction();
                    try
                    {
                        var tempFile = CreateTempFile(user.Id, user.FullName, file.FileName, trans);

                        // Build file path
                        var filePath = RepositoryHelper.BuildRepositoryFilePathforTempFiles(_repositoryRoot,
                                                                                             user.Id,
                                                                                             tempFile.Id,
                                                                                             file.FileName);
                        await _storageManager.StoreFile(file, filePath);
                        await trans.CommitAsync();

                        return Ok(MsgKeys.DocumentUploadedSuccessMsg);
                    }
                    catch (Exception e)
                    {
                        // Rollback transaction
                        await trans.RollbackAsync();

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
        /// Delete the DMSOutLookAddInTempFile.
        /// </summary>
        /// <param name="id">The DMSOutLookAddInTempFile id.</param>
        /// <returns>An IActionResult.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var query = _repositoryDMSOutLookAddInTempFile.Query(x => x.Id == id);

                var entity = await query.FirstOrDefaultAsync();
                if (entity == null)
                    return NotFound();

                if (entity.UserId == UserId)
                {
                    // Build file path
                    var filePath  = RepositoryHelper.BuildRepositoryFilePathforTempFiles(_repositoryRoot,
                                                                                             entity.UserId,
                                                                                             entity.Id,
                                                                                             entity.FileName);
                    //deleting file from file path
                    var pathOnly = Path.GetDirectoryName(filePath);
                    var fulPath = Path.GetFullPath(filePath);
                    if (Directory.Exists(pathOnly))
                        System.IO.File.Delete(fulPath);

                    _repositoryDMSOutLookAddInTempFile.Delete(id, false);
                    _repositoryDMSOutLookAddInTempFile.SaveChanges(UserName, null, null);
                }
                else
                {
                    return NotFound();
                }

                return Ok(MsgKeys.DeletedSuccessfully);
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
        /// Get Temp File By User Id
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        private DMSOutLookAddInTempFile CreateTempFile(int userId,
                                                            string userName,
                                                            string fileName,
                                                            ITransactionHandler trans)
        {


            var dMSOutLookAddInTempFile = new DMSOutLookAddInTempFile
            {
                UserId = userId,
            };
            dMSOutLookAddInTempFile.OriginalFileName = Path.GetFileName(fileName);
            _repositoryDMSOutLookAddInTempFile.Insert(dMSOutLookAddInTempFile);
            _repositoryDMSOutLookAddInTempFile.SaveChanges(userName, null, trans);
            dMSOutLookAddInTempFile.FileName = RepositoryHelper.BuildTempFileName(userId, dMSOutLookAddInTempFile.Id, fileName);
            _repositoryDMSOutLookAddInTempFile.Update(dMSOutLookAddInTempFile);
            _repositoryDMSOutLookAddInTempFile.SaveChanges(userName, null, trans);

            return dMSOutLookAddInTempFile;
        }

    }
}