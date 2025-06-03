using AutoMapper;
using Intelli.DMS.Api.Services.Email.Impl;
using Intelli.DMS.Domain.Core.Repository;
using Intelli.DMS.Domain.Database;
using Intelli.DMS.Domain.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Linq;


namespace Intelli.DMS.Api.Services.BopConfig.Impl
{
    public class BopConfigService :IBopConfigService
    {
        private readonly IRepository<Domain.Model.BopConfig> _repositoryBopConfig;
        private IConfiguration Configuration { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BopConfigService"/> class.
        /// </summary>
        /// <param name="context">Instance of <see cref="DMSContext"/> will be injected</param>
        /// <param name="logger">Instance of <see cref="ILogger"/> will be injected</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/> will be injected</param>
        /// <param name="accessor">Instance of <see cref="IHttpContextAccessor"/> will be injected</param>
        /// 
        public BopConfigService(IConfiguration configuration)
        {
            DMSContext context = new(configuration);
            _repositoryBopConfig = new GenericRepository<Domain.Model.BopConfig>(context);
            Configuration = configuration;
        }

        public SmtpEmailSettings getStmtpEmailSettings()
        {
            SmtpEmailSettings smtpEmailSettings = new();

            var port = GetConfigFromDataBase(nameof(smtpEmailSettings.Port));
            var Host = GetConfigFromDataBase(nameof(smtpEmailSettings.Host));
            var FromEmailUsername = GetConfigFromDataBase(nameof(smtpEmailSettings.FromEmailUsername));
            var FromEmailPassword = GetConfigFromDataBase(nameof(smtpEmailSettings.FromEmailPassword));
            var From = GetConfigFromDataBase(nameof(smtpEmailSettings.From));
            var EnableSsl = GetConfigFromDataBase(nameof(smtpEmailSettings.EnableSsl));
            var TimeOut = GetConfigFromDataBase(nameof(smtpEmailSettings.TimeOut));
            var UseDefaultCredentials = GetConfigFromDataBase(nameof(smtpEmailSettings.UseDefaultCredentials));
            var DisplayName = GetConfigFromDataBase(nameof(smtpEmailSettings.DisplayName));
            Configuration.GetSection(nameof(SmtpEmailSettings)).Value = Newtonsoft.Json.JsonConvert.SerializeObject(smtpEmailSettings);
            var config = Configuration.GetSection(nameof(SmtpEmailSettings));
                      
            smtpEmailSettings.Port = port != null ? int.Parse(port) : int.Parse(GetConfigFromAppSettings(config,nameof(smtpEmailSettings.Port)));
            smtpEmailSettings.Host = Host != null ? Host : GetConfigFromAppSettings(config, nameof(smtpEmailSettings.Host));
            smtpEmailSettings.FromEmailUsername = FromEmailUsername != null ? FromEmailUsername : GetConfigFromAppSettings(config, nameof(smtpEmailSettings.FromEmailUsername));
            smtpEmailSettings.FromEmailPassword = FromEmailPassword != null ? FromEmailPassword : GetConfigFromAppSettings(config, nameof(smtpEmailSettings.FromEmailPassword));
            smtpEmailSettings.From = From != null ? From : GetConfigFromAppSettings(config, nameof(smtpEmailSettings.From));
            smtpEmailSettings.EnableSsl = EnableSsl != null ? bool.Parse(EnableSsl) : bool.Parse(GetConfigFromAppSettings(config, nameof(smtpEmailSettings.EnableSsl)));
            smtpEmailSettings.TimeOut = TimeOut != null ? int.Parse(TimeOut) : int.Parse(GetConfigFromAppSettings(config, nameof(smtpEmailSettings.TimeOut)));
            smtpEmailSettings.UseDefaultCredentials = UseDefaultCredentials != null ? bool.Parse( UseDefaultCredentials) : bool.Parse(GetConfigFromAppSettings(config, nameof(smtpEmailSettings.UseDefaultCredentials)));
            smtpEmailSettings.DisplayName = DisplayName != null ? DisplayName : GetConfigFromAppSettings(config, nameof(smtpEmailSettings.DisplayName));

        

            return smtpEmailSettings;
        }

        private string GetConfigFromDataBase(string EnumValue)
        {
            return _repositoryBopConfig.Query(x => x.EnumValue == EnumValue).FirstOrDefault()?.Value;
        }
        private string GetConfigFromAppSettings (IConfiguration configuration,string EnumValue)
        {
            return configuration.GetSection(EnumValue)?.Value;
        }
    }
}
