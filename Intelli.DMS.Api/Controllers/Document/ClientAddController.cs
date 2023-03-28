using AutoMapper;
using Intelli.DMS.Api.Basic_Authorization;
using Intelli.DMS.Api.DTO;
using Intelli.DMS.Domain.Core.Helpers;
using Intelli.DMS.Domain.Core.Repository;
using Intelli.DMS.Domain.Database;
using Intelli.DMS.Domain.Model;
using Intelli.DMS.EventBus.RabbitMQ.Event;
using Intelli.DMS.EventBus.RabbitMQ.Sender;
using Intelli.DMS.Shared.Mvc.Controllers;
using Intelli.DMS.Shared.Mvc.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intelli.DMS.Api.Controllers.Document
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [BasicAuthentication]
    public class ClientAddController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly string _username;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<Client> _repositoryClient;
        private readonly IRepository<ClientTag> _repositoryClientTag;
        private readonly IRepository<ClientCompanyCustomFieldValue> _repositoryClientCompanyCustomFieldValue;
        private readonly IRepository<CompanyCustomField> _repositoryCompanyCustomField;
        private readonly IRepository<DocumentClassFieldType> _repositoryFieldType;
        private readonly IRepository<BopDictionary> _repositoryBopDictionary;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientAddController"/> class.
        /// </summary>
        /// <param name="context">Instance of <see cref="DMSContext"/> will be injected</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/> will be injected</param>
        /// <param name="logger">Instance of <see cref="ILogger"/> will be injected</param>
        public ClientAddController(DMSContext context, IMapper mapper, ILogger<Client> logger,
            IEventSender sender, IConfiguration configuration)
        {
            _configuration = configuration;
            _username = _configuration.GetSection("authUsername").Value;

            _repositoryClient = new GenericRepository<Client>(context);
            _repositoryClientTag = new GenericRepository<ClientTag>(context);
            _repositoryClientCompanyCustomFieldValue = new GenericRepository<ClientCompanyCustomFieldValue>(context);
            _repositoryCompanyCustomField = new GenericRepository<CompanyCustomField>(context);
            _repositoryFieldType = new GenericRepository<DocumentClassFieldType>(context);
            _repositoryBopDictionary = new GenericRepository<BopDictionary>(context);

            ((GenericRepository<ClientCompanyCustomFieldValue>)_repositoryClientCompanyCustomFieldValue).AfterSave =

                      ((GenericRepository<ClientTag>)_repositoryClientTag).AfterSave =
            ((GenericRepository<Client>)_repositoryClient).AfterSave = (logs) =>
            {
                sender.SendEvent(new MQEvent<List<AuditEntry>>(AUDIT_EVENT_NAME, (List<AuditEntry>)logs));
            };

            _logger = logger;
            _mapper = mapper;
        }
        /// <summary>
        /// Creates the Customer.
        /// </summary>
        /// <param name="dto">The Customer dto.</param>
        /// <returns>An IActionResult.</returns>
        [HttpPost]
        public async Task<IActionResult> Post(ClientAddDTO dto)
        {
            // log information
            _logger.LogInformation("Creating Client: {0}", dto);

            // Checking if the passed DTO is valid
            if (!ModelState.IsValid || dto == null)
                return BadRequest(MsgKeys.InvalidInputParameters);


            await using var trans = _repositoryClient.GetTransaction();
            ClientDTO dtoResult = new();
            try
            {
                var entityClient = _mapper.Map<Client>(dto);
                int companyId = BasicAuthStaticContants.CompanyId;

                dto.JMBG = dto.JMBG.Trim(' ','\t');

                if(dto.JMBG.Length <= 0 || dto.JMBG.Length > 20 )
                {
                    return BadRequest(MsgKeys.InvalidJMBGLength);
                }

                //duplicate JMBG validations
                if (_repositoryClient.Query(x => x.CompanyId == companyId &&
                                               x.JMBG == dto.JMBG).Any(d => d.JMBG == dto.JMBG))
                    return BadRequest(MsgKeys.RecordWithSameJMBGExists);

                dto.ExternalId = dto.ExternalId.Trim(' ','\t');
                //duplicate External Id validations
                if (_repositoryClient.Query(x => x.ExternalId == dto.ExternalId)
                                      .Any(d => d.ExternalId == dto.ExternalId))
                    return BadRequest(MsgKeys.RecordWithSameExternalIdExists);


                entityClient.CompanyId = companyId;
                //saving client
                _repositoryClient.Insert(entityClient);
                _repositoryClient.SaveChanges("Basic Auth Call of Company Id = "+BasicAuthStaticContants.CompanyId, null, trans);
                dtoResult = _mapper.Map<ClientDTO>(entityClient);

                // Check that ClientCompanyFieldValues Not Null
                if (dto.ClientCompanyFieldValues != null)
                {
                    //getting custom fields against company Id
                    var customFieldsFromDatabase = _repositoryCompanyCustomField.Query(d => d.CompanyId == companyId && d.IsActive == true)
                                                                           .ToList();
                    List<CompanyCustomField> customFieldsFromDb = new();
                    
                    foreach (var item in customFieldsFromDatabase)
                    {
                        var cloneItem = (CompanyCustomField)item.Clone();
                        customFieldsFromDb.Add(cloneItem);
                    }

                    var customFieldsToAdd = new List<CompanyCustomField>();

                    foreach (var dtoField in dto.ClientCompanyFieldValues)
                    {
                        //test if custom field exists of the above companyId
                        var checkField = customFieldsFromDb?.FirstOrDefault(s => s.Uilabel.ToLower() == dtoField.Uilabel.ToLower());
                        if (checkField != null)
                        {
                            //validate custom fields
                            //get field type

                            var fieldType = _repositoryFieldType.Query(d => d.Id == checkField.DocumentClassFieldTypeId).FirstOrDefault().Type;
                            if ((fieldType == "Integer" || fieldType == "Decimal") &&
                             (Convert.ToInt64(dtoField.FieldValue) < checkField.MinLength || Convert.ToInt64(dtoField.FieldValue) > checkField.MaxLength))
                            {
                                _logger.LogInformation("Cannot add field due to wrong input: {dtoField}", dtoField);
                                return BadRequest(MsgKeys.InvalidInputParameters);
                            }
                            else if ((fieldType == "SingleLineText" || fieldType == "MultiLineText") &&
                            ((dtoField.FieldValue.Length < checkField.MinLength || dtoField.FieldValue.Length > checkField.MaxLength)))
                            {
                                _logger.LogInformation("Cannot add field due to wrong input: {dtoField}", dtoField);
                                return BadRequest(MsgKeys.InvalidInputParameters);
                            }
                            else if (fieldType == "Dictionary") 
                            {
                                var dictionaryTypePossibleValues = _repositoryBopDictionary.Query(x => x.DictionaryTypeId == checkField.DictionaryTypeId)
                                                                                            .Select(x => x.Id).ToList();
                                string fieldValue = dto.ClientCompanyFieldValues.FirstOrDefault(d => d.Uilabel == checkField.Uilabel).FieldValue;
                                if (int.TryParse(fieldValue, out int selectedDictionaryValueId))
                                {
                                    if (dictionaryTypePossibleValues.Contains(selectedDictionaryValueId))
                                    {
                                        checkField.Company = null;
                                        checkField.DocumentClassFieldType = null;
                                        customFieldsToAdd.Add(checkField);
                                        customFieldsFromDb.Remove(checkField);
                                    }
                                    else
                                    {
                                        _logger.LogInformation("Cannot add field due to wrong input: {dtoField}", dtoField);
                                        return BadRequest(MsgKeys.InvalidDictionaryTypeParameters);
                                    }
                                }
                                else
                                {
                                    _logger.LogInformation("Cannot add field due to wrong input: {dtoField}", dtoField);
                                    return BadRequest(MsgKeys.InvalidDictionaryTypeParameters);
                                }
                               
                            }
                            else
                            {
                                checkField.Company = null;
                                checkField.DocumentClassFieldType = null;
                                customFieldsToAdd.Add(checkField);
                                customFieldsFromDb.Remove(checkField);
                            }
                        }
                        else
                        {
                            _logger.LogInformation("Cannot add field due to wrong company Id or custom fields: {dtoField}", dtoField);
                            return BadRequest(MsgKeys.ObjectNotExists);
                        }
                    }

                    //add required fields in logger which are not provided in post call nd throw 400
                    var getMandatoryFields = customFieldsFromDb.Where(d => d.IsMandatory == true).ToList();
                    if (getMandatoryFields.Count > 0)
                    {
                        _logger.LogInformation("Cannot add fields due to missing required custom fields: {getMandatoryFields}", getMandatoryFields);
                        return BadRequest(MsgKeys.RequiredFieldsAreNotProvided);
                    }


                    //saving custom fields
                    var customFieldMapping = _mapper.Map<List<CompanyCustomFieldDTO>>(customFieldsToAdd);
                    customFieldMapping.ForEach(s => s.FieldValue = dto.ClientCompanyFieldValues.FirstOrDefault(d => d.Uilabel == s.Uilabel).FieldValue);
                    // Add New  ClientCompanyFieldValues of this Client
                    SaveClientCompanyFieldValues(customFieldMapping, trans, entityClient);

                    dtoResult.ClientCompanyFieldValues = customFieldMapping;

                }
                if (dto.Tags != null)
                {
                    //storing list of Tags for Client
                    foreach (var tag in dto.Tags)
                    {
                        tag.ClientId = entityClient.Id;
                        var entityTag = _mapper.Map<ClientTag>(tag);
                        _repositoryClientTag.Insert(entityTag);
                        _repositoryClientTag.SaveChanges(_username, null, null);
                    }
                    dtoResult.Tags = dto.Tags;
                }


                await trans.CommitAsync();

                // log information
                _logger.LogInformation("Creating Client with client Id: {0}", dto.Id);
                
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
        /// Save Client Company Field Values
        /// </summary>
        /// <param name="dto"> Instance of ClientDTO </param>
        /// <param name="trans"> Instance of Transaction </param>
        /// <param name="entity"> Instance of Client </param>
        private void SaveClientCompanyFieldValues(List<CompanyCustomFieldDTO> customFieldList, ITransactionHandler trans, Client entity)
        {
            //This Function made for Save Client Company Field Values and use this
            // Function in Post and Put Call of Client

            foreach (CompanyCustomFieldDTO item in customFieldList)
            {
                var clientCompanyCustomFieldValue = new ClientCompanyCustomFieldValue
                {
                    ClientId = entity.Id,
                    DictionaryValueId = item.DictionaryValueId,
                    FieldId = item.Id,
                    RegisteredFieldValue = item.FieldValue,
                };

                _repositoryClientCompanyCustomFieldValue.Insert(clientCompanyCustomFieldValue);
                _repositoryClientCompanyCustomFieldValue.SaveChanges(_username, null, trans);
            }
        }
    }
}
