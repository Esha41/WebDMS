using AutoMapper;
using Intelli.DMS.Api.Constants;
using Intelli.DMS.Api.DTO;
using Intelli.DMS.Api.Helpers;
using Intelli.DMS.Domain.Core.Helpers;
using Intelli.DMS.Domain.Core.Repository;
using Intelli.DMS.Domain.Database;
using Intelli.DMS.Domain.Model;
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
using EncryptionHelper = Intelli.DMS.Api.Helpers.EncryptionHelper;

namespace Intelli.DMS.Api.Controllers.v1
{
    /// <summary>
    /// The Companies Controller.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CompaniesController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IRepository<Company> _repository;
        private readonly IRepository<CompanyCustomField> _repositoryCompanyCustomField;
        

        /// <summary>
        /// Initializes a new instance of the <see cref="CompaniesController"/> class.
        /// </summary>
        /// <param name="context">Instance of <see cref="DMSContext"/> will be injected</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/> will be injected</param>
        /// <param name="logger">Instance of <see cref="ILogger"/> will be injected</param>
        /// <param name="sender">Instance of <see cref="IEventSender"/> will be injected</param>
        public CompaniesController(DMSContext context,
            IMapper mapper,
            ILogger<CompaniesController> logger,
            IEventSender sender)
        {
            _repository = new GenericRepository<Company>(context);
            _repositoryCompanyCustomField = new GenericRepository<CompanyCustomField>(context);

            ((GenericRepository<CompanyCustomField>)_repositoryCompanyCustomField).AfterSave =
            ((GenericRepository<Company>)_repository).AfterSave = (logs) =>
                 sender.SendEvent(new MQEvent<List<AuditEntry>>(AUDIT_EVENT_NAME, (List<AuditEntry>)logs));

            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Get All Companies.
        /// </summary>
        /// <returns>List of CompanyDTO</returns>
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var dto = new CompanyDTO();
            var result = await _repository.GetAllActiveAsync(nameof(dto.CompanyName));
            result = FilterCompaniesBasedUponCompanyIds(result);
            int check = 0;
            foreach (var item in result.List)
            {
                if (!int.TryParse(item.UsersPerCompany, out check))
                {
                    item.UsersPerCompany = EncryptionHelper.DecryptString(item.UsersPerCompany);
                }
            }
            return Ok(new { Items = result.List.Select(x => _mapper.Map<CompanyDTO>(x)).ToList() });
        }

