using AutoMapper;
using Intelli.DMS.Api.Basic_Authorization;
using Intelli.DMS.Api.DTO.Mapping;
using Intelli.DMS.Api.Events;
using Intelli.DMS.Api.Helpers;
using Intelli.DMS.Api.Services;
using Intelli.DMS.Api.Services.ActiveDirectory;
using Intelli.DMS.Api.Services.ActiveDirectory.Impl;
using Intelli.DMS.Api.Services.Cache;
using Intelli.DMS.Api.Services.Cache.Impl;
using Intelli.DMS.Api.Services.ConvertToZip;
using Intelli.DMS.Api.Services.ConvertToZip.Impl;
using Intelli.DMS.Api.Services.ConfigFromDB;
using Intelli.DMS.Api.Services.DocumentCheckIn;
using Intelli.DMS.Api.Services.DocumentCheckIn.Impl;
using Intelli.DMS.Api.Services.DocumentCheckOut;
using Intelli.DMS.Api.Services.DocumentCheckOut.Impl;
using Intelli.DMS.Api.Services.DocumentFields;
using Intelli.DMS.Api.Services.DocumentFields.Impl;
using Intelli.DMS.Api.Services.DocumentReview;
using Intelli.DMS.Api.Services.DocumentReview.Impl;
using Intelli.DMS.Api.Services.Email.Impl;

using Intelli.DMS.Api.Services.LocalStorage;
using Intelli.DMS.Api.Services.Session;
using Intelli.DMS.Api.Services.Session.Impl;
using Intelli.DMS.Domain.Database;
using Intelli.DMS.Domain.Model;
using Intelli.DMS.EventBus.RabbitMQ.Config;
using Intelli.DMS.EventBus.RabbitMQ.Receiver;
using Intelli.DMS.EventBus.RabbitMQ.Receiver.Impl;
using Intelli.DMS.EventBus.RabbitMQ.Sender;
using Intelli.DMS.EventBus.RabbitMQ.Sender.Impl;
using Intelli.DMS.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Text;
using Intelli.DMS.Api.Services.ClientRepository;
using Intelli.DMS.Api.Services.ClientRepository.Impl;
using Intelli.DMS.Api.Services.DocumentUpload;
using Intelli.DMS.Api.Services.DocumentUpload.Impl;
using Intelli.DMS.Api.Services.DocumentApproved;
using Intelli.DMS.Api.Services.DocumentApproved.Impl;
using Intelli.DMS.Api.Services.Status;
using Intelli.DMS.Api.Services.Status.Impl;
using System.Linq;
using Intelli.DMS.Api.DTO;
using System.Collections.Generic;
using NLog;
using NLog.Targets;
using Intelli.DMS.Shared.DTO;
using Intelli.DMS.Api.Services.DocumentDelete;
using Intelli.DMS.Api.Services.DocumentDelete.Impl;

namespace Intelli.DMS.Api
{
    /// <summary>
    /// The startup class.
    /// </summary>
    public class Startup
    {
        private DMSContext _dMSContext;
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            AssemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Gets or sets the assembly name.
        /// </summary>
        public string AssemblyName { get; }

        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Configure Auto Mapper
            ConfigureAutoMapper(services);

