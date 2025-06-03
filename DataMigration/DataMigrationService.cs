using Intelli.DMS.Domain.Database;
using Intelli.DMS.Domain.Model;
using Intelli.DMS.Domain.OLDDBModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using Intelli.DMS.Api.Services.DataMigration.DataModel;
using NLog;
using DataMigration.DocumentUpload.Impl;
using DataMigration.DocumentUpload;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using Intelli.DMS.Api.Helpers;
using AutoMapper;

namespace DataMigration
{
    public class DataMigrationService
    {
        DMSOLDDBContext _dMSOLDDBContext;
        DataMigrationConfiguraton _dataMigrationConfiguraton;
        IDocumentUploadService _documentUploadService;
        public static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        public DMSContext _context;
        public IMapper mapper;
        public DataMigrationService(DataMigrationConfiguraton dataMigrationConfiguraton,
                                    string documentsSaveToRootPath,
                                    string dMSconnectionString)
        {
            _dataMigrationConfiguraton = dataMigrationConfiguraton;
            _documentUploadService = new DocumentUploadService(documentsSaveToRootPath, dMSconnectionString);
            InitializeAutoMapper();
        }

        /// <summary>
        /// DataMigration Start Async
        /// </summary>
        /// <returns></returns>
        public void DataMigrationStart(List<string> allDataMigrationHistoriesTables,
                                       string connectionString,
                                       string dMSconnectionString)
        {
            _context = new DMSContext(dMSconnectionString);
            _dMSOLDDBContext = new DMSOLDDBContext(connectionString);

            //Check Company TableName Exists in Data Migration Histories 
            if (!allDataMigrationHistoriesTables.Contains(DataMigrationConstants.Company))
            {
                Logger.Info("Migrate Company Data Start ...");
                MigrateCompanyData();
                Logger.Info("Migrate Company Data End ...");
            }
            //Check Role TableName Exists in Data Migration Histories 
            if (!allDataMigrationHistoriesTables.Contains(DataMigrationConstants.Role))
            {
                Logger.Info("Migrate Role Data Start ...");
                MigrateRoleData();
                Logger.Info("Migrate Role Data End ...");
            }
            //Check User TableName Exists in Data Migration Histories 
            if (!allDataMigrationHistoriesTables.Contains(DataMigrationConstants.User))
            {
                Logger.Info("Migrate User Data Start ...");
                MigrateUserData();
                Logger.Info("Migrate User Data End ...");
            }

            //Check Company TableName Exists in Data Migration Histories 
            if (!allDataMigrationHistoriesTables.Contains(DataMigrationConstants.CompanyField))
            {
                Logger.Info("Company Custom Field Insertion Start ...");
                CompanyCustomFieldAdded();
                Logger.Info("Company Custom Field Insertion End ...");
            }
            //Check Client TableName Exists in Data Migration Histories 
            if (!allDataMigrationHistoriesTables.Contains(DataMigrationConstants.Client))
            {
                Logger.Info("Migrate Clients Data Start ...");
                MigrateClientsData();
                Logger.Info("Migrate Clients Data End ...");
            }
            //Check BatchItem TableName Exists in Data Migration Histories 
            if (!allDataMigrationHistoriesTables.Contains(DataMigrationConstants.BatchItem))
            {
                Logger.Info("Migrate Batch Item Data Start ...");
                MigrateBatchItemData();
                Logger.Info("Migrate Batch " +
                    "Item Data End ...");
            }
        }

