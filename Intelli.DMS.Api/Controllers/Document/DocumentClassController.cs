using AutoMapper;
using Intelli.DMS.Api.Constants;
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
    public class DocumentClassController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly ICompanyRepository<DocumentClass> _repositoryCompanySpecificDocumentClass;
        private readonly IRepository<DocumentClassField> _repositoryDocumentClassField;
        private readonly IRepository<BatchItemField> _repositoryBatchItemField;
        private readonly IRepository<DocumentTypeRoleAccess> _repositoryDocumentTypeRoleAccess;
        private readonly IRepository<SystemUserRole> _repositorySystemUserRole;
        private readonly IRepository<DocumentClass> _repositoryDocumentClass;



        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentClassFieldsController"/> class.
        /// </summary>
        /// <param name="context">Instance of <see cref="DMSContext"/> will be injected</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/> will be injected</param>
        /// <param name="logger">Instance of <see cref="ILogger"/> will be injected</param>
        /// <param name="sender">Instance of <see cref="IEventSender"/> will be injected</param>
        public DocumentClassController(DMSContext context,
            IMapper mapper,
            ILogger<DocumentClassController> logger,
            IEventSender sender)
        {
            _repositoryCompanySpecificDocumentClass = new CompanyEntityRepository<DocumentClass>(context);
            _repositoryDocumentClass = new GenericRepository<DocumentClass>(context);
            _repositoryDocumentClassField = new GenericRepository<DocumentClassField>(context);
            _repositoryBatchItemField = new GenericRepository<BatchItemField>(context);
            _repositoryDocumentTypeRoleAccess = new GenericRepository<DocumentTypeRoleAccess>(context);
            _repositorySystemUserRole = new GenericRepository<SystemUserRole>(context);

            ((GenericRepository<DocumentClass>)_repositoryDocumentClass).AfterSave =
            ((GenericRepository<SystemUserRole>)_repositorySystemUserRole).AfterSave =
            ((GenericRepository<DocumentTypeRoleAccess>)_repositoryDocumentTypeRoleAccess).AfterSave =
            ((GenericRepository<BatchItemField>)_repositoryBatchItemField).AfterSave =
            ((CompanyEntityRepository<DocumentClass>)_repositoryCompanySpecificDocumentClass).AfterSave =
            ((GenericRepository<DocumentClassField>)_repositoryDocumentClassField).AfterSave = (logs) =>
            {
                sender.SendEvent(new MQEvent<List<AuditEntry>>(AUDIT_EVENT_NAME, (List<AuditEntry>)logs));
            };

            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Get All Companies.
        /// </summary>
        /// <returns>List of DocumentClassController</returns>
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll()
        {
            var dto = new DocumentClassDTO();
            var result = await _repositoryCompanySpecificDocumentClass.GetAllActiveAsync(CompanyIds, nameof(dto.DocumentClassName), x => x.DocumentType);
            return Ok(new { Items = result.List.Select(x => _mapper.Map<DocumentClassDTO>(x)).ToList() });
        }

        /// <summary>
        /// Get All Companies.
        /// </summary>
        /// <returns>List of DocumentClassController</returns>
        [HttpGet]
        [Route("allDocumentClasses")]
        public async Task<IActionResult> GetAllDocumentClasses()
        {
            var dto = new DocumentClassDTO();
            var result = await _repositoryDocumentClass.GetAllActiveAsync(nameof(dto.DocumentClassName), x => x.DocumentClassFields);

            return Ok(new
            {
                documentClasses = result.List.
                Select(x =>
                {
                    var model = _mapper.Map<DocumentClassContractsDTO>(x);
                    model.DocumentClassFields= _mapper.Map<List<DocumentClassFieldContractDTO>>(model.DocumentClassFields);
                    return model;
                }).ToList()
            },1);
        }

        /// <summary>
        /// Get All DocumentClass.
        /// </summary>
        /// <param name="documentClassName">DocumentClasse.</param>
        /// <param name="companyId">DocumentClasse.</param>
        /// <returns>List of DocumentClassDTO</returns>
        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> GetAll(string documentClassName, int companyId)
        {
            var query = _repositoryCompanySpecificDocumentClass.Query(CompanyIds,
                                                      x =>
                                                      (
                                                        (x.DocumentClassName.Contains(documentClassName ?? String.Empty))
                                                        && (x.CompanyId == companyId)
                                                        && (x.IsActive == true)
                                                      )).Take(10);
            var result = await query.ToListAsync();

            return Ok(new { Items = result.Select(x => _mapper.Map<DocumentClassDTO>(x)).ToList() });
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

            PagedResult<DocumentClassDTO> result = null;
            try
            {
                QueryResult<DocumentClass> queryResult = _repositoryCompanySpecificDocumentClass.Get(
                                                    CompanyIds,
                                                    queryStringParams.FilterExpression,
                                                    queryStringParams.OrderBy,
                                                    queryStringParams.PageSize,
                                                    queryStringParams.PageNumber,
                                                    x => x.DocumentType);

                int total = queryResult.Count;
                IEnumerable<DocumentClass> List = queryResult.List;
                var listDocumentClassDTO = List.Select(x => _mapper.Map<DocumentClassDTO>(x)).ToList();
                foreach (var item in listDocumentClassDTO)
                {
                    item.DocumentTypeName = item?.DocumentType?.DocumentTypeName;
                }
                result = new PagedResult<DocumentClassDTO>(
                                total,
                                queryStringParams.PageNumber,
                                listDocumentClassDTO,
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
        /// Gets the DocumentClass.
        /// </summary>
        /// <param name="id">The DocumentClass id.</param>
        /// <returns>The DocumentClassDTO.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var query = _repositoryCompanySpecificDocumentClass.Query(CompanyIds, x => x.Id == id)
                                                     .Include(x => x.DocumentClassFields)
                                                      .ThenInclude(x => x.DocumentClassFieldType)
                                                     .Include(x => x.DocumentClassFields)
                                                      .ThenInclude(x => x.DictionaryType)
                                                     .Include(x => x.DocumentType);

                var entity = await query.FirstOrDefaultAsync();
                var dto = _mapper.Map<DocumentClassDTO>(entity);
                if (entity != null)
                {
                    dto.IsDocumentTypeModifiedable = true;
                    dto.DocumentTypeName = entity?.DocumentType?.DocumentTypeName;
                    dto.Fields = entity?.DocumentClassFields
                                        .Where(x => x.IsActive == true)
                                        .Select(x => _mapper.Map<DocumentClassFieldDTO>(x))
                                        .ToList();
                    foreach (var item in dto.Fields)
                    {
                        var chkRef = _repositoryBatchItemField.Query(a => a.DocumentClassFieldId == item.Id)
                                                                .FirstOrDefault();
                        if (chkRef != null)
                        {
                            item.IsModifiedable = true;
                            item.IsDeleteable = false;
                            dto.IsDocumentTypeModifiedable = false;
                        }
                        else
                        {
                            item.IsModifiedable = true;
                            item.IsDeleteable = true;
                        }
                    }
                    if (dto.IsDocumentTypeModifiedable == true)
                    {
                        var checkRoleAccessListOfDocumentType = GetDocumentTypeIdsByRoleAccess();
                        if (!checkRoleAccessListOfDocumentType.Contains(dto.DocumentTypeId))
                        {
                            dto.IsDocumentTypeModifiedable = false;
                        }
                    }
                }

                return Ok(dto);
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
        /// Creates the DocumentClass.
        /// </summary>
        /// <param name="dto">The DocumentClass dto.</param>
        /// <returns>An IActionResult.</returns>
        [HttpPost]
        public async Task<IActionResult> Post(DocumentClassDTO dto)
        {
            await using var trans = _repositoryCompanySpecificDocumentClass.GetTransaction();

            try
            {
                // log information
                _logger.LogInformation("Creating document class: {0}", dto);

                // Checking if the passed DTO is valid
                if (!ModelState.IsValid || dto == null)
                    return BadRequest(MsgKeys.InvalidInputParameters);



                List<int> companyIds = new();
                companyIds.Add(dto.CompanyId);

                dto.DocumentClassName = dto.DocumentClassName.Trim(' ', '\t');
                //duplicate field validations
                if (_repositoryCompanySpecificDocumentClass.Query(companyIds, orderBy: s => s.OrderBy(s => s.DocumentClassName))
                                                            .Any(d => d.CompanyId == dto.CompanyId &&
                                                                      d.DocumentClassName == dto.DocumentClassName))
                    return BadRequest(MsgKeys.RecordWithSameNameExists);

                dto.DocumentClassName = dto.DocumentClassName.Trim(' ', '\t');
                //duplicate EnumVale validations
                if (_repositoryCompanySpecificDocumentClass.Query(companyIds, orderBy: s => s.OrderBy(s => s.DocumentClassName))
                                                            .Any(d => d.CompanyId == dto.CompanyId &&
                                                                      d.DocumentClassCode == dto.DocumentClassCode))
                    return BadRequest(MsgKeys.RecordWithSameDocumentClassCodeExists);


                List<string> listDocumentClassfieldCodes = GetDocumentClassFieldCodeByCompanyId(dto.CompanyId)
                                                                              .Select(x => x.DocumentClassFieldCode)
                                                                              .ToList();
                if (listDocumentClassfieldCodes.Count > 0)
                {
                    var dtoDocumentClassFieldCodestobeDelete = dto.FieldCodes.Where(y => y.recordStatus == RecordStatusConstant.DeleteFromDb)?
                                                                    .Select(x => x.DocumentClassFieldCode)
                                                                    .ToList();
                    if (dtoDocumentClassFieldCodestobeDelete.Count > 0)
                    {
                        foreach (var item in dtoDocumentClassFieldCodestobeDelete)
                        {
                            listDocumentClassfieldCodes.Remove(item);
                        }
                    }
                    var dtoDocumentClassFieldCodes = dto.FieldCodes.Where(y => y.recordStatus == RecordStatusConstant.AddToDB)?
                                                                    .Select(x => x.DocumentClassFieldCode)
                                                                    .ToList();
                    if (dtoDocumentClassFieldCodes.Count > 0)
                    {
                        if (dtoDocumentClassFieldCodes.Intersect(listDocumentClassfieldCodes).Any())
                        {
                            return BadRequest(MsgKeys.RecordWithSameDocumentClassFieldCodesExists);
                        }
                    }
                }
                DocumentClassDTO dtoResult = new();

                var entityClass = _mapper.Map<DocumentClass>(dto);
                _repositoryCompanySpecificDocumentClass.Insert(CompanyIds, entityClass);
                _repositoryCompanySpecificDocumentClass.SaveChanges(UserName, null, null);

                //storing list of fields for document class
                foreach (var field in dto.Fields)
                {
                    field.DocumentClassId = entityClass.Id;
                    var entityField = _mapper.Map<DocumentClassField>(field);
                    _repositoryDocumentClassField.Insert(entityField);
                    _repositoryDocumentClassField.SaveChanges(UserName, null, null);
                    field.Id = entityField.Id;
                }
                dtoResult = _mapper.Map<DocumentClassDTO>(entityClass);
                dtoResult.Fields = dto.Fields;

                await trans.CommitAsync();

                return Ok(dtoResult);
            }
            catch (Exception e)
            {
                // Rollback transaction
                await trans.RollbackAsync();
                _logger.LogError("{0}: {1}", e.Message, e);

                return BadRequest(new
                {
                    Errors = e,
                    e.Message
                });
            }

        }

        // <summary>
        /// Updates the DocumentClass.
        /// </summary>
        /// <param name="id">The DocumentClass id.</param>
        /// <param name="dto">The DocumentClassdDTO.</param>
        /// <returns>An IActionResult.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, DocumentClassDTO dto)
        {
            await using var trans = _repositoryCompanySpecificDocumentClass.GetTransaction();

            try
            {
                // log information
                _logger.LogInformation("Updating document class: Id:{0} | {1}", id, dto);

                // Checking if the passed DTO is valid
                if (!ModelState.IsValid || dto == null || id != dto.Id)
                    return BadRequest(MsgKeys.InvalidInputParameters);
                List<int> companyIds = new();
                companyIds.Add(dto.CompanyId);
                if (dto.IsDocumentClassNameDuplicationCheck)
                {
                    dto.DocumentClassName = dto.DocumentClassName.Trim(' ', '\t');
                    //duplicate field validations
                    if (_repositoryCompanySpecificDocumentClass.Query(companyIds, orderBy: s => s.OrderBy(s => s.DocumentClassName))
                                                                .Any(d => d.CompanyId == dto.CompanyId &&
                                                                          d.DocumentClassName == dto.DocumentClassName))
                        return BadRequest(MsgKeys.RecordWithSameNameExists);
                }
                //duplicate EnumVale validations
                if (dto.IsDocumentClassCodeDuplicationCheck)
                {
                    dto.DocumentClassCode = dto.DocumentClassCode.Trim(' ', '\t');
                    if (_repositoryCompanySpecificDocumentClass.Query(companyIds, orderBy: s => s.OrderBy(s => s.DocumentClassName))
                                                                .Any(d => d.CompanyId == dto.CompanyId &&
                                                                          d.DocumentClassCode == dto.DocumentClassCode))
                        return BadRequest(MsgKeys.RecordWithSameDocumentClassCodeExists);
                }
                List<string> listDocumentClassfieldCodes = GetDocumentClassFieldCodeByCompanyId(dto.CompanyId)
                                                                                .Select(x => x.DocumentClassFieldCode)
                                                                                .ToList();
                if (listDocumentClassfieldCodes.Count > 0)
                {
                    var dtoDocumentClassFieldCodestobeDelete = dto.FieldCodes.Where(y => y.recordStatus == RecordStatusConstant.DeleteFromDb)?
                                                                    .Select(x => x.DocumentClassFieldCode)
                                                                    .ToList();
                    if (dtoDocumentClassFieldCodestobeDelete.Count > 0)
                    {
                        foreach (var item in dtoDocumentClassFieldCodestobeDelete)
                        {
                            listDocumentClassfieldCodes.Remove(item);
                        }
                    }
                    var dtoDocumentClassFieldCodes = dto.FieldCodes.Where(y => y.recordStatus == RecordStatusConstant.AddToDB)?
                                                                    .Select(x => x.DocumentClassFieldCode)
                                                                    .ToList();
                    if (dtoDocumentClassFieldCodes.Count > 0)
                    {
                        if (dtoDocumentClassFieldCodes.Intersect(listDocumentClassfieldCodes).Any())
                        {
                            return BadRequest(MsgKeys.RecordWithSameDocumentClassFieldCodesExists);
                        }
                    }
                }
                DocumentClass entity = new();
                DocumentClassDTO dtoResult = new();

                entity = _mapper.Map<DocumentClass>(dto);
                dtoResult = _mapper.Map<DocumentClassDTO>(entity);

                var chkRefDClasField = _repositoryBatchItemField.Query(a => a.DocumentClassFieldId == dto.Fields.FirstOrDefault().Id)
                                                                  .FirstOrDefault();
                if (chkRefDClasField != null)
                {
                    entity.DocumentTypeId = dto.previousDocumentTypeId;
                    entity.CompanyId = dto.previousCompanyId;
                }

                _repositoryCompanySpecificDocumentClass.Update(CompanyIds, entity);
                _repositoryCompanySpecificDocumentClass.SaveChanges(UserName, null, null);

                //updating class fields
                if (dto.Fields.Count > 0)
                {
                    foreach (var field in dto.Fields.ToList())
                    {
                        var entityField = _mapper.Map<DocumentClassField>(field);
                        if (field.recordStatus == RecordStatusConstant.InDB)
                        {
                            continue;
                        }
                        else if (field.recordStatus == RecordStatusConstant.AddToDB)
                        {
                            _repositoryDocumentClassField.Insert(entityField);
                        }
                        else if (field.recordStatus == RecordStatusConstant.UpdateFromDB)
                        {
                            _repositoryDocumentClassField.Update(entityField);
                        }
                        else if (field.recordStatus == RecordStatusConstant.DeleteFromDb)
                        {
                            _repositoryDocumentClassField.Delete(field.Id, false);
                        }

                    }
                    _repositoryDocumentClassField.SaveChanges(UserName, null, null);
                }
                dtoResult.Fields = dto.Fields;


                await trans.CommitAsync();
                return Ok(dtoResult);

            }
            catch (Exception e)
            {
                // Rollback transaction
                await trans.RollbackAsync();
                _logger.LogError("{0}: {1}", e.Message, e);

                return BadRequest(new
                {
                    Errors = e,
                    e.Message
                });
            }
        }

        /// <summary>
        /// Delete the DocumentClass.
        /// </summary>
        /// <param name="id">The DocumentClass id.</param>
        /// <returns>An IActionResult.</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            //var getClassFields = _repositoryDocumentClassField.Query(filter: s => s.DocumentClassId == id);
            //if (getClassFields.Count() > 0)
            //{
            //    return BadRequest(MsgKeys.ChildEntityExists);
            //}

            _repositoryCompanySpecificDocumentClass.Delete(CompanyIds, id);
            _repositoryCompanySpecificDocumentClass.SaveChanges(UserName, null, null);

            return Ok(MsgKeys.DeletedSuccessfully);
        }

        /// <summary>
        /// Get All GetAllDocumentClassFields.
        /// </summary>
        /// <returns>List of GetAllDocumentClassFields associated with document class</returns>
        [HttpGet("fields/{documentClassId}")]
        public IActionResult GetAllDocumentClassFields(int documentClassId)
        {
            try
            {
                var result = _repositoryDocumentClassField.Query(filter: s => s.DocumentClassId == documentClassId &&
                                                                              s.IsActive == true)
                                                            .Include(x => x.DictionaryType)
                                                            .ThenInclude(x => x.BopDictionaries)
                                                            .OrderBy(x => x.UISort);
                var itemsDocumentClassFields = result.Select(x => _mapper.Map<DocumentClassAllFieldsDTO>(x)).ToList();
                var getItemOfUIsortNull = itemsDocumentClassFields.Where(x => x.UISort == null).ToList();
                var getItemOfUIsortNotNull = itemsDocumentClassFields.Where(x => x.UISort != null).ToList();
                List<DocumentClassAllFieldsDTO> documentClassAllFieldsDTOs = new();
                documentClassAllFieldsDTOs.AddRange(getItemOfUIsortNotNull);
                documentClassAllFieldsDTOs.AddRange(getItemOfUIsortNull);

                return Ok(new { Items = documentClassAllFieldsDTOs });

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
        /// Gets the DocumentClasses By Company Id.
        /// </summary>
        /// <param name="companyId">Company Id</param>
        /// <returns>List Of Document Classes</returns>
        [HttpGet]
        [Route("documentClassesByCompanyId/{companyId}")]
        public async Task<IActionResult> GetDocumentClassesByCompanyId(int companyId)
        {
            try
            {
                var dto = new DocumentClassDTO();
                List<int> companyIds = new() { companyId };
                var result = await _repositoryCompanySpecificDocumentClass.GetAllActiveAsync(companyIds,
                                                                                            nameof(dto.DocumentClassName),
                                                                                            x => x.DocumentType
                                                                                            );
                return Ok(new { Items = result.List.Select(x => _mapper.Map<DocumentClassDTO>(x)).ToList() });
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
        /// Gets the DocumentClassFieldCodes By Company Id.
        /// </summary>
        /// <param name="companyId">Company Id</param>
        /// <returns>List Of DocumentClassFieldCode DTO</returns>
        [HttpGet]
        [Route("documentClassFieldCodesByCompanyId/{companyId}")]
        public IActionResult GetDocumentClassFieldCodesByCompanyId(int companyId)
        {
            try
            {
                List<DocumentClassFieldCodeDTO> listDocumentClassfieldCodes = GetDocumentClassFieldCodeByCompanyId(companyId);
                return Ok(new { Items = listDocumentClassfieldCodes });
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

        private List<DocumentClassFieldCodeDTO> GetDocumentClassFieldCodeByCompanyId(int companyId)
        {
            var documentClassesWithCompanyId = _repositoryCompanySpecificDocumentClass.Query(CompanyIds, x => x.CompanyId == companyId)
                                                                                       .Select(x => x.Id).ToList();
            var listDocumentClassfield_with_documentClassesWithCompanyId = _repositoryDocumentClassField.Query(x => documentClassesWithCompanyId.Contains(x.DocumentClassId)).AsNoTracking()
                                                                                                              .ToList();
            var listDocumentClassfieldCodes = listDocumentClassfield_with_documentClassesWithCompanyId.Select(x => _mapper.Map<DocumentClassFieldCodeDTO>(x)).ToList();
            return listDocumentClassfieldCodes;
        }

        private List<int> GetDocumentTypeIdsByRoleAccess()
        {
            var userRolelist = _repositorySystemUserRole.Query(x => x.SystemUserId == UserId)
                                                        .Select(x => x.SystemRoleId).ToList();

            var documentTypeIds = _repositoryDocumentTypeRoleAccess.Query(x => userRolelist.Contains(x.SystemRoleId))
                                                                    .Select(x => x.DocumentTypeId).ToList();
            return documentTypeIds;
        }
    }
}