            // Adding MVC API controllers to services
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            services
             .AddAuthentication()
             .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", options => { });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("BasicAuthentication", new AuthorizationPolicyBuilder("BasicAuthentication").RequireAuthenticatedUser().Build());
            });

            // Setting up Http Context Accessors to be used to get username from header
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Configure database context
            ConfigureDatabaseContext(services);

            // Configure Authentication
            ConfigureAuthentication(services);

            // Configure audit database context.
            ConfigureAuditDbContext(services);

            // Configure swagger
            ConfigureSwaggerGen(services);

            // Load email sending settings from configuration for the email sender service.
            services.Configure<AuthMessageSenderOptions>(Configuration.GetSection("SendEmailSettings"));

            // Inject email sending service for users' email verification. 
            //services.AddSingleton<IEmailSender, EmailSender>();
            services.AddSingleton<IEmailSender, SmtpEmailSender>();

            // Setting up auth email service
            services.AddSingleton<IAuthEmailService, AuthEmailService>();

            // Setting up privileges service
            services.AddScoped<IPrivilegesService, PrivilegesService>();

            // Setting up jwt token service
            services.AddSingleton<IJWTService, JWTService>();

            // Cache settings
            services.Configure<CacheSettings>(Configuration.GetSection(nameof(CacheSettings)));

            // Load email sending settings from configuration for the email sender service.
            services.Configure<SmtpEmailSettings>(Configuration.GetSection(nameof(SmtpEmailSettings)));

            // Cache setup
            services.AddSingleton<IGenericCache<string>, GenericCache<string>>();

            // Setting up session manager 
            services.AddScoped<ISessionManager, SessionManager>();

            // Setting up Auth Service
            services.AddScoped<IAuthService, AuthService>();

            // Setting up document check out Service
            services.AddScoped<ICheckOutService, CheckOutService>();

            // Setting up document check out Service
            services.AddScoped<ICheckInService, CheckInService>();

            // Setting up document check out Service
            services.AddScoped<IDocumentFieldService, DocumentFieldService>();

            // Setting up document review Service
            services.AddScoped<IDocumentReviewService, DocumentReviewService>();


            //setting up file storage service
            services.AddSingleton<IStorageManager, FileSystemStorageService>();

            //setting Up Alerts Service
            services.AddScoped<IAlertsService, AlertsService>();

            //setting Up Convert to zip Service
            services.AddScoped<IConvertToZipService, ConvertToZipService>();

            //setting Up Config From DB Service
            services.AddScoped<IConfigFromDBService, ConfigFromDBService>();

            //setting Up Document Url Service
            services.AddScoped<IDocumentUrlService, DocumentUrlService>();

            // Setting up active directory integration service
            services.AddScoped<IActiveDirectoryService, ActiveDirectoryService>();


            // Setting up Client Repository Service
            services.AddScoped<IClientRepositoryService, ClientRepositoryService>();

            // Setting up Document Upload Service
            services.AddScoped<IDocumentUploadService, DocumentUploadService>();

            // Setting up Document Upload Service
            services.AddScoped<IDocumentApprovedService, DocumentApprovedService>();

            // Setting up Status Service
            services.AddScoped<IStatusService, StatusService>();

            // Setting up Document Delete Service
            services.AddScoped<IDocumentDeleteService, DocumentDeleteService>();


            // Configures the event bus.
            ConfigureEventBus(services);
        }

        /// <summary>
        /// Configures the swagger.
        /// </summary>
        /// <param name="services">The services.</param>
        private void ConfigureSwaggerGen(IServiceCollection services)
        {
            _ = services.AddSwaggerGen(c =>
            {
                // configure SwaggerDoc and others
                c.SwaggerDoc("v1", new OpenApiInfo { Title = AssemblyName, Version = "v1" });

                // add JWT Authentication
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "JWT Authentication",
                    Description = "Enter JWT Bearer token **_only_**",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer", // must be lower case
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {securityScheme, Array.Empty<string>()}
                });
            });
        }

        /// <summary>
        /// Configures the authentication.
        /// </summary>
        /// <param name="services">The services.</param>
        private void ConfigureAuthentication(IServiceCollection services)
        {
            // Load password policy from configurations.
            var dto = ConfigurationHelper.Read(
                                        services.BuildServiceProvider().GetRequiredService<DMSContext>(),
                                        services.BuildServiceProvider().GetRequiredService<IMapper>());

            // Adding MS Identity and Role to the services
            services.AddIdentity<AspNetUser, IdentityRole>(options =>
            {
                // Password policy.
                options.Password = dto.ToPasswordOptions();

                // User settings.
                options.User.RequireUniqueEmail = true;

                // Signin options.
                options.SignIn.RequireConfirmedAccount = true;

                // Token provider.
                options.Tokens.PasswordResetTokenProvider = "Default";
            })
                .AddEntityFrameworkStores<DMSContext>()
                .AddErrorDescriber<CustomIdentityErrorDescriber>()
                .AddDefaultTokenProviders()
                .AddTokenProvider<DefaultDataProtectorTokenProvider<AspNetUser>>("Default")
                .AddPasswordValidator<DMSPasswordValidator>()
                .AddSignInManager<DMSSignInManager>();
            // Read time span for token expiration time from configuration file
            int tokenLifespanInMinutes = Configuration.GetValue<int>(nameof(tokenLifespanInMinutes));

            // Set time span for token expiration
            services.Configure<DefaultDataProtectorTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromMinutes(tokenLifespanInMinutes);
            });

            // Configure token authentication
            var key = Encoding.UTF8.GetBytes(Configuration[IAuthConstants.JwtSecretKey]);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = Configuration[IAuthConstants.JwtIssuer],
                    ValidateAudience = true,
                    ValidAudience = Configuration[IAuthConstants.JwtAudience],
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }

        /// <summary>
        /// Configures the audit database context.
        /// </summary>
        /// <param name="services">The services.</param>
        private void ConfigureAuditDbContext(IServiceCollection services)
        {
            // Adding Audit Db Context to services
            services.AddDbContext<DMSAuditContext>(options =>
            {
                options.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
                options.UseSqlServer(GetSqlConnectionString("DBConnections:AuditDB"),
                    b => b.MigrationsAssembly(AssemblyName));
            });
        }

        /// <summary>
        /// Configures the event bus.
        /// </summary>
        /// <param name="services">The services collection.</param>
        private void ConfigureEventBus(IServiceCollection services)
        {
            services.AddOptions();

            services.Configure<RabbitMqConfiguration>(Configuration.GetSection("RabbitMq"));
            services.AddTransient<IEventSender, EventSender>();

            services.AddTransient<IQueueHandlerMappingFactory, QueueHandlerMappingFactory>();
            services.AddHostedService<EventListener>();
        }

        /// <summary>
        /// Configures the auto mapper.
        /// </summary>
        /// <param name="services">The services collection.</param>
        private static void ConfigureAutoMapper(IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc => mc.AddProfile(new MappingProfile()));

            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        /// <summary>
        /// Configures the database context.
        /// </summary>
        /// <param name="services">The services.</param>
        private void ConfigureDatabaseContext(IServiceCollection services)
        {
            // Adding Db Context to services
            services.AddDbContext<DMSContext>(options =>
            {
                options.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
                options.UseSqlServer(GetSqlConnectionString("DBConnections:DMSDB"),
                    b => b.MigrationsAssembly("Intelli.DMS.Domain"));
            });
        }
        /// <summary>
        /// Gets the sql connection string.
        /// </summary>
        /// <param name="sectionName">The section name.</param>
        /// <returns>A string.</returns>
        private string GetSqlConnectionString(string sectionName)
        {
            SqlConnectionConfigurationDTO config = Configuration.GetSection(sectionName)
                                                                .Get<SqlConnectionConfigurationDTO>();
            return SqlConnectionHelper.ToConnectionString(config);
        }
        /// <summary>
        /// Configures the.
        /// </summary>
        /// <param name="app">The app.</param>
        /// <param name="env">The env.</param>
        public void Configure(IApplicationBuilder app,
                              IWebHostEnvironment env,
                              IHostApplicationLifetime hostApplicationLifetime,
                              DMSContext dMSContext)
        {

            foreach (DatabaseTarget target in LogManager.Configuration.AllTargets.Where(t => t is DatabaseTarget))
            {
                target.ConnectionString = GetSqlConnectionString("DBConnections:DMSDB");
            }

            LogManager.ReconfigExistingLoggers();

            _dMSContext = dMSContext;
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(a => a.Run(async context =>
                {
                    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                    var exception = exceptionHandlerPathFeature.Error;

                    var response = new Intelli.DMS.Shared.Response
                    {
                        Errors = exception,
                        Message = exception.Message,
                        Payload = null,
                        Status = Shared.Response.RequestStatus.Error
                    };

                    var result = JsonConvert.SerializeObject(response);
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(result);
                }));
            }

            // Use Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", AssemblyName));

            // Routing
            app.UseRouting();

            // Authentication
            app.UseAuthentication();

            // Authorization
            app.UseAuthorization();

            // Endpoints
            app.UseEndpoints(endpoints => endpoints.MapControllers().RequireAuthorization());

        }
    }
}