        private void MigrateUserData()
        {
            var checkuserExsists = _context.SystemUsers.Where(x => x.Email == "super@mailinator.com" || x.Id == _dataMigrationConfiguraton.UserId).FirstOrDefault();

            if (checkuserExsists == null)
            {
                string commandText = @$"
                                SET IDENTITY_INSERT [dbo].[SystemUsers] ON
                                  INSERT [dbo].[SystemUsers] ([Id], [FullName], [Email], [Jmbg], [IsActive], [CreatedAt], [UpdatedAt]) VALUES ({_dataMigrationConfiguraton.UserId}, N'super', N'super@mailinator.com', NULL, 1, 0, 1651513307)
                                SET IDENTITY_INSERT [dbo].[SystemUsers] OFF
                                  
                                INSERT [dbo].[AspNetUsers] ([Id], [SystemUserId], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'7a442ad8-e27a-4f65-b021-71883b1e7da7', 1, N'super@mailinator.com', N'SUPER@MAILINATOR.COM', N'super@mailinator.com', N'SUPER@MAILINATOR.COM', 1, N'AQAAAAEAACcQAAAAEI6lbgmfmQCSdBv9lhwc93eBfL1sSQpdFdUryvGKxC3W9+el7IwEfju1m2WjpfWwVQ==', N'U7IA3KPURJV4FNS2AGY35XQ7TPCNENA3', N'5e1059d2-c621-492a-b6ed-eaca5c6df851', NULL, 0, 0, NULL, 1, 0)
                                
                                  SET IDENTITY_INSERT [dbo].[SystemUserRole] ON
                                INSERT INTO [dbo].[SystemUserRole]
                                           ([Id]
                                           ,[SystemUserId]
                                           ,[SystemRoleId]
                                           ,[IsActive]
                                           ,[CreatedAt]
                                           ,[UpdatedAt])
                                     VALUES
                                           ({_dataMigrationConfiguraton.UserId}
                                		   ,{_dataMigrationConfiguraton.RoleId}
                                           ,1
                                           ,1
                                           ,0
                                           ,0)
                                SET IDENTITY_INSERT [dbo].[SystemUserRole] OFF";
                _context.Database.ExecuteSqlRaw(commandText);
            }

            DataMigrationHistory dataMigrationHistory = new()
            {
                TableName = DataMigrationConstants.User,
                CreatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                IsActive = true
            };

            _context.DataMigrationHistories.Add(dataMigrationHistory);
            _context.SaveChanges();
            Logger.Info($"Data Migration History Added With TableName ==> {dataMigrationHistory.TableName}");

        }

