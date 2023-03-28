using AutoMapper;
using Intelli.DMS.Api.DTO;
using Intelli.DMS.Api.Helpers;
using Intelli.DMS.Domain.Core.Repository;
using Intelli.DMS.Domain.Database;
using Intelli.DMS.Domain.Model.Views;
using Intelli.DMS.Shared.DTO;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NLog;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Timers;

namespace Intelli.DMS.ApplyGDPR
{
    public partial class ApplyGDPRService : ServiceBase
    {
        private readonly Logger _logger;
        private readonly DMSContext _context;
        public ApplyGDPRService()
        {
            string jsonFilePath = $"{Directory.GetCurrentDirectory().Replace("\\Intelli.DMS.ApplyGDPR\\bin\\Debug\\net5.0", "")}";
            // Comment this line before publish the project
            jsonFilePath = jsonFilePath + "\\Intelli.DMS.Api";
            var builder = new ConfigurationBuilder()
             .SetBasePath(jsonFilePath)
             .AddJsonFile("appsettings.json", optional: false);

            IConfiguration config = builder.Build();

            SqlConnectionConfigurationDTO configDTO = config.GetSection("DBConnections:DMSDB")
                                                                .Get<SqlConnectionConfigurationDTO>();
            string DMSConnectionString = SqlConnectionHelper.ToConnectionString(configDTO);

            DMSContext context = new(DMSConnectionString);
            _context = context;
            InitializeComponent();

            _logger = LogManager.GetCurrentClassLogger();
        }
        private void InitializeComponent()
        {
            this.ServiceName = "ApplyGDPRService";
        }

        protected override void OnStart(string[] args)
        {
            _logger.Log(NLog.LogLevel.Info, "GDPR service STARTED");

            try
            {
                int pollingIntervalSeconds = 5;

                // Set up a timer for polling.
                Timer timer = new()
                {
                    Interval = pollingIntervalSeconds * 1000 // convert to milliseconds
                };
                timer.Elapsed += new ElapsedEventHandler(this.OnTimer);
                timer.Start();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex);
            }
        }

        protected override void OnStop()
        {
            _logger.Log(LogLevel.Info, "GDPR service STOPED");
        }

        public void OnTimer(object sender, ElapsedEventArgs args)
        {
            ((Timer)sender).Stop();

            try
            {
                _logger.Log(LogLevel.Info, "GDPR service STOPED");

                ApplyGDPRCompliance();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex);
            }

            ((Timer)sender).Start();
        }

        private void ApplyGDPRCompliance()
        {
            var query = _context.ApplyGDPR.FromSqlInterpolated($"EXEC dbo.sp_ApplyGDPR");
            var data = query.ToList();

            //logging query result
            foreach (var item in data)
            {
                _logger.Log(LogLevel.Info, "GDPR applied in data "+ item);
            }
        }

    }
}
