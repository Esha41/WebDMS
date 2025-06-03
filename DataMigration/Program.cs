using Intelli.DMS.Domain.Database;
using Intelli.DMS.Domain.OLDDBModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using NLog;
using Microsoft.Extensions.Configuration;
using System.IO;
using Intelli.DMS.Shared.DTO;
using Intelli.DMS.Api.Helpers;

namespace DataMigration
{
    public class Program
    {
        public static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        private static void Main(string[] args)
        {
        Start:
            try
            {
                string jsonFilePath = $"{Directory.GetCurrentDirectory().Replace("\\DataMigration\\bin\\Debug\\net5.0", "")}";

                //jsonFilePath = jsonFilePath + "\\Intelli.DMS.Api"; //Uncomment this to run in debug mode or in VisualStudio
                var builder = new ConfigurationBuilder()
                 .SetBasePath(jsonFilePath)
                 .AddJsonFile("appsettings.json", optional: false);

                IConfiguration config = builder.Build();

                var dataMigrationConfiguraton = config.GetSection("DataMigrationConfiguraton")
                                           .Get<DataMigrationConfiguraton>();

                var documentsSaveToRootPath = config.GetSection("DocumentUploadPath")
                                           .Get<string>();
                SqlConnectionConfigurationDTO configDTO = config.GetSection("DBConnections:DMSDB")
                                                                .Get<SqlConnectionConfigurationDTO>();
                string DMSConnectionString = SqlConnectionHelper.ToConnectionString(configDTO);
                string connectionString = $"Server={dataMigrationConfiguraton.SourceServerName};" +
                                         $"Database={dataMigrationConfiguraton.SourceDatabaseName};" +
                                         $"User Id={dataMigrationConfiguraton.SourceUserName};" +
                                         $"Password={dataMigrationConfiguraton.SourcePassWord};";

                Logger.Info(" Checking Database Connection Starts .... ");
                DMSOLDDBContext dMSOLDDBContext = new DMSOLDDBContext(connectionString);
                dMSOLDDBContext.Database.OpenConnection();
                dMSOLDDBContext.Database.CloseConnection();
                Logger.Info(" Checking Database Connection Ends .... ");
                if (dataMigrationConfiguraton.CheckSchema && !AreRequiredTablesExist(dMSOLDDBContext))
                {
                    Logger.Info("\nRequired tables are not exist. Please check the above expception.\n");
                    Console.ReadKey();
                    Environment.Exit(0);
                }
                Console.WriteLine(" Enter 1 To Start Data Migration and Any Other Number For Exit .... ");
                int Option = Convert.ToInt32(Console.ReadLine());
                Logger.Info(" Data Migration Starts .... ");

                if (Option == 1)
                {
                    DataMigrationStarting(connectionString, dataMigrationConfiguraton, documentsSaveToRootPath, DMSConnectionString);
                }
            }
            catch (Exception ex)
            {
                string logMessage = $"{ex.Message}: {ex}";
                Logger.Error(logMessage);
                Console.ReadKey();
                goto Start;
            }

        }

        private static void DataMigrationStarting(string connectionString,
                                                  DataMigrationConfiguraton dataMigrationConfiguraton,
                                                  string documentsSaveToRootPath,
                                                  string DMSconnectionString)
        {
            DataMigrationService dataMigrationService = new DataMigrationService(dataMigrationConfiguraton, documentsSaveToRootPath, DMSconnectionString);
            DMSContext _context = new DMSContext(DMSconnectionString);

            var tableNames = _context.DataMigrationHistories.ToList().Select(x => x.TableName).ToList();
            dataMigrationService.DataMigrationStart(tableNames, connectionString, DMSconnectionString);

            Logger.Info(" Data Migration Ends .... ");
            Console.WriteLine(" Press Any key To Exit ... ");
            Console.ReadKey();
        }
        private static bool AreRequiredTablesExist(DMSOLDDBContext dMSOLDDBContext)
        {
            bool areExist = true;
            try
            {
                dMSOLDDBContext.PelaXrs.Any();
                dMSOLDDBContext.FilingCategories.Any();
                dMSOLDDBContext.Filings.Any();
            }
            catch (Exception ex)
            {
                Logger.Info($"Error while checking existence of tables. \nMessage :: {ex.Message}" +
                    $"\nError :: ${ex}");
                areExist = false;
            }
            return areExist;
        }
    }

    public class MyFirstClass
    {
        public string Option1 { get; set; }
        public int Option2 { get; set; }
    }

    public class MySecondClass
    {
        public string SettingOne { get; set; }
        public int SettingTwo { get; set; }
    }

}
