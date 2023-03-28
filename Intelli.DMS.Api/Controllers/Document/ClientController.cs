using AutoMapper;
using Intelli.DMS.Api.Constants;
using Intelli.DMS.Api.DTO;
using Intelli.DMS.Api.Services.Status;
using Intelli.DMS.Domain.Core.Helpers;
using Intelli.DMS.Domain.Core.Repository;
using Intelli.DMS.Domain.Database;
using Intelli.DMS.Domain.Model;
using Intelli.DMS.Domain.Model.Views;
using Intelli.DMS.Domain.Repository;
using Intelli.DMS.Domain.Repository.Impl;
using Intelli.DMS.EventBus.RabbitMQ.Event;
using Intelli.DMS.EventBus.RabbitMQ.Sender;
using Intelli.DMS.Shared;
using Intelli.DMS.Shared.Mvc.Controllers;
using Intelli.DMS.Shared.Mvc.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intelli.DMS.Api.Controllers.Document
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClientController : BaseController
    {
        private const string key = "NumberOfClientsOnSearch";
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        private readonly IStatusService _statusService;
        private readonly IConfiguration _config;

        private readonly ICompanyRepository<Client> __companySpecificRepositoryClient;
        private readonly ICompanyRepository<ClientView> _companySpecificClientView;
        private readonly IRepository<ClientTag> _repositoryClientTag;
        private readonly IRepository<ClientCompanyCustomFieldValue> _repositoryClientCompanyCustomFieldValue;
        private readonly IRepository<Batch> _repositoryBatch;
        private readonly IRepository<ClientRepositoryView> _repositoryClientRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientController"/> class.
        /// </summary>
        /// <param name="context">Instance of <see cref="DMSContext"/> will be injected</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/> will be injected</param>
        /// <param name="logger">Instance of <see cref="ILogger"/> will be injected</param>
        /// <param name="sender">Instance of <see cref="IEventSender"/> will be injected</param>
        /// <param name="statusService">Instance of <see cref="IStatusService"/> will be injected</param>
        public ClientController(DMSContext context, IMapper mapper, ILogger<Client> logger,
            IEventSender sender, IStatusService statusService, IConfiguration configuration)
        {
            __companySpecificRepositoryClient = new CompanyEntityRepository<Client>(context);
            _companySpecificClientView = new CompanyEntityRepository<ClientView>(context);
            _repositoryClientTag = new GenericRepository<ClientTag>(context);
            _repositoryClientCompanyCustomFieldValue = new GenericRepository<ClientCompanyCustomFieldValue>(context);
            _repositoryBatch = new GenericRepository<Batch>(context);
            _repositoryClientRepository = new GenericRepository<ClientRepositoryView>(context);

            ((GenericRepository<Batch>)_repositoryBatch).AfterSave =
            ((GenericRepository<ClientCompanyCustomFieldValue>)_repositoryClientCompanyCustomFieldValue).AfterSave =
            ((GenericRepository<ClientTag>)_repositoryClientTag).AfterSave =
            ((CompanyEntityRepository<Client>)__companySpecificRepositoryClient).AfterSave = (logs) =>
                sender.SendEvent(new MQEvent<List<AuditEntry>>(AUDIT_EVENT_NAME, (List<AuditEntry>)logs));

            _config = configuration;
            _logger = logger;
            _mapper = mapper;
            _statusService = statusService;
        }



        /// <summary>
        /// Get All Customers.
        /// </summary>
        /// <returns>List of CustomerDTO</returns>
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll()
        {
            var dto = new ClientDTO();
            var result = await __companySpecificRepositoryClient.GetAllActiveAsync(CompanyIds, nameof(dto.FirstName));
            var listofClients = result.List.Select(x => _mapper.Map<ClientDTO>(x)).ToList();

            foreach (var client in listofClients)
            {
                client.LastName = $"{client.LastName} - {client.JMBG} - {client.ExternalId}";
            }

            return Ok(new { Items = listofClients });
        }

        /// <summary>
        /// Get All Customers.
        /// </summary>
        /// <param name="clientName">Client Name.</param>
        /// <returns>List of ClientDTO</returns>
        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> GetAll(string clientName, int companyId)
        {
            var take = _config.GetValue<int>(key, 10);
            List<int> companyIds = companyId > 0 ? new List<int>() { companyId } : CompanyIds;
            var query = __companySpecificRepositoryClient.Query(companyIds, x => ((x.FirstName.Contains(clientName ?? String.Empty)
                                                                                   || x.LastName.Contains(clientName ?? String.Empty)
                                                                                   || x.JMBG.Contains(clientName ?? String.Empty)
                                                                                   || x.ExternalId.Contains(clientName ?? String.Empty))
                                                    && (x.IsActive == true))).Take(take);
            var result = await query.ToListAsync();
            var clients = result.Select(x => _mapper.Map<ClientDTO>(x)).ToList();
            foreach (var client in clients)
            {
                client.LastName = $"{client.LastName} - {client.JMBG} - {client.ExternalId}";
            }

            return Ok(new { Items = clients });
        }

        /// <summary>
        /// Gets the Customer w.r.t query string parameters.
        /// </summary>
        /// <param name="queryStringParams">The query string params.</param>
        /// <returns>An IActionResult.</returns>
        [HttpGet]
        public IActionResult Get([FromQuery] QueryStringParams queryStringParams)
        {
            _logger.LogInformation("Get Client Called with params: {0}", queryStringParams);

            PagedResult<ClientDTO> result = null;
            try
            {
                QueryResult<ClientView> queryResult = _companySpecificClientView.Get(
                                                    CompanyIds,
                                                    queryStringParams.FilterExpression,
                                                    queryStringParams.OrderBy,
                                                    queryStringParams.PageSize,
                                                    queryStringParams.PageNumber
                                                    );

                int total = queryResult.Count;
                IEnumerable<ClientView> List = queryResult.List;

                result = new PagedResult<ClientDTO>(
                                total,
                                queryStringParams.PageNumber,
                                List.Select(x => _mapper.Map<ClientDTO>(x)).ToList(),
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
        /// Gets the Customer.
        /// </summary>
        /// <param name="id">The Customer id.</param>
        /// <returns>The CustomerDTO.</returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var entity = __companySpecificRepositoryClient.Query(CompanyIds, x => x.Id == id)
                                               .Include(x => x.ClientTags)
                                               .ThenInclude(x => x.CreatedBy)
                                               .Include(x => x.ClientCompanyCustomFieldValues)
                                                .ThenInclude(x => x.Field)
                                                 .ThenInclude(x => x.DictionaryType)
                                                  .ThenInclude(x => x.BopDictionaries)
                                                   .FirstOrDefault();

                var mappedEntity = _mapper.Map<ClientDTO>(entity);

                if (mappedEntity != null)
                {
                    mappedEntity.Tags = _mapper.Map<List<ClientTagDTO>>(entity.ClientTags);
                    if (entity?.ClientCompanyCustomFieldValues != null)
                    {
                        mappedEntity.ClientCompanyFieldValues = CreateClientCompanyFieldValueList(entity.ClientCompanyCustomFieldValues);
                    }
                    //Get All ClientRepository of this Client
                    var clientrepository = _repositoryClientRepository.Query(x => x.ClientId == entity.Id);
                    if (clientrepository.Any())
                    {
                        mappedEntity.Repository = _mapper.Map<List<ClientRepositoryViewDTO>>(clientrepository.ToList());
                    }
                    //Get Batch of this Client
                    var batch = _repositoryBatch.Query(x => x.CustomerId == mappedEntity.Id).FirstOrDefault();

                    // Check Batch Not NUll
                    if (batch != null)
                    {
                        // Check Batch Status is Pending
                        if (batch.BatchStatusId == BatchStatusConstants.Pending)
                        {
                            //Set IsNotValidForTransaction to True 
                            mappedEntity.IsNotValidForTransaction = true;
                        }
                        // Check Batch Status is Checked
                        else if (batch.BatchStatusId == BatchStatusConstants.Checked)
                        {
                            //Set IsChecked to True
                            mappedEntity.IsChecked = true;
                        }
                        mappedEntity.IsNotVaildForChangeStatus = true;
                    }
                }
                return Ok(mappedEntity);
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

        private List<CompanyCustomFieldDTO> CreateClientCompanyFieldValueList(ICollection<ClientCompanyCustomFieldValue> clientCompanyCustomFieldValues)
        {
            var fieldValueDTOs = new List<CompanyCustomFieldDTO>();
            foreach (var item in clientCompanyCustomFieldValues)
            {
                string selectedDictionaryTypeName = "";
                var dictionary = _mapper.Map<DictionaryTypeDTO>(item.Field.DictionaryType);
                // if DocumentClassField is DictionaryType
                if (item.Field.DictionaryType != null)
                {
                    // Set Selected Dictionary Type Name
                    int? bopDictionaryId = Convert.ToInt32(item.RegisteredFieldValue);
                    if (bopDictionaryId != 0)
                    {
                        selectedDictionaryTypeName = item?.Field?.DictionaryType
                                                           ?.BopDictionaries
                                                            ?.Where(x => x.Id == bopDictionaryId)
                                                             ?.FirstOrDefault()!.Value;
                        dictionary.SelectedDictionaryTypeName = selectedDictionaryTypeName;
                    }
                }
                CompanyCustomFieldDTO fieldValueDTO = new()
                {
                    Id = item.FieldId,
                    CompanyId = item.Field.CompanyId,
                    FieldTypeId = item.Field.DocumentClassFieldTypeId,
                    Uilabel = item.Field?.Uilabel,
                    UISort = item.Field?.UISort,
                    DictionaryTypeId = item.Field.DictionaryTypeId,
                    IsMandatory = item.Field.IsMandatory,
                    IsActive = item.IsActive,
                    MinLength = item.Field.MinLength,
                    MaxLength = item.Field.MaxLength,
                    DictionaryValueId = item.Field.DictionaryTypeId,
                    FieldValue = item.RegisteredFieldValue,
                    DictionaryType = dictionary,
                };



                // DocumentClassFieldTypeId Equal to ExpirationDate
                if (item.Field.DocumentClassFieldTypeId == 10)
                {
                    // Set ExpirationDate value to proper format
                    bool checkLongConvertion = long.TryParse(item.RegisteredFieldValue, out long unixLong);
                    if (checkLongConvertion != false)
                    {
                        DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(unixLong / 1000);
                        DateTime date = dateTimeOffset.DateTime.AddDays(1);
                        if (date.Month < 10)
                        {
                            if (date.Day < 10)
                            {
                                fieldValueDTO.FieldValue = $"{date.Year}-0{date.Month}-0{date.Day}";
                            }
                            else
                            {
                                fieldValueDTO.FieldValue = $"{date.Year}-0{date.Month}-{date.Day}";
                            }
                        }
                        else
                        {
                            if (date.Day < 10)
                            {
                                fieldValueDTO.FieldValue = $"{date.Year}-{date.Month}-0{date.Day}";
                            }
                            else
                            {
                                fieldValueDTO.FieldValue = $"{date.Year}-{date.Month}-{date.Day}";
                            }
                        }
                    }
                }

                fieldValueDTOs.Add(fieldValueDTO);

            }
            var itemsCompanyCustomFields = fieldValueDTOs.OrderBy(x => x.UISort).ToList();
            var getItemOfUIsortNull = itemsCompanyCustomFields.Where(x => x.UISort == null).ToList();
            var getItemOfUIsortNotNull = itemsCompanyCustomFields.Where(x => x.UISort != null).ToList();
            List<CompanyCustomFieldDTO> companyCustomFieldDTOs = new();
            companyCustomFieldDTOs.AddRange(getItemOfUIsortNotNull);
            companyCustomFieldDTOs.AddRange(getItemOfUIsortNull);
            fieldValueDTOs = companyCustomFieldDTOs;
            return fieldValueDTOs;
        }


        [HttpGet("SearchClientNameByRepoId/{RepoId}")]
        public IActionResult SearchClientNameByRepoId(int RepoId)
        {
            var client = _repositoryBatch.Query(x => x.Id == RepoId).Include(x => x.Customer).FirstOrDefault();
            return Ok($"{client?.Customer.FirstName} {client?.Customer.LastName}");
        }


        /// <summary>
        /// Creates the Customer.
        /// </summary>
        /// <param name="dto">The Customer dto.</param>
        /// <returns>An IActionResult.</returns>
        /// 

        [HttpPost]
        public async Task<IActionResult> Post(ClientDTO dto)
        {
            // log information
            _logger.LogInformation("Creating Client: {0}", dto);

            // Checking if the passed DTO is valid
            if (!ModelState.IsValid || dto == null)
                return BadRequest(MsgKeys.InvalidInputParameters);


            await using var trans = __companySpecificRepositoryClient.GetTransaction();
            ClientDTO dtoResult = new();
            try
            {
                var entityClient = _mapper.Map<Client>(dto);


                dto.JMBG = dto.JMBG.Trim(' ', '\t');
                //duplicate JMBG validations
                if (__companySpecificRepositoryClient.Query(CompanyIds, orderBy: s => s.OrderBy(s => s.JMBG)).Any(d => d.JMBG == dto.JMBG))
                    return BadRequest(MsgKeys.RecordWithSameJMBGExists);

                dto.ExternalId = dto.ExternalId.Trim(' ', '\t');
                //duplicate External Id validations
                if (__companySpecificRepositoryClient.Query(null, orderBy: s => s.OrderBy(s => s.ExternalId)).Any(d => d.ExternalId == dto.ExternalId))
                    return BadRequest(MsgKeys.RecordWithSameExternalIdExists);

                __companySpecificRepositoryClient.Insert(CompanyIds, entityClient);
                __companySpecificRepositoryClient.SaveChanges(UserName, null, trans);

                //storing list of fields for document class
                if (dto.Tags != null)
                {
                    foreach (var tag in dto.Tags)
                    {
                        tag.ClientId = entityClient.Id;
                        tag.CreatedById = UserId;
                        var entityTag = _mapper.Map<ClientTag>(tag);
                        _repositoryClientTag.Insert(entityTag);
                        _repositoryClientTag.SaveChanges(UserName, null, trans);
                    }
                }
                // Check that ClientCompanyFieldValues Not Null
                if (dto.ClientCompanyFieldValues != null)
                {
                    // Add New  ClientCompanyFieldValues of this Client
                    SaveClientCompanyFieldValues(dto, trans, entityClient);
                }
                dtoResult = _mapper.Map<ClientDTO>(entityClient);
                dtoResult.Tags = dto.Tags;

                await trans.CommitAsync();
                _logger.LogInformation("Creating Client with client Id: {0}", dto.Id);

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

            return Ok(dtoResult);
        }

        /// <summary>
        /// Updates the Customer.
        /// </summary>
        /// <param name="id">The Customer id.</param>
        /// <param name="dto">The CustomerDTO.</param>
        /// <returns>An IActionResult.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ClientDTO dto)
        {
            // log information
            _logger.LogInformation("Updating Client: Id:{0} | {1}", id, dto);

            // Checking if the passed DTO is valid
            if (!ModelState.IsValid || dto == null || id != dto.Id)
                return BadRequest(MsgKeys.InvalidInputParameters);

            await using var trans = __companySpecificRepositoryClient.GetTransaction();

            Client entity = new();
            ClientDTO dtoResult = new();
            try
            {
                entity = _mapper.Map<Client>(dto);
                dtoResult = _mapper.Map<ClientDTO>(entity);

                if (dto.IsJMBGDuplicationCheck)
                {
                    dto.JMBG = dto.JMBG.Trim(' ', '\t');
                    //duplicate JMBG validations
                    if (__companySpecificRepositoryClient.Query(CompanyIds, orderBy: s => s.OrderBy(s => s.JMBG)).Any(d => d.JMBG == dto.JMBG))
                        return BadRequest(MsgKeys.RecordWithSameJMBGExists);
                }
                if (dto.IsExternalIdDuplicationCheck)
                {
                    dto.ExternalId = dto.ExternalId.Trim(' ', '\t');
                    //duplicate External Id validations
                    if (__companySpecificRepositoryClient.Query(null, orderBy: s => s.OrderBy(s => s.ExternalId)).Any(d => d.ExternalId == dto.ExternalId))
                        return BadRequest(MsgKeys.RecordWithSameExternalIdExists);
                }
                __companySpecificRepositoryClient.Update(CompanyIds, entity);
                __companySpecificRepositoryClient.SaveChanges(UserName, null, trans);

                //Check IsNotValidForTransaction for Set Client Status Pending
                if (dto.IsNotValidForTransaction)
                {
                    _statusService.SetBatchStatusWithClientId(entity.Id, BatchStatusConstants.Pending);
                }
                //Check IsChecked for Set Client Status Checked
                else if (dto.IsChecked)
                {
                    _statusService.SetBatchStatusWithClientId(entity.Id, BatchStatusConstants.Checked);
                }
                //Check IsChecked and IsNotValidForTransaction false
                //Set Client Status Created
                else if (!dto.IsNotValidForTransaction && !dto.IsChecked)
                {
                    _statusService.SetBatchStatusWithClientId(entity.Id, BatchStatusConstants.Created);
                }
                //updating class fields
                if (dto.Tags != null)
                {
                    if (dto.Tags.Count > 0)
                    {
                        foreach (var tag in dto.Tags.ToList())
                        {
                            var entityTag = _mapper.Map<ClientTag>(tag);

                            if (tag.RecordStatus == RecordStatusConstant.InDB)
                            {
                                continue;
                            }
                            else if (tag.RecordStatus == RecordStatusConstant.AddToDB)
                            {
                                entityTag.CreatedById = UserId;
                                _repositoryClientTag.Insert(entityTag);
                            }
                            else if (tag.RecordStatus == RecordStatusConstant.UpdateFromDB)
                            {
                                entityTag.CreatedById = UserId;
                                _repositoryClientTag.Update(entityTag);
                            }
                            else if (tag.RecordStatus == RecordStatusConstant.DeleteFromDb)
                            {
                                _repositoryClientTag.Delete(tag.Id, false);
                            }

                            _repositoryClientTag.SaveChanges(UserName, null, trans);
                        }
                    }
                }

                //Check ClientCompanyFieldValues Not Null
                if (dto.ClientCompanyFieldValues != null)
                {
                    // Delete Previous ClientCompanyFieldValues of this Client
                    _repositoryClientCompanyCustomFieldValue.Delete(x => x.ClientId == entity.Id);
                    _repositoryClientCompanyCustomFieldValue.SaveChanges(UserName, null, trans);

                    // Add New ClientCompanyFieldValues of this Client
                    SaveClientCompanyFieldValues(dto, trans, entity);
                }
                dtoResult.Tags = dto.Tags;
                await trans.CommitAsync();
                _logger.LogInformation("Update Client with client Id: {0}", dto.Id);

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
            return Ok(dtoResult);
        }

        /// <summary>
        /// Delete the Customer.
        /// </summary>
        /// <param name="id">The Customer id.</param>
        /// <returns>An IActionResult.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var query = __companySpecificRepositoryClient.Query(CompanyIds, x => x.Id == id);

                var entity = await query.FirstOrDefaultAsync();
                if (entity == null)
                    return NotFound();

                __companySpecificRepositoryClient.Delete(CompanyIds, id);
                __companySpecificRepositoryClient.SaveChanges(UserName, null, null);

                _logger.LogInformation("Delete Client with client Id: {0}", id);

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
        /// Get All Customers By Company Id.
        /// </summary>
        /// <param name="companyId">Company Id.</param>
        /// <returns>List of ClientDTO</returns>
        [HttpGet]
        [Route("ClientByCompanyId/{companyId}")]
        public async Task<IActionResult> GetClientByCompanyId(int companyId)
        {
            try
            {
                var query = __companySpecificRepositoryClient.Query(CompanyIds, x => (x.CompanyId == companyId) && (x.IsActive == true));
                var result = await query.ToListAsync();

                return Ok(new { Items = result.Select(x => _mapper.Map<ClientDTO>(x)).ToList() });
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
        /// Save Client Company Field Values
        /// </summary>
        /// <param name="dto"> Instance of ClientDTO </param>
        /// <param name="trans"> Instance of Transaction </param>
        /// <param name="entity"> Instance of Client </param>
        private void SaveClientCompanyFieldValues(ClientDTO dto, ITransactionHandler trans, Client entity)
        {
            //This Function made for Save Client Company Field Values and use this
            // Function in Post and Put Call of Client

            foreach (CompanyCustomFieldDTO item in dto.ClientCompanyFieldValues)
            {
                var clientCompanyCustomFieldValue = new ClientCompanyCustomFieldValue
                {
                    ClientId = entity.Id,
                    DictionaryValueId = item.DictionaryValueId,
                    FieldId = item.Id,
                    RegisteredFieldValue = item.FieldValue,
                };

                _repositoryClientCompanyCustomFieldValue.Insert(clientCompanyCustomFieldValue);
                _repositoryClientCompanyCustomFieldValue.SaveChanges(UserName, null, trans);
            }
        }
    }
}