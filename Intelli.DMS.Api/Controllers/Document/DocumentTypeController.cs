using AutoMapper;
using Intelli.DMS.Api.DTO;
using Intelli.DMS.Domain.Core.Helpers;
using Intelli.DMS.Domain.Core.Repository;
using Intelli.DMS.Domain.Database;
using Intelli.DMS.Domain.Model;
using Intelli.DMS.Domain.Repository;
using Intelli.DMS.Domain.Repository.Impl;
using Intelli.DMS.EventBus.RabbitMQ.Event;
using Intelli.DMS.EventBus.RabbitMQ.Sender;
using Intelli.DMS.Shared;
using Intelli.DMS.Shared.Mvc.Controllers;
using Intelli.DMS.Shared.Mvc.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intelli.DMS.Api.Controllers.Document
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DocumentTypeController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ICompanyRepository<DocumentType> _companySpecificRepository;
        private readonly IRepository<DocumentTypeRoles> _repositoryDocumentTypeRoles;
        private readonly IRepository<DocumentTypeRoleAccess> _repositoryDocumentTypeRoleAccess;
        private readonly IRepository<SystemUserRole> _repositorySystemUserRole;
        private readonly IRepository<DocumentClass> _repositoryDocumentClass;
        private readonly IRepository<BatchItem> _repositoryBatchItem;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentTypeController"/> class.
        /// </summary>
        /// <param name="context">Instance of <see cref="DMSContext"/> will be injected</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/> will be injected</param>
        /// <param name="logger">Instance of <see cref="ILogger"/> will be injected</param>
        /// <param name="sender">Instance of <see cref="IEventSender"/> will be injected</param>
        public DocumentTypeController(DMSContext context,
            IMapper mapper,
            ILogger<DocumentTypeController> logger,
            IEventSender sender)
        {
            _companySpecificRepository = new CompanyEntityRepository<DocumentType>(context);
            _repositoryDocumentTypeRoles = new GenericRepository<DocumentTypeRoles>(context);
            _repositoryDocumentClass = new GenericRepository<DocumentClass>(context);
            _repositoryBatchItem = new GenericRepository<BatchItem>(context);
            _repositoryDocumentTypeRoleAccess = new GenericRepository<DocumentTypeRoleAccess>(context);
            _repositorySystemUserRole = new GenericRepository<SystemUserRole>(context);

            ((GenericRepository<SystemUserRole>)_repositorySystemUserRole).AfterSave =
            ((GenericRepository<DocumentTypeRoleAccess>)_repositoryDocumentTypeRoleAccess).AfterSave =
            ((GenericRepository<BatchItem>)_repositoryBatchItem).AfterSave =
            ((GenericRepository<DocumentClass>)_repositoryDocumentClass).AfterSave =
            ((GenericRepository<DocumentTypeRoles>)_repositoryDocumentTypeRoles).AfterSave =
            ((CompanyEntityRepository<DocumentType>)_companySpecificRepository).AfterSave = (logs) =>
            {
                sender.SendEvent(new MQEvent<List<AuditEntry>>(AUDIT_EVENT_NAME, (List<AuditEntry>)logs));
            };

            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Get All DocumentTypes.
        /// </summary>
        /// <returns>List of DocumentTypeDTO</returns>
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll()
        {

            List<int> documentTypeIds = GetDocumentTypeIdsByRoleAccess();

            var dto = new DocumentTypeDTO();
            var result = await _companySpecificRepository.GetAllActiveAsync(CompanyIds, nameof(dto.DocumentTypeName));
            if (CompanyIds.Count > 0)
            {
                IEnumerable<DocumentType> list = result.List.Where(x => documentTypeIds.Contains(x.Id)).ToList();

                return Ok(new { Items = list.Select(x => _mapper.Map<DocumentTypeDTO>(x)).ToList() });
            }
            else
            {
                return Ok(new { Items = result.List.Select(x => _mapper.Map<DocumentTypeDTO>(x)).ToList() });
            }
        }

        /// <summary>
        /// Gets the DocumentTypes w.r.t query string parameters.
        /// </summary>
        /// <param name="queryStringParams">The query string params.</param>
        /// <returns>An IActionResult.</returns>
        [HttpGet]
        public IActionResult Get([FromQuery] QueryStringParams queryStringParams)
        {
            _logger.LogInformation("Get DocumentType Called with params: {0}", queryStringParams);

            PagedResult<DocumentTypeDTO> result = null;
            try
            {
                List<int> documentTypeIds = GetDocumentTypeIdsByRoleAccess();
                QueryResult<DocumentType> queryResult;

                if (CompanyIds.Count > 0)
                {
                    queryResult = _companySpecificRepository.GetByIds(
                                                        CompanyIds,
                                                        documentTypeIds,
                                                        queryStringParams.FilterExpression,
                                                        queryStringParams.OrderBy,
                                                        queryStringParams.PageSize,
                                                        queryStringParams.PageNumber);
                }
                else
                {
                    queryResult = _companySpecificRepository.Get(
                                                       CompanyIds,
                                                       queryStringParams.FilterExpression,
                                                       queryStringParams.OrderBy,
                                                       queryStringParams.PageSize,
                                                       queryStringParams.PageNumber);
                }

                var ListofDocumentTypes = queryResult.List.Select(x => _mapper.Map<DocumentTypeDTO>(x))
                                     .ToList();
                int total;
                if (CompanyIds.Count > 0)
                {
                    total = documentTypeIds.Count;
                }
                else
                {
                    total = _companySpecificRepository.GetAllActiveAsync(CompanyIds, "DocumentTypeName").Result.Count;
                }

                result = new PagedResult<DocumentTypeDTO>(
                                total,
                                queryStringParams.PageNumber,
                                ListofDocumentTypes,
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

        /// <summary>
        ///  Get DocumentTypeIds By Role Access
        /// </summary>
        /// <returns></returns>
        private List<int> GetDocumentTypeIdsByRoleAccess()
        {
            var userRolelist = _repositorySystemUserRole.Query(x => x.SystemUserId == UserId)
                                                        .Select(x => x.SystemRoleId).ToList();

            var documentTypeIds = _repositoryDocumentTypeRoleAccess.Query(x => userRolelist.Contains(x.SystemRoleId) && x.DocumentType.IsActive == true)
                                                                    .Select(x => x.DocumentTypeId).ToList();
            return documentTypeIds;
        }

        /// <summary>
        /// Gets the DocumentType.
        /// </summary>
        /// <param name="id">The DocumentType id.</param>
        /// <returns>The DocumentTypeDTO.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var entity = await _companySpecificRepository.GetById(id);
                var documentTypeDTO = _mapper.Map<DocumentTypeDTO>(entity);
                var roleListWithDocumentType = _repositoryDocumentTypeRoles.Query(x => x.DocumentTypeId == id)
                                                                            .Include(x => x.SystemRole)
                                                                             .Select(x => x.SystemRole).ToList();
                var roleListWithDocumentTypeRoleAccess = _repositoryDocumentTypeRoleAccess.Query(x => x.DocumentTypeId == id)
                                                                            .Include(x => x.SystemRole)
                                                                                    .Select(x => x.SystemRole).ToList();

                documentTypeDTO.RoleLists = _mapper.Map<List<DocumrntTypeRoleDTO>>(roleListWithDocumentType);

                documentTypeDTO.RoleAccessLists = _mapper.Map<List<DocumrntTypeRoleDTO>>(roleListWithDocumentTypeRoleAccess);

                CheckDocumentTypeRolesModifiable(id, documentTypeDTO);
                return Ok(documentTypeDTO);

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
        /// Check Roles Modifiable Or Not and Set IsRolesModifiable flag
        /// </summary>
        /// <param name="id"></param>
        /// <param name="documentTypeDTO"></param>
        private void CheckDocumentTypeRolesModifiable(int id, DocumentTypeDTO documentTypeDTO)
        {
            // Get All Document Class with Specific Document Type
            var documentClass = _repositoryDocumentClass.Query(x => x.DocumentTypeId == id).ToList();
            if (documentClass.Count != 0)
            {
                foreach (var item in documentClass)
                {
                    var checkBatcItem = _repositoryBatchItem.Query(x => x.DocumentClassId == item.Id)
                                                                .FirstOrDefault();
                    if (checkBatcItem != null)
                    {
                        documentTypeDTO.IsRolesModifiable = false;
                        break;
                    }
                    else
                    {
                        documentTypeDTO.IsRolesModifiable = true;
                    }
                }
            }
            else
            {
                documentTypeDTO.IsRolesModifiable = true;
            }
        }

        /// <summary>
        /// Creates the DocumentType.
        /// </summary>
        /// <param name="dto">The DocumentType dto.</param>
        /// <returns>An IActionResult.</returns>
        [HttpPost]
        public async Task<IActionResult> PostAsync(DocumentTypeDTO dto)
        {


            // Checking if the passed DTO is valid
            if (!ModelState.IsValid || dto == null)
                return BadRequest(MsgKeys.InvalidInputParameters);

            if (dto.RoleAccessLists == null)
            {
                return BadRequest(MsgKeys.RolesAccessMustBeSelected);
            }
            await using var trans = _companySpecificRepository.GetTransaction();

            List<int> companyIds = new();
            companyIds.Add(dto.CompanyId);
            dto.DocumentTypeName = dto.DocumentTypeName.Trim(' ','\t');
            //duplicate field validations
            if (_companySpecificRepository.Query(companyIds, orderBy: s => s.OrderBy(s => s.DocumentTypeName))
                                                  .Any(d => d.DocumentTypeName == dto.DocumentTypeName))
                return BadRequest(MsgKeys.RecordWithSameNameExists);
            dto.DocumentTypeCode = dto.DocumentTypeCode.Trim(' ','\t');
            if (_companySpecificRepository.Query(companyIds, orderBy: s => s.OrderBy(s => s.DocumentTypeCode))
                                                     .Any(d => d.DocumentTypeCode == dto.DocumentTypeCode))
                return BadRequest(MsgKeys.RecordWithSameDocumentTypeCodeExists);

            var entity = _mapper.Map<DocumentType>(dto);

            try
            {
                _companySpecificRepository.Insert(CompanyIds, entity, entity.IsActive ?? true);
                _companySpecificRepository.SaveChanges(UserName, null, trans);

                if (dto.RoleLists != null)
                {
                    foreach (var item in dto.RoleLists)
                    {
                        DocumentTypeRoles documentTypeRoles = new();
                        documentTypeRoles.DocumentTypeId = entity.Id;
                        documentTypeRoles.SystemRoleId = item.Id;
                        _repositoryDocumentTypeRoles.Insert(documentTypeRoles);
                    }
                    _repositoryDocumentTypeRoles.SaveChanges(UserName, null, trans);
                }

                foreach (var item in dto.RoleAccessLists)
                {
                    DocumentTypeRoleAccess documentTypeRoleAccess = new();
                    documentTypeRoleAccess.DocumentTypeId = entity.Id;
                    documentTypeRoleAccess.SystemRoleId = item.Id;
                    _repositoryDocumentTypeRoleAccess.Insert(documentTypeRoleAccess);
                }
                _repositoryDocumentTypeRoleAccess.SaveChanges(UserName, null, trans);

                await trans.CommitAsync();
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();

                return BadRequest($"{ex.Message} {ex?.InnerException?.Message}");

            }
            return Ok(_mapper.Map<DocumentTypeDTO>(entity));
        }

        /// <summary>
        /// Updates the DocumentType.
        /// </summary>
        /// <param name="id">The DocumentType id.</param>
        /// <param name="dto">The DocumentTypeDTO.</param>
        /// <returns>An IActionResult.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, DocumentTypeDTO dto)
        {
            // Checking if the passed DTO is valid
            if (!ModelState.IsValid || dto == null || id != dto.Id)
                return BadRequest(MsgKeys.InvalidInputParameters);

            if (dto.RoleAccessLists == null)
            {
                return BadRequest(MsgKeys.RolesAccessMustBeSelected);
            }
            await using var trans = _companySpecificRepository.GetTransaction();

            List<int> companyIds = new();
            companyIds.Add(dto.CompanyId);
            //duplicate field validations
            if (dto.IsDocumentTypeChanged)
            {
                dto.DocumentTypeName = dto.DocumentTypeName.Trim(' ','\t');
                if (_companySpecificRepository.Query(companyIds, orderBy: s => s.OrderBy(s => s.DocumentTypeName))
                                                     .Any(d => d.DocumentTypeName == dto.DocumentTypeName))
                    return BadRequest(MsgKeys.RecordWithSameNameExists);
            }
            if (dto.IsDocumentCodeChanged)
            {
                dto.DocumentTypeCode = dto.DocumentTypeCode.Trim(' ','\t');

                if (_companySpecificRepository.Query(companyIds, orderBy: s => s.OrderBy(s => s.DocumentTypeCode))
                                                     .Any(d => d.DocumentTypeCode == dto.DocumentTypeCode))
                    return BadRequest(MsgKeys.RecordWithSameDocumentTypeCodeExists);
            }
            var entityDocumentTypeDTO = _mapper.Map<DocumentType>(dto);

            try
            {

                _companySpecificRepository.Update(CompanyIds, entityDocumentTypeDTO);
                _companySpecificRepository.SaveChanges(UserName, null, trans);

                _repositoryDocumentTypeRoles.Delete(x => x.DocumentTypeId == entityDocumentTypeDTO.Id);
                _repositoryDocumentTypeRoles.SaveChanges(UserName, null, trans);

                _repositoryDocumentTypeRoleAccess.Delete(x => x.DocumentTypeId == entityDocumentTypeDTO.Id);
                _repositoryDocumentTypeRoleAccess.SaveChanges(UserName, null, trans);


                CheckDocumentTypeRolesModifiable(id, dto);

                if (dto.RoleLists != null && dto.IsRolesModifiable)
                {
                    foreach (var item in dto.RoleLists)
                    {
                        DocumentTypeRoles documentTypeRoles = new();
                        documentTypeRoles.DocumentTypeId = entityDocumentTypeDTO.Id;
                        documentTypeRoles.SystemRoleId = item.Id;
                        _repositoryDocumentTypeRoles.Insert(documentTypeRoles);
                    }
                    _repositoryDocumentTypeRoles.SaveChanges(UserName, null, trans);
                }
                foreach (var item in dto.RoleAccessLists)
                {
                    DocumentTypeRoleAccess documentTypeRoleAccess = new();
                    documentTypeRoleAccess.DocumentTypeId = entityDocumentTypeDTO.Id;
                    documentTypeRoleAccess.SystemRoleId = item.Id;
                    _repositoryDocumentTypeRoleAccess.Insert(documentTypeRoleAccess);
                }
                _repositoryDocumentTypeRoleAccess.SaveChanges(UserName, null, trans);

                await trans.CommitAsync();
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();

                return BadRequest($"{ex.Message} {ex?.InnerException?.Message}");

            }
            return Ok(_mapper.Map<DocumentTypeDTO>(entityDocumentTypeDTO));
        }

        /// <summary>
        /// Delete the DocumentType.
        /// </summary>
        /// <param name="id">The DocumentType id.</param>
        /// <returns>An IActionResult.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var query = _companySpecificRepository.Query(CompanyIds, x => x.Id == id);

                var entity = await query.FirstOrDefaultAsync();
                if (entity == null)
                    return NotFound();

                entity.IsActive = false;
                _companySpecificRepository.SaveChanges(UserName, null, null);

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
        /// Gets the DocumentType By Company Id.
        /// </summary>
        /// <param name="companyId">Company Id</param>
        /// <returns>List Of Document Classes</returns>
        [HttpGet]
        [Route("documentTypesByCompanyId/{companyId}")]
        public async Task<IActionResult> GetDocumentTypesByCompanyId(int companyId)
        {
            try
            {
                List<int> documentTypeIds = GetDocumentTypeIdsByRoleAccess();
                var dto = new DocumentTypeDTO();
                List<int> companyIds = new() { companyId };
                var result = await _companySpecificRepository.GetAllActiveAsync(companyIds, nameof(dto.DocumentTypeName));
                if (CompanyIds.Count > 0)
                {
                    IEnumerable<DocumentType> List = result.List.Where(x => documentTypeIds.Contains(x.Id)).ToList();
                    return Ok(new { Items = List.Select(x => _mapper.Map<DocumentTypeDTO>(x)).ToList() });
                }
                else
                {
                    return Ok(new { Items = result.List.Select(x => _mapper.Map<DocumentTypeDTO>(x)).ToList() });
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
    }
}