        private void MigrateRoleData()
        {
            var checkRoleExsists = _context.SystemRoles.Where(x => x.Id == _dataMigrationConfiguraton.RoleId).FirstOrDefault();

            if (checkRoleExsists == null)
            {
                string commandText = @$"
                            SET IDENTITY_INSERT [dbo].[SystemRoles] ON 
                            
                            INSERT [dbo].[SystemRoles] ([Id], [Name], [IsActive], [CreatedAt], [UpdatedAt], [CompanyId], [Priority]) VALUES ({_dataMigrationConfiguraton.RoleId}, N'Super Admin', 1, 1604403101, 1651137657, 1, 1)
                            SET IDENTITY_INSERT [dbo].[SystemRoles] OFF 
                            
                            INSERT [dbo].[RoleScreens] ([SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES ({_dataMigrationConfiguraton.RoleId}, 1, 2, 1, 1647857717, 1647857717)
                            INSERT [dbo].[RoleScreens] ([SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES ({_dataMigrationConfiguraton.RoleId}, 2, 2, 1, 1647857717, 1647857717)
                            INSERT [dbo].[RoleScreens] ([SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES ({_dataMigrationConfiguraton.RoleId}, 3, 2, 1, 1647857717, 1647857717)
                            INSERT [dbo].[RoleScreens] ([SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES ({_dataMigrationConfiguraton.RoleId}, 4, 2, 1, 1647857717, 1647857717)
                            INSERT [dbo].[RoleScreens] ([SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES ({_dataMigrationConfiguraton.RoleId}, 5, 2, 1, 1647857717, 1647857717)
                            INSERT [dbo].[RoleScreens] ([SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES ({_dataMigrationConfiguraton.RoleId}, 6, 2, 1, 1647857717, 1647857717)
                            INSERT [dbo].[RoleScreens] ([SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES ({_dataMigrationConfiguraton.RoleId}, 7, 2, 1, 1647857717, 1647857717)
                            INSERT [dbo].[RoleScreens] ([SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES ({_dataMigrationConfiguraton.RoleId}, 8, 2, 1, 1647857717, 1647857717)
                            INSERT [dbo].[RoleScreens] ([SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES ({_dataMigrationConfiguraton.RoleId}, 9, 2, 1, 1647857717, 1647857717)
                            INSERT [dbo].[RoleScreens] ([SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES ({_dataMigrationConfiguraton.RoleId}, 10, 2, 1, 1647857717, 1647857717)
                            INSERT [dbo].[RoleScreens] ([SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES ({_dataMigrationConfiguraton.RoleId}, 11, 2, 1, 1647857717, 1647857717)
                            INSERT [dbo].[RoleScreens] ([SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES ({_dataMigrationConfiguraton.RoleId}, 12, 2, 1, 1647857717, 1647857717)
                            INSERT [dbo].[RoleScreens] ([SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES ({_dataMigrationConfiguraton.RoleId}, 13, 2, 1, 0, 0)
                            INSERT [dbo].[RoleScreens] ([SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES ({_dataMigrationConfiguraton.RoleId}, 14, 2, 1, 1647857717, 1647857717)
                            INSERT [dbo].[RoleScreens] ([SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES ({_dataMigrationConfiguraton.RoleId}, 15, 1, 1, 1651137640, 1651137640)
                            INSERT [dbo].[RoleScreens] ([SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES ({_dataMigrationConfiguraton.RoleId}, 17, 2, 1, 1647857717, 1647857717)
                            INSERT [dbo].[RoleScreens] ([SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES ({_dataMigrationConfiguraton.RoleId}, 18, 2, 1, 1647857717, 1647857717)
                            INSERT [dbo].[RoleScreens] ([SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES ({_dataMigrationConfiguraton.RoleId}, 20, 2, 1, 1647857717, 1647857717)
                            INSERT [dbo].[RoleScreens] ([SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES ({_dataMigrationConfiguraton.RoleId}, 21, 2, 1, 1647857717, 1647857717)
                            INSERT [dbo].[RoleScreens] ([SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES ({_dataMigrationConfiguraton.RoleId}, 22, 1, 1, 1651137640, 1651137650)
                            INSERT [dbo].[RoleScreens] ([SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES ({_dataMigrationConfiguraton.RoleId}, 23, 1, 1, 1651137640, 1651137660)


                                       ";
                _context.Database.ExecuteSqlRaw(commandText);
            }

            DataMigrationHistory dataMigrationHistory = new()
            {
                TableName = DataMigrationConstants.Role,
                CreatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                IsActive = true
            };

            _context.DataMigrationHistories.Add(dataMigrationHistory);
            _context.SaveChanges();
            Logger.Info($"Data Migration History Added With TableName ==> {dataMigrationHistory.TableName}");

        }

        private void MigrateCompanyData()
        {

            var comapnyWithId_1 = _context.Companies.Where(x => x.Id == _dataMigrationConfiguraton.CompanyId)
                                                         .FirstOrDefault();
            if (comapnyWithId_1 == null)
            {
                Company company = new Company();
                company.Id = _dataMigrationConfiguraton.CompanyId;
                company.CompanyName = "InteliSoft";
                company.CompanyCode = "int";
                company.GdprdaysToBeKept = 5;
                company.Email = "int-sol@mailinator.com";
                company.IsActive = true;
                company.CreatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                company.UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                company.UsersPerCompany = EncryptionHelper.EncryptString("10");

                using var transaction = _context.Database.BeginTransactionAsync().Result;
                SetIdentityInsertAsync<Company>(true).Wait();
                _context.Add(company);
                _context.SaveChanges();
                SetIdentityInsertAsync<Company>(false).Wait();
                transaction.CommitAsync().Wait();

            }
            else
            {
                comapnyWithId_1.CompanyName = "InteliSoft";
                comapnyWithId_1.CompanyCode = "int";
                comapnyWithId_1.GdprdaysToBeKept = 5;
                comapnyWithId_1.Email = "int-sol@mailinator.com";
                comapnyWithId_1.IsActive = true;
                comapnyWithId_1.CreatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                comapnyWithId_1.UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                comapnyWithId_1.UsersPerCompany = EncryptionHelper.EncryptString("10");

                _context.Update(comapnyWithId_1);
                _context.SaveChanges();

            }
            DataMigrationHistory dataMigrationHistory = new()
            {
                TableName = DataMigrationConstants.Company,
                CreatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                IsActive = true
            };

            _context.DataMigrationHistories.Add(dataMigrationHistory);
            _context.SaveChanges();
            Logger.Info($"Data Migration History Added With TableName ==> {dataMigrationHistory.TableName}");


        }

        private async Task SetIdentityInsertAsync<TEnt>(bool enable)
        {
            var entityType = _context.Model.FindEntityType(typeof(TEnt));
            var value = enable ? "ON" : "OFF";
            string query = $"SET IDENTITY_INSERT {entityType.GetSchema()}.{entityType.GetTableName()} {value}";
            await _context.Database.ExecuteSqlRawAsync(query);
        }
        private void MigrateBatchItemData()
        {
            var checkDataMigrationRecordHistoryRecordIds = _context.DataMigrationReordHistories
                                                             ?.Where(x => x.TableName == DataMigrationConstants.BatchItem)
                                                             ?.Select(x => x.RecordId)
                                                             ?.ToList();
            var checkDataMigrationRecordHistory = new DataMigrationReordHistory();
            if (checkDataMigrationRecordHistoryRecordIds == null)
            {
                checkDataMigrationRecordHistory = _context.DataMigrationReordHistories
                                                              ?.Where(x => x.TableName == DataMigrationConstants.BatchItem &&
                                                                           x.RecordId == checkDataMigrationRecordHistoryRecordIds.Max()).FirstOrDefault();

            }
            var oldBatchItems = new List<Filing>();
            if (checkDataMigrationRecordHistory?.TableName == null)
            {
                oldBatchItems = _dMSOLDDBContext.Filings.OrderBy(x => x.FilingId).ToList();
            }
            else
            {
                oldBatchItems = _dMSOLDDBContext.Filings.OrderBy(x => x.FilingId)
                                                    .Where(x => x.FilingId > checkDataMigrationRecordHistory.RecordId)
                                                    .ToList();
            }


            foreach (var item in oldBatchItems)
            {
                IFormFile formFile = Download(item.FilingFolder + "/" + item.FilingPathname, out string localsavePath);
                int documentClassId = GetDocumentClassId(item.FilingCategory);
                int clientId = GetClientId(item.FilingXacode);
                int DocumentClassFieldId = GetDocumentClassFieldId(documentClassId);
                String fieldvalues = "[{\"DocumentClassFieldId\":" + DocumentClassFieldId + ",\"FieldValue\":\"ok\"}]";

                _documentUploadService.DocumentUpload(formFile,
                                                      documentClassId,
                                                      clientId,
                                                      _dataMigrationConfiguraton.CompanyId,
                                                      fieldvalues,
                                                      _dataMigrationConfiguraton.UserName,
                                                      _dataMigrationConfiguraton.UserId,
                                                      localsavePath);

                DataMigrationReordHistory dataMigrationReordHistory = new DataMigrationReordHistory();
                dataMigrationReordHistory.TableName = DataMigrationConstants.BatchItem;
                dataMigrationReordHistory.RecordId = Convert.ToDecimal(item.FilingId);
                _context.DataMigrationReordHistories.Add(dataMigrationReordHistory);
                _context.SaveChanges();

            }
            var value = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            DataMigrationHistory dataMigrationHistory = new()
            {
                TableName = DataMigrationConstants.BatchItem,
                CreatedAt = value,
                UpdatedAt = value,
                IsActive = true
            };

            _context.DataMigrationHistories.Add(dataMigrationHistory);
            _context.SaveChanges();
            Logger.Info($"Data Migration History Added With TableName ==> {dataMigrationHistory.TableName}");


        }

        private int GetDocumentClassFieldId(int documentClassId)
        {
            return _context.DocumentClassFields.Where(x => x.DocumentClassId == documentClassId).FirstOrDefault().Id;

        }

        private int GetClientId(decimal? filingXacode)
        {
            return _context.Clients.Where(x => x.ExternalId == filingXacode.ToString()).FirstOrDefault().Id;

        }

        private int GetDocumentClassId(int? filingCategory)
        {
            return _context.DocumentClasses.Where(x => x.DocumentClassCode == filingCategory.ToString()).FirstOrDefault().Id;
        }

        public IFormFile Download(string filePathName, out string localsavePath)
        {
            string filePath = _dataMigrationConfiguraton.SourceBaseUrl.TrimEnd('/') + "/" + filePathName;
            try
            {
                if (filePathName == null)
                {
                    throw new ArgumentNullException(nameof(filePathName));
                }
                //filePath = $"{DMSDataMigrationConstants.BaseUrlOfFileServer}" +
                //              @"\FileServer\SharePath\" + filePathName;

                var saveTopathDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                var savePath = Path.Combine(saveTopathDirectory, Path.GetFileName(filePath));

                //If Current Document Version Directory does Not Exists Create It 
                if (!Directory.Exists(saveTopathDirectory)) Directory.CreateDirectory(saveTopathDirectory);

                using (WebClient webClient = new WebClient())
                {
                    webClient.DownloadFile(
                        new Uri(filePath),
                        // Param2 = Path to save
                        $"{savePath}"
                    );
                }


                var memory = new MemoryStream();
                using (var stream = new FileStream(savePath, FileMode.Open))
                {
                    stream.CopyToAsync(memory).Wait();
                }
                memory.Position = 0;
                localsavePath = savePath;
                return new FormFile(memory, 0, memory.Length, Path.GetFileName(filePath), $"{Path.GetFileName(filePath)}{Path.GetExtension(filePath)}");
            }
            catch (Exception ex)
            {
                Logger.Error($"File Not Downloaded of File Path ==> {filePath} Exception :: {ex.Message}");
                throw;
            }
        }

        private async void MigrateClientsData()
        {
            try
            {
                var oldClients = _dMSOLDDBContext.PelaXrs.OrderBy(x => x.PelXaas).ToList();


                var value = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                List<ClientCompanyCustomFieldValue> clientCompanyCustomFields;
                List<Client> clients = mapper.Map<List<Client>>(oldClients);
                using (var trans = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _context.Clients.BulkInsert(clients);

                        Logger.Info($"Clients inserted. Total clients added are :: {clients.Count}");
                        Logger.Info("Start mapping company custom fields on basis of client's Id, to insert PelProf " +
                            "as a custom field.");

                        clientCompanyCustomFields = oldClients.Join(clients,
                            oclient => oclient.PelXaas.ToString(),
                            nclient => nclient.ExternalId,
                            (oclient, nclient) => new ClientCompanyCustomFieldValue
                            {
                                FieldId = _dataMigrationConfiguraton.CompanyCustomeFieldId,
                                ClientId = nclient.Id,
                                RegisteredFieldValue = oclient.PelProf,
                                CreatedAt = value,
                                UpdatedAt = value,
                                IsActive = true
                            }).ToList();

                        Logger.Info("Mapping completed. Now start insertion of clientCompanyFields to database");

                        _context.ClientCompanyCustomFieldValues.BulkInsert(clientCompanyCustomFields);

                        Logger.Info($"Company Custom Fields added successfully.");
                        _context.SaveChanges();
                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        Logger.Error($"Error on bulk insertion of clients and there custom field\n" +
                            $"Message :: {ex.Message} \n Exception :: {ex}");
                        trans.Rollback();
                        throw;
                    }
                }

                DataMigrationHistory dataMigrationHistory = new()
                {
                    TableName = DataMigrationConstants.Client,
                    CreatedAt = value,
                    UpdatedAt = value,
                    IsActive = true
                };

                _context.DataMigrationHistories.Add(dataMigrationHistory);
                _context.SaveChanges();
                Logger.Info($"Data Migration History Added With TableName ==> {dataMigrationHistory.TableName}");


            }
            catch (Exception ex)
            {
                Logger.Error($"Error on migrating clients. \n Message :: {ex.Message} \n Exception :: {ex}");
                throw;
            }

        }

        public void CompanyCustomFieldAdded()
        {
            try
            {
                var value = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                var comcustfield = _context.CompanyCustomFieldes.Where(x => x.Id == 1).FirstOrDefault();

                if (comcustfield == null)
                {
                    CompanyCustomField companyCustomField = new()
                    {
                        Id = 1,
                        CompanyId = _dataMigrationConfiguraton.CompanyId,
                        Uilabel = _dataMigrationConfiguraton.CompanyCustomFieldUiLabel,
                        DocumentClassFieldTypeId = _dataMigrationConfiguraton.CompanyCustomeFieldTypeId,
                        IsMandatory = false,
                        UISort = 1,
                        CreatedAt = value,
                        UpdatedAt = value,
                        IsActive = true
                    };

                    //Add New CompanyCustomFielde in CompanyId = 1 
                    using var transaction = _context.Database.BeginTransactionAsync().Result;
                    SetIdentityInsertAsync<CompanyCustomField>(true).Wait();
                    _context.CompanyCustomFieldes.Add(companyCustomField);
                    _context.SaveChanges();
                    SetIdentityInsertAsync<CompanyCustomField>(false).Wait();
                    transaction.CommitAsync().Wait();

                    Logger.Info($"Company Custom Field Added With Data ==> CompanyId = {companyCustomField.CompanyId}");
                }

                DataMigrationHistory dataMigrationHistory = new()
                {
                    TableName = DataMigrationConstants.CompanyField,
                    CreatedAt = value,
                    UpdatedAt = value,
                    IsActive = true
                };

                // Add Company TableName in Data Migration History
                _context.DataMigrationHistories.Add(dataMigrationHistory);
                _context.SaveChanges();
                Logger.Info($"Data Migration History Added With TableName ==> {dataMigrationHistory.TableName}");

            }
            catch (Exception)
            {
                throw;
            }
        }


        private void InitializeAutoMapper()
        {
            var value = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var mappeConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PelaXr, Client>()
                .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.PelXaas))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.PelOnom))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.PelEpon))
                .ForMember(dest => dest.JMBG, opt => opt.MapFrom(src => src.Pel_Idno))
                .ForMember(dest => dest.CompanyId, opt => opt.NullSubstitute(_dataMigrationConfiguraton.CompanyId))
                .ForMember(dest => dest.CreatedAt, opt => opt.NullSubstitute(value))
                .ForMember(dest => dest.UpdatedAt, opt => opt.NullSubstitute(value))
                .ForMember(dest => dest.IsActive, opt => opt.NullSubstitute(true))
                ;
            });
            mapper = mappeConfig.CreateMapper();
        }
    }
}
