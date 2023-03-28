using AutoMapper;
using Intelli.DMS.Domain.Model;
using System.Threading.Tasks;
using Intelli.DMS.Domain.Model.Views;

namespace Intelli.DMS.Api.DTO.Mapping
{
    /// <summary>
    /// The mapping profile.
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MappingProfile"/> class.
        /// </summary>
        public MappingProfile()
        {
            // AlertDTO
            CreateMap<Domain.Model.Alert, AlertDTO>();
            CreateMap<AlertDTO, Domain.Model.Alert>();

            // BopConfigsDTO
            CreateMap<Domain.Model.BopConfig, BopConfigsDTO>();
            CreateMap<BopConfigsDTO, Domain.Model.BopConfig>();

            // CompanyDTO
            CreateMap<Domain.Model.Company, CompanyDTO>()
                .ForMember(dest => dest.UsersPerCompany, 
                            opt => opt.MapFrom(src => int.Parse(src.UsersPerCompany)));
            CreateMap<CompanyDTO, Domain.Model.Company>();

            // CompanyCustomFieldDTO
            CreateMap<Domain.Model.CompanyCustomField, CompanyCustomFieldDTO>()
            .ForMember(dest => dest.FieldTypeId, opt => opt.MapFrom(src => src.DocumentClassFieldTypeId));
            CreateMap<CompanyCustomFieldDTO, Domain.Model.CompanyCustomField>()
                .ForMember(dest => dest.DocumentClassFieldTypeId, opt => opt.MapFrom(src => src.FieldTypeId));
            
            // CompanyRepositoryDTO
            CreateMap<CompanyRepositoryDTO, Company>();
            CreateMap<Company, CompanyRepositoryDTO>();

            // RoleDTO
            CreateMap<SystemRole, RoleDTO>();
            CreateMap<RoleDTO, SystemRole>();

            //DocumrntTypeRoleDTO
            CreateMap<SystemRole, DocumrntTypeRoleDTO>();
            CreateMap<DocumrntTypeRoleDTO, SystemRole>();

            // RoleScreenDTO
            CreateMap<RoleScreen, RoleScreenDTO>()
                .ForMember(dest => dest.ScreenPriviliges, opt => opt.MapFrom(src => src.Privilege));
            CreateMap<RoleScreenDTO, RoleScreen>()
                .ForMember(dest => dest.Privilege, opt => opt.MapFrom(src => src.ScreenPriviliges))
                .ForMember(dest => dest.SystemRole, opt => opt.Ignore())
                .ForMember(dest => dest.Screen, opt => opt.Ignore());

            // RoleScreenElementDTO
            CreateMap<RoleScreenElement, RoleScreenElementDTO>()
                .ForMember(dest => dest.Priviliges, opt => opt.MapFrom(src => src.Privilege));
            CreateMap<RoleScreenElementDTO, RoleScreenElement>()
                .ForMember(dest => dest.Privilege, opt => opt.MapFrom(src => src.Priviliges))
                .ForMember(dest => dest.Role, opt => opt.Ignore())
                .ForMember(dest => dest.ScreenElement, opt => opt.Ignore());

            // ScreenElementDTO
            CreateMap<ScreenElement, ScreenElementDTO>();
            CreateMap<ScreenElementDTO, ScreenElement>();

            // ScreensDTO
            CreateMap<Screen, ScreensDTO>();
            CreateMap<ScreensDTO, Screen>();

            // UserPreferencesDTO
            CreateMap<ResourceLanguage, UserPreferencesDTO>();
            CreateMap<UserPreferencesDTO, ResourceLanguage>();

            // UserReadDTO
            CreateMap<SystemUser, UserReadDTO>();
            CreateMap<UserReadDTO, SystemUser>();

            // UserReadPrivilegesDTO
            CreateMap<SystemUser, UserReadPrivilegesDTO>();
            CreateMap<UserReadPrivilegesDTO, SystemUser>();

            // ClientContractDTO
            CreateMap<Client, ClientContractDTO>();
            CreateMap<ClientContractDTO, Client>();

            // SystemUserCompanyDTO
            CreateMap<Domain.Model.Country, CountryDTO>();
            CreateMap<CountryDTO, Domain.Model.Country>();

            // UserPreferencesDTO
            CreateMap<UserPreference, UserPreferencesDTO>();
            CreateMap<UserPreferencesDTO, UserPreference>();

            // DocumentsPerCompanyDTO
            CreateMap<DocumentsPerCompany, DocumentsPerCompanyDTO>();
            CreateMap<DocumentsPerCompanyDTO, DocumentsPerCompany>();

            // DocumentsPerCompanyDTO
            CreateMap<DocumentClassField, DocumentFieldValueDTO>();
            CreateMap<DocumentFieldValueDTO, DocumentClassField>();

            // ConfigurationDto
            CreateMap<Configuration, ConfigurationDto>();
            CreateMap<ConfigurationDto, Configuration>();

            // DocumentType
            CreateMap<DocumentType, DocumentTypeDTO>();
            CreateMap<DocumentTypeDTO, DocumentType>();

            // DocumentClassDTO
            CreateMap<DocumentClass, DocumentClassDTO>();
            CreateMap<DocumentClassDTO, DocumentClass>();

            // DocumentClassDTO
            CreateMap<BatchMetaDTO, DocumentClassFieldContractDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.DocumentClassFieldId))
                .ForMember(dest => dest.DocumentClassValue, opt => opt.MapFrom(src => src.FieldValue));
           
            CreateMap<DocumentClassFieldContractDTO, BatchMetaDTO>()
                .ForMember(dest => dest.DocumentClassFieldId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FieldValue, opt => opt.MapFrom(src => src.DocumentClassValue));
            

            // DocumentClassContractDTO
            CreateMap<DocumentClass, DocumentClassContractDTO>();
            CreateMap<DocumentClassContractDTO, DocumentClass>();

            // DocumentClassContractsDTO
            CreateMap<DocumentClass, DocumentClassContractsDTO>();
            CreateMap<DocumentClassContractsDTO, DocumentClass>();

            // DocumentClassFieldContractDTO
            CreateMap<DocumentClassField, DocumentClassFieldContractDTO>();
            CreateMap<DocumentClassFieldContractDTO, DocumentClassField>();

            // DocumentClassFieldTypeDTO
            CreateMap<DocumentClassFieldType, DocumentClassFieldTypeDTO>();
            CreateMap<DocumentClassFieldTypeDTO, DocumentClassFieldType>();

            // DocumentClassField base class dto
            CreateMap<DocumentClassField, DocumentClassFieldBaseDTO>();
            CreateMap<DocumentClassFieldBaseDTO, DocumentClassField>();

            // DocumentClassFieldCodeDTO

            CreateMap<DocumentClassField, DocumentClassFieldCodeDTO>();
            CreateMap<DocumentClassFieldCodeDTO, DocumentClassField>();

            // DocumentClassField dto for doc class fields
            CreateMap<DocumentClassField, DocumentClassFieldDTO>();
            CreateMap<DocumentClassFieldDTO, DocumentClassField>();

            // DocumentClassField dto for updated document response
            CreateMap<DocumentClassField, UpdatedFieldDTO>();
            CreateMap<UpdatedFieldDTO, DocumentClassField>();

            // Document checked out dto
            CreateMap<DocumentsCheckedOut, DocumentsCheckedOutDTO>();
            CreateMap<DocumentsCheckedOutDTO, DocumentsCheckedOut>();

            // Document checked out log dto
            CreateMap<DocumentsCheckedOutLog, DocumentsCheckedOutLogDTO>();
            CreateMap<DocumentsCheckedOutLogDTO, DocumentsCheckedOutLog>();


            // DocumentClassField dto for doc class
            CreateMap<DocumentClassField, DocumentClassAllFieldsDTO>();
            CreateMap<DocumentClassAllFieldsDTO, DocumentClassField>();

            //batch base class DTO
            CreateMap<Batch, BatchDTO>();
            CreateMap<BatchDTO, Batch>();

            //batch DTO for repository
            CreateMap<Batch, ClientRepositoryViewDTO>();
            CreateMap<ClientRepositoryViewDTO, Batch>();

            //batch meta base DTO
            CreateMap<BatchMetum, BatchMetaBaseDTO>();
            CreateMap<BatchMetaBaseDTO, BatchMetum>();

            //batch meta DTO
            CreateMap<BatchMetum, BatchMetaDTO>();
            CreateMap<BatchMetaDTO, BatchMetum>();

            //Batch Meta Base DTO and ClientCompanyCustomFieldValue
            CreateMap<ClientCompanyCustomFieldValue, BatchMetaBaseDTO>()
                .ForMember(dest => dest.FieldValue, opt => opt.MapFrom(src => src.RegisteredFieldValue))
                .ForMember(dest => dest.DocumentClassFieldId, opt => opt.MapFrom(src => src.FieldId))
                .ForMember(dest => dest.DictionaryValueId, opt => opt.MapFrom(src => src.DictionaryValueId));

            CreateMap<BatchMetaBaseDTO, ClientCompanyCustomFieldValue>()
                .ForMember(dest => dest.RegisteredFieldValue, opt => opt.MapFrom(src => src.FieldValue))
                .ForMember(dest => dest.FieldId, opt => opt.MapFrom(src => src.DocumentClassFieldId))
                .ForMember(dest => dest.DictionaryValueId, opt => opt.MapFrom(src => src.DictionaryValueId));


            //batch meta DTO for update fields on document check out
            CreateMap<BatchMetum, BatchMetaCheckOutDTO>();
            CreateMap<BatchMetaCheckOutDTO, BatchMetum>();

            //batch meta DTO for updated document response
            CreateMap<BatchMetum, UpdatedFieldValuesDTO>();
            CreateMap<UpdatedFieldValuesDTO, BatchMetum>();

            //customer meta DTO
            CreateMap<Client, ClientDTO>();
            CreateMap<ClientDTO, Client>();

            //customer Add DTO
            CreateMap<Client, ClientAddDTO>();
            CreateMap<ClientAddDTO, Client>();
            
            //customer meta DTO
            CreateMap<ClientView, ClientDTO>();
            CreateMap<ClientDTO, ClientView>();

            //DocumentSearchView DTO
            CreateMap<DocumentSearchView, DocumentSearchViewDTO>();
            CreateMap<DocumentSearchViewDTO, DocumentSearchView>();

            //ContractSearchView DTO
            CreateMap<ContractSearchView, ContractSearchViewDTO>();
            CreateMap<ContractSearchViewDTO, ContractSearchView>();

            //bop dictionary DTO
            CreateMap<BopDictionary, BopDictionaryDTO>();
            CreateMap<BopDictionaryDTO, BopDictionary>();

            // dictionary type DTO
            CreateMap<DictionaryType, DictionaryTypeDTO>();
            CreateMap<DictionaryTypeDTO, DictionaryType>();

            // DocumentReviewView
            CreateMap<DocumentReviewView, DocumentReviewPaginationDTO>();
            CreateMap<DocumentReviewPaginationDTO, DocumentReviewView>();

            // DocumentClassField
            CreateMap<DocumentClassField, DocumentClassFieldDTO>();
            CreateMap<DocumentClassFieldDTO, DocumentClassField>();

            // ClientRepositoryView
            CreateMap<ClientRepositoryView, ClientRepositoryViewDTO>();
            CreateMap<ClientRepositoryViewDTO, ClientRepositoryView>();

            // ClientTag
            CreateMap<ClientTag, ClientTagDTO>();
            CreateMap<ClientTagDTO, ClientTag>();

            // Logging
            CreateMap<Nlog, SystemLogDTO>();
            CreateMap<SystemLogDTO, Nlog>();

            // DMSOutLookAddInTempFileDTO
            CreateMap<DMSOutLookAddInTempFile, DMSOutLookAddInTempFileDTO>();
            CreateMap<DMSOutLookAddInTempFileDTO, DMSOutLookAddInTempFile>();
        }
    }
}