        /// <summary>
        /// Gets the companies w.r.t query string parameters.
        /// </summary>
        /// <param name="queryStringParams">The query string params.</param>
        /// <returns>An IActionResult.</returns>
        [HttpGet]
        public IActionResult Get([FromQuery] QueryStringParams queryStringParams)
        {
            _logger.LogInformation("Get Companies Called with params: {0}", queryStringParams);

            PagedResult<CompanyDTO> result = null;
            try
            {
                QueryResult<Company> queryResult = _repository.Get(
                                                    queryStringParams.FilterExpression,
                                                    queryStringParams.OrderBy,
                                                    queryStringParams.PageSize,
                                                    queryStringParams.PageNumber);

                queryResult = FilterCompaniesBasedUponCompanyIds(queryResult);

                int total = queryResult.Count;
                IEnumerable<Company> roleList = queryResult.List;
                foreach (var item in roleList)
                {
                    item.UsersPerCompany = EncryptionHelper.DecryptString(item.UsersPerCompany);
                }
                result = new PagedResult<CompanyDTO>(
                                total,
                                queryStringParams.PageNumber,
                                roleList.Select(x => _mapper.Map<CompanyDTO>(x)).ToList(),
                                queryStringParams.PageSize
                            );
            }
            catch (ArgumentException e)
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
        /// Creates the company.
        /// </summary>
        /// <param name="dto">The company dto.</param>
        /// <returns>An IActionResult.</returns>
        [HttpPost]
        public async Task<IActionResult> Post(CompanyDTO dto)
        {
            await using var trans = _repository.GetTransaction();

            try
            {
                // Checking if the passed DTO is valid
                if (!ModelState.IsValid || dto == null)
                    return BadRequest(MsgKeys.InvalidInputParameters);

                dto.CompanyCode = dto.CompanyCode.Trim(' ','\t');
                //duplicate CompanyCode validations
                if (_repository.Query(d => d.CompanyCode == dto.CompanyCode)?.FirstOrDefault() != null)
                    return BadRequest(MsgKeys.RecordWithSameCompanyCodeExists);

                dto.UserName = dto.UserName.Trim(' ','\t');
                //duplicate UserName validations
                if (_repository.Query(d => d.UserName == dto.UserName)?.FirstOrDefault() != null)
                    return BadRequest(MsgKeys.RecordWithSameUserNameExists);

                dto.ActiveDirectoryDomainName = dto.ActiveDirectoryDomainName.Trim(' ','\t');
                //duplicate ActiveDirectoryDomainName validations
                if (_repository.Query(d => d.ActiveDirectoryDomainName == dto.ActiveDirectoryDomainName)?.FirstOrDefault() != null)
                    return BadRequest(MsgKeys.RecordWithSameActiveDirectoryDomainNameExists);

                var entity = _mapper.Map<Company>(dto);
                entity.UsersPerCompany = EncryptionHelper.EncryptString(entity.UsersPerCompany.ToString());
                entity.Password = EncryptionHelper.EncryptString(entity.Password.ToString());

                _repository.Insert(entity);
                _repository.SaveChanges(UserName,null, trans);

                if (dto.Fields != null)
                {
                    foreach (var field in dto.Fields.ToList())
                    {
                        field.CompanyId = entity.Id;
                        var entityField = _mapper.Map<CompanyCustomField>(field);
                        entityField.DocumentClassFieldTypeId = field.FieldTypeId;
                        entityField.DocumentClassFieldType = null;
                        _repositoryCompanyCustomField.Insert(entityField);
                        _repositoryCompanyCustomField.SaveChanges(UserName, null, trans);
                        field.Id = entityField.Id;
                    }
                }
                entity.UsersPerCompany = EncryptionHelper.DecryptString(entity.UsersPerCompany);
                var companyDTO = _mapper.Map<CompanyDTO>(entity);
                companyDTO.Fields = dto.Fields;


                await trans.CommitAsync();

                return Ok(companyDTO);
            }
            catch (ArgumentException e)
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

        /// <summary>
        /// Gets the company.
        /// </summary>
        /// <param name="id">The company id.</param>
        /// <returns>The CompanyDTO.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var query = _repository.Query(x => x.Id == id)
                            .Include(x => x.DocumentsPerCompanies)
                            .Include(x=>x.CompanyCustomFields)
                             .ThenInclude(x=>x.DocumentClassFieldType)
                            .Include(x => x.CompanyCustomFields)
                             .ThenInclude(x => x.DictionaryType)
                            .Include(x => x.UserCompanies)
                            .ThenInclude(x => x.SystemUser);


                var entity = await query.FirstOrDefaultAsync();


                if (entity == null) return NotFound();


                entity.UsersPerCompany = EncryptionHelper.DecryptString(entity.UsersPerCompany);
                entity.Password = EncryptionHelper.DecryptString(entity.Password);
                var dto = _mapper.Map<CompanyDTO>(entity);
                
                dto.Fields = entity?.CompanyCustomFields
                                    ?.Where(x => x.IsActive == true)
                                    ?.Select(x => _mapper.Map<CompanyCustomFieldDTO>(x))
                                    ?.ToList();

                dto?.Fields?.ForEach(x => x.RecordStatus = RecordStatusConstant.InDB);
                
                dto.SystemUsers = GetAllUsersOfSelectedCompany(entity);
                return Ok(dto);
            }
            catch (ArgumentException e)
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
        /// Updates the company.
        /// </summary>
        /// <param name="id">The company id.</param>
        /// <param name="dto">The CompanyDTO.</param>
        /// <returns>An IActionResult.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, CompanyDTO dto)
        {
            await using var trans = _repository.GetTransaction();

            try
            {
                // Checking if the passed DTO is valid
                if (!ModelState.IsValid || dto == null || id != dto.Id)
                    return BadRequest(MsgKeys.InvalidInputParameters);

                if (dto.IsCompanyCodeChanged)
                {
                    dto.CompanyCode = dto.CompanyCode.Trim(' ','\t');
                    //duplicate CompanyCode validations
                    if (_repository.Query(d => d.CompanyCode == dto.CompanyCode)?.FirstOrDefault() != null)
                        return BadRequest(MsgKeys.RecordWithSameCompanyCodeExists);
                }
                
                if (dto.IsUserNameChange)
                {
                    dto.UserName = dto.UserName.Trim(' ','\t');
                    //duplicate UserName validations
                    if (_repository.Query(d => d.UserName == dto.UserName)?.FirstOrDefault() != null)
                        return BadRequest(MsgKeys.RecordWithSameUserNameExists);
                }
                if (dto.IsActiveDirectoryDomainNameChange)
                {
                    dto.ActiveDirectoryDomainName = dto.ActiveDirectoryDomainName.Trim(' ','\t');
                    //duplicate ActiveDirectoryDomainName validations
                    if (_repository.Query(d => d.ActiveDirectoryDomainName == dto.ActiveDirectoryDomainName)?.FirstOrDefault() != null)
                        return BadRequest(MsgKeys.RecordWithSameActiveDirectoryDomainNameExists);
                }

                    var entity = _mapper.Map<Company>(dto);
                entity.UsersPerCompany = EncryptionHelper.EncryptString(dto.UsersPerCompany.ToString());
                entity.Password = EncryptionHelper.EncryptString(entity.Password.ToString());

                _repository.Update(entity);
                _repository.SaveChanges(UserName, null, trans);

                //updating Company Custom Fields
                if (dto.Fields != null)
                {
                    foreach (var field in dto.Fields.ToList())
                    {
                        field.CompanyId = entity.Id;
                        var entityField = _mapper.Map<CompanyCustomField>(field);
                        entityField.DocumentClassFieldTypeId = field.FieldTypeId;
                        entityField.DocumentClassFieldType = null;
                        if (field.RecordStatus == RecordStatusConstant.InDB)
                        {
                            continue;
                        }
                        else if (field.RecordStatus == RecordStatusConstant.AddToDB)
                        {
                            _repositoryCompanyCustomField.Insert(entityField);
                        }
                        else if (field.RecordStatus == RecordStatusConstant.UpdateFromDB)
                        {
                            _repositoryCompanyCustomField.Update(entityField);
                        }
                        else if (field.RecordStatus == RecordStatusConstant.DeleteFromDb)
                        {
                            _repositoryCompanyCustomField.Delete(field.Id, true);
                        }

                    }
                    _repositoryCompanyCustomField.SaveChanges(UserName,null, trans);
                }
                await trans.CommitAsync();
                entity.UsersPerCompany = EncryptionHelper.DecryptString(entity.UsersPerCompany);
                return Ok(_mapper.Map<CompanyDTO>(entity));
            }
            catch (ArgumentException e)
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

        /// <summary>
        /// Delete the company.
        /// </summary>
        /// <param name="id">The company id.</param>
        /// <returns>An IActionResult.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var query = _repository.Query(x => x.Id == id)
                            .Include(x => x.DocumentsPerCompanies)
                            .Include(x => x.UserCompanies);

                var entity = await query.FirstOrDefaultAsync();
                if (entity == null) return NotFound();

                if (entity.DocumentsPerCompanies.Count == 0 && entity.UserCompanies.Count == 0)
                {
                    entity.IsActive = false;
                    _repository.SaveChanges(UserName,null,null);

                    return Ok(MsgKeys.DeletedSuccessfully);
                }

                return BadRequest(MsgKeys.ChildEntityExists);
            }
            catch (ArgumentException e)
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
        /// Filter Companies Based Upon CompanyIds
        /// </summary>
        /// <param name="result">Instance Of QueryResult<Company> </param>
        /// <returns></returns>
        private QueryResult<Company> FilterCompaniesBasedUponCompanyIds(QueryResult<Company> result)
        {
            // if user is associated it one or more companies then filter all those companies
            if (CompanyIds != null && CompanyIds.Count >= 1)
            {
                result.List = result.List.FindAll(x => CompanyIds.Contains(x.Id));
                result.Count = result.List.Count;
            }

            return result;
        }

        /// <summary>
        /// Get All Users Of Selected Company
        /// </summary>
        /// <param name="entity">Instance of <see cref="Company"/> </param>
        /// <returns></returns>
        private List<UserReadDTO> GetAllUsersOfSelectedCompany(Company entity)
        {
            List<UserReadDTO> users = new();
            //add company users in dto
            if (entity != null)
            {
                foreach (var userCompany in entity.UserCompanies)
                {
                    List<string> userCompanies = GetAllAssociatedCompanies(userCompany.SystemUser.UserCompanies);
                    userCompany.SystemUser.UserCompanies = null;
                    var dto = _mapper.Map<UserReadDTO>(userCompany.SystemUser);
                    dto.CompanyNames = userCompanies;
                    users.Add(dto);

                }
            }

            return users;
        }

        /// <summary>
        /// Get All GetAllDocumentClassFields.
        /// </summary>
        /// <returns>List of GetAllDocumentClassFields associated with document class</returns>
        [HttpGet("fields/{companyId}")]
        public IActionResult GetAllCompanyCustomFields(int companyId)
        {
            try
            {
                var result = _repositoryCompanyCustomField.Query(filter: s => s.CompanyId == companyId &&
                                                                              s.IsActive == true)
                                                            .Include(x => x.DictionaryType)
                                                            .ThenInclude(x => x.BopDictionaries)
                                                            .OrderBy(x => x.UISort);
                var iitemsCompanyCustomFields = result.Select(x => _mapper.Map<CompanyCustomFieldDTO>(x)).ToList();
                var getItemOfUIsortNull = iitemsCompanyCustomFields.Where(x => x.UISort == null).ToList();
                var getItemOfUIsortNotNull = iitemsCompanyCustomFields.Where(x => x.UISort != null).ToList();
                List<CompanyCustomFieldDTO> companyCustomFieldDTOs = new();
                companyCustomFieldDTOs.AddRange(getItemOfUIsortNotNull);
                companyCustomFieldDTOs.AddRange(getItemOfUIsortNull);

                return Ok(new { Items = companyCustomFieldDTOs });

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
        /// Get All Associated Companies
        /// </summary>
        /// <param name="userCompany"></param>
        /// <returns></returns>
        private static List<string> GetAllAssociatedCompanies(ICollection<UserCompany> userCompany)
        {
            List<string> userCompanies = new();

            if (userCompany != null)
            {
                foreach (var company in userCompany)
                {
                    if (company.Company != null)
                    {
                        userCompanies.Add(company.Company.CompanyName);
                    }

                }
            }
            return userCompanies;
        }
    }
}
