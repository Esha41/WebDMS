using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdvancedLogging",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentID = table.Column<int>(type: "int", nullable: true),
                    RequestId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IP = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Browser = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Device = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    System = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Controller = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Action = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ActionCompletion = table.Column<bool>(type: "bit", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Level = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    RequestURL = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: true),
                    RequestPayload = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    ResponceStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ResponceError = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponcePayload = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponceTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    ExitDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvancedLogging", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Audits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditDateTimeUtc = table.Column<long>(type: "bigint", nullable: false),
                    AuditType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    AuditUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TableName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KeyValues = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OldValues = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NewValues = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChangedColumns = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "(CONVERT([bit],(1)))"),
                    CreatedAt = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedAt = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BatchesCount",
                columns: table => new
                {
                    Count = table.Column<int>(type: "int", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "BatchItemStatuses",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BatchItemStatus = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EnumValue = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BatchItemStatuses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BatchSources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BatchSource = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EnumValue = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BatchSourceCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BatchSources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BatchStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BatchStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EnumValue = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BatchStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BopConfigs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Setting = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EnumValue = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BopConfigs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BUs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BUs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatalogNameProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Product = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogNameProducts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CallBackURL = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    HawkAppID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    HawkUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    HawkSecret = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FtpHostName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FtpUserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FtpPassword = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FtpDirectory = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    RetriesWhenFailPublished = table.Column<int>(type: "int", nullable: false),
                    GDPRDaysToBeKept = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    FtpPort = table.Column<int>(type: "int", nullable: true),
                    FtpUserSecureProtocol = table.Column<bool>(type: "bit", nullable: true),
                    FtpActive = table.Column<bool>(type: "bit", nullable: true),
                    FtpResponseHostName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FtpResponseUserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FtpResponsePassword = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FtpResponsePort = table.Column<int>(type: "int", nullable: true),
                    FtpResponseUserSecureProtocol = table.Column<bool>(type: "bit", nullable: true),
                    FtpResponseActive = table.Column<bool>(type: "bit", nullable: true),
                    FtpResponseDirectory = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ResponseWithFtp = table.Column<bool>(type: "bit", nullable: true),
                    SimilarityThreshold = table.Column<int>(type: "int", nullable: true),
                    Enabled = table.Column<bool>(type: "bit", nullable: true),
                    SLAMinutes = table.Column<int>(type: "int", nullable: true),
                    SLABatchQuantity = table.Column<int>(type: "int", nullable: true),
                    SLAImportance = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsSignedCompany = table.Column<bool>(type: "bit", nullable: false),
                    SendRejectionReasonAsCode = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))"),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SendLink = table.Column<bool>(type: "bit", nullable: false),
                    SupportsCalls = table.Column<bool>(type: "bit", nullable: false),
                    MaxCallTIme = table.Column<int>(type: "int", nullable: true),
                    VideoCallBackUrl = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    AgentController = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CustomerRetries = table.Column<int>(type: "int", nullable: true),
                    SMSProvider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedAt = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedAt = table.Column<long>(type: "bigint", nullable: false),
                    UsersPerCompany = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyFlows",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    FlowName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyFlows", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CompanySigningDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    DocumentName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DocumentType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanySigningDocuments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Configurations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PasswordRequireNonAlphanumeric = table.Column<bool>(type: "bit", nullable: false),
                    PasswordRequireLowercase = table.Column<bool>(type: "bit", nullable: false),
                    PasswordRequireUppercase = table.Column<bool>(type: "bit", nullable: false),
                    PasswordRequireDigit = table.Column<bool>(type: "bit", nullable: false),
                    PasswordRequiredLength = table.Column<int>(type: "int", nullable: false),
                    RestrictLastUsedPasswords = table.Column<int>(type: "int", nullable: false),
                    ForcePasswordChangeDays = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedAt = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configurations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Code2D = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Code3D = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    MobileCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedAt = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedAt = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AFM = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CDI = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedAt = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedAt = table.Column<long>(type: "bigint", nullable: false),
                    IsValidForTransaction = table.Column<bool>(type: "bit", nullable: false),
                    Reason = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DashboardMenu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ViewName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DashboardMenu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DictionaryTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DictionaryType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "(CONVERT([bigint],(0)))"),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    UpdatedAt = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "(CONVERT([bigint],(0)))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DictionaryTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentCategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedAt = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedAt = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentClassFieldTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "(CONVERT([bigint],(0)))"),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    UpdatedAt = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "(CONVERT([bigint],(0)))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentClassFieldTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentRejectionReasons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    Descr = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentRejectionReasons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentTypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedAt = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedAt = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Licenses",
                columns: table => new
                {
                    License = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "LivenessTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Gr = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    En = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NumberResult = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LivenessTokens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NLog",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    PosId = table.Column<int>(type: "int", nullable: true),
                    Logged = table.Column<DateTime>(type: "datetime", nullable: false),
                    Level = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ClassName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Stacktrace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Exception = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NLog", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "OCREngines",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OCREngines", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Programme_List",
                columns: table => new
                {
                    Agreed_Power = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Hron_Programme_ApplicationForm = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Day_Charge = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Night_Charge = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Rules_xref",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyID = table.Column<int>(type: "int", nullable: false),
                    DocClassFieldID = table.Column<int>(type: "int", nullable: false),
                    RuleType = table.Column<byte>(type: "tinyint", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: true),
                    Formula = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rules_xref", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Screens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScreenName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedAt = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedAt = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Screens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceLastExcecution",
                columns: table => new
                {
                    ServiceName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Time = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Stations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComputerName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StationVariableTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StationVariableType = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    EnumValue = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    SupportsGlobal = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StationVariableTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Jmbg = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedAt = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedAt = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblMissingTemplateFields",
                columns: table => new
                {
                    CompanyID = table.Column<int>(type: "int", nullable: false),
                    DocumentClassFieldID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "TempBatchPages",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BatchId = table.Column<int>(type: "int", nullable: false),
                    KeyFileName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempBatchPages", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdvancedSignatureCallHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: false),
                    CallBodyInput = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SigningSucceeded = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvancedSignatureCallHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdvancedSignatureCallHistory_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SystemRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedAt = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedAt = table.Column<long>(type: "bigint", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SystemRoles_company",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyAbbyTemplateFields",
                columns: table => new
                {
                    CompanyID = table.Column<int>(type: "int", nullable: false),
                    DocumentClassFieldID = table.Column<int>(type: "int", nullable: false),
                    CompanyFlowId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK__CompanyAb__Compa__3587F3E0",
                        column: x => x.CompanyFlowId,
                        principalTable: "CompanyFlows",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Batches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BusinessUnitId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    BatchStatusId = table.Column<int>(type: "int", nullable: false),
                    LockedBy = table.Column<int>(type: "int", nullable: true),
                    VerifiedStartDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    VerifiedEndDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    BatchSourceId = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))"),
                    PublishedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    RetriesCount = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))"),
                    MandatoryAlerts = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidationAlerts = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    CurrentOTP = table.Column<int>(type: "int", nullable: true),
                    OTPValidUntil = table.Column<DateTime>(type: "datetime", nullable: true),
                    AppliedGDPR = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((0))"),
                    RecognizedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    StartProcessDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    InternalRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LockedByNavigationId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedAt = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedAt = table.Column<long>(type: "bigint", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Batches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Batches_BatchSources",
                        column: x => x.BatchSourceId,
                        principalTable: "BatchSources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Batches_BatchStatuses1",
                        column: x => x.BatchStatusId,
                        principalTable: "BatchStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Batches_BUs",
                        column: x => x.BusinessUnitId,
                        principalTable: "BUs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_batches_Companies",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Batches_Customers",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BopDictionaries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DictionaryTypeId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValueSql: "((1))"),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedAt = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedAt = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BopDictionaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dictionaries_DictionaryTypes",
                        column: x => x.DictionaryTypeId,
                        principalTable: "DictionaryTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DocumentSubCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentSubCategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValueSql: "(N'')"),
                    DocumentCategoryId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedAt = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedAt = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentSubCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentSubCategories_DocumentCategories_DocumentCategoryId",
                        column: x => x.DocumentCategoryId,
                        principalTable: "DocumentCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocumentRejectionReasonCompany",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    DocumentRejectionReasonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentRejectionReasonCompany", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentRejectionReasonCompany_Companies",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DocumentRejectionReasonCompany_DocumentRejectionReasons",
                        column: x => x.DocumentRejectionReasonId,
                        principalTable: "DocumentRejectionReasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DocumentClasses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentClassName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    EnumValue = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RecognitionMappedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GroupMandatoryDocument = table.Column<short>(type: "smallint", nullable: true),
                    DocumentTypeId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "(CONVERT([bigint],(0)))"),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    UpdatedAt = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "(CONVERT([bigint],(0)))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentClasses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentClasses_DocumentTypes",
                        column: x => x.DocumentTypeId,
                        principalTable: "DocumentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ScreenColumns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScreenId = table.Column<int>(type: "int", nullable: false),
                    ColumnName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DefaultOrder = table.Column<int>(type: "int", nullable: false),
                    DefaultVisibility = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedAt = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedAt = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScreenColumns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScreenColumns_Screens_ScreenId",
                        column: x => x.ScreenId,
                        principalTable: "Screens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScreenElements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScreenElementName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ScreenId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedAt = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedAt = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScreenElements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScreenElements_Screens_ScreenId",
                        column: x => x.ScreenId,
                        principalTable: "Screens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StationVariables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StationId = table.Column<int>(type: "int", nullable: true),
                    StationVariableTypeId = table.Column<int>(type: "int", nullable: false),
                    VariableValue = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StationVariables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StationVariables_Stations",
                        column: x => x.StationId,
                        principalTable: "Stations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StationVariables_StationVariableTypes",
                        column: x => x.StationVariableTypeId,
                        principalTable: "StationVariableTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SystemUserId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_SystemUsers_SystemUserId",
                        column: x => x.SystemUserId,
                        principalTable: "SystemUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ColumnPreferences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SystemUserId = table.Column<int>(type: "int", nullable: false),
                    ScreenId = table.Column<int>(type: "int", nullable: false),
                    ColumnName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedAt = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedAt = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColumnPreferences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ColumnPreferences_Screens_ScreenId",
                        column: x => x.ScreenId,
                        principalTable: "Screens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ColumnPreferences_SystemUsers_SystemUserId",
                        column: x => x.SystemUserId,
                        principalTable: "SystemUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PasswordHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SystemUserId = table.Column<int>(type: "int", nullable: false),
                    ChangedAt = table.Column<long>(type: "bigint", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasswordHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PasswordHistory_SystemUsers_SystemUserId",
                        column: x => x.SystemUserId,
                        principalTable: "SystemUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SystemUserCountries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SystemUserId = table.Column<int>(type: "int", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedAt = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedAt = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemUserCountries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SystemUserCountries_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SystemUserCountries_SystemUsers_SystemUserId",
                        column: x => x.SystemUserId,
                        principalTable: "SystemUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserCompanies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SystemUserId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedAt = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedAt = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCompanies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Companyies_Companyid",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SystemUserId",
                        column: x => x.SystemUserId,
                        principalTable: "SystemUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserPreferences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Language = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedAt = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedAt = table.Column<long>(type: "bigint", nullable: false),
                    GridPageSize = table.Column<int>(type: "int", nullable: false),
                    SystemUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPreferences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPreferences_SystemUsers_SystemUserId",
                        column: x => x.SystemUserId,
                        principalTable: "SystemUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSessions",
                columns: table => new
                {
                    SystemUserId = table.Column<int>(type: "int", nullable: false),
                    SessionId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSessions", x => x.SystemUserId);
                    table.ForeignKey(
                        name: "FK_UserSessions_SystemUsers_SystemUserId",
                        column: x => x.SystemUserId,
                        principalTable: "SystemUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleScreens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SystemRoleId = table.Column<int>(type: "int", nullable: false),
                    ScreenId = table.Column<int>(type: "int", nullable: false),
                    Privilege = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedAt = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedAt = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleScreens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleScreens_Screens_ScreenId",
                        column: x => x.ScreenId,
                        principalTable: "Screens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleScreens_SystemRoles_SystemRoleId",
                        column: x => x.SystemRoleId,
                        principalTable: "SystemRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SystemUserRole",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SystemUserId = table.Column<int>(type: "int", nullable: false),
                    SystemRoleId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedAt = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedAt = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemUserRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SystemUserRole_SystemRoles_SystemRoleId",
                        column: x => x.SystemRoleId,
                        principalTable: "SystemRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SystemUserRole_SystemUsers_SystemUserId",
                        column: x => x.SystemUserId,
                        principalTable: "SystemUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BatchMetaHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OccuredAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    BatchId = table.Column<int>(type: "int", nullable: false),
                    PreviousValues = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentValues = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BatchMetaHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BatchMetaHistory_Batches",
                        column: x => x.BatchId,
                        principalTable: "Batches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BatchMetaHistory_SystemUsers",
                        column: x => x.SystemUserId,
                        principalTable: "SystemUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BatchItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BatchId = table.Column<int>(type: "int", nullable: false),
                    BatchItemStatusId = table.Column<int>(type: "int", nullable: false),
                    DocumentClassId = table.Column<int>(type: "int", nullable: true),
                    OccuredAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    AspNetUserId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedAt = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedAt = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BatchItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BatchItems_Batches",
                        column: x => x.BatchId,
                        principalTable: "Batches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BatchItems_BatchItems_PARENT",
                        column: x => x.ParentId,
                        principalTable: "BatchItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BatchItems_BatchItemStatuses",
                        column: x => x.BatchItemStatusId,
                        principalTable: "BatchItemStatuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BatchItems_DocumentClasses",
                        column: x => x.DocumentClassId,
                        principalTable: "DocumentClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BatchSourceDocumentsSpecifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BatchSourceId = table.Column<int>(type: "int", nullable: false),
                    DocumentClassId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    IsVirtual = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BatchSourceDocumentsSpecifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BatchSourceDocumentsSpecifications_BatchSources",
                        column: x => x.BatchSourceId,
                        principalTable: "BatchSources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BatchSourceDocumentsSpecifications_DocumentClasses",
                        column: x => x.DocumentClassId,
                        principalTable: "DocumentClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyAbbyTemplates",
                columns: table => new
                {
                    CompanyID = table.Column<int>(type: "int", nullable: false),
                    DocumentClassID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_CompanyAbbyTemplates_Companies",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyAbbyTemplates_DocumentClasses",
                        column: x => x.DocumentClassID,
                        principalTable: "DocumentClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DocumentClassFields",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentClassId = table.Column<int>(type: "int", nullable: false),
                    DocumentClassFieldTypeId = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))"),
                    EnumValue = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValueSql: "(N'-')"),
                    UILabel = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false, defaultValueSql: "('')"),
                    PublishEnabled = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))"),
                    UISort = table.Column<int>(type: "int", nullable: true),
                    DictionaryTypeId = table.Column<int>(type: "int", nullable: true),
                    IsMandatory = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))"),
                    MappedName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CorrectiveActionMappedName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedAt = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "(CONVERT([bigint],(0)))"),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    UpdatedAt = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "(CONVERT([bigint],(0)))"),
                    MinLength = table.Column<int>(type: "int", nullable: true),
                    MaxLength = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentClassFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentClassFields_DictionaryTypes",
                        column: x => x.DictionaryTypeId,
                        principalTable: "DictionaryTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DocumentClassFields_DocumentClasses",
                        column: x => x.DocumentClassId,
                        principalTable: "DocumentClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DocumentClassFields_DocumentClassFieldTypes",
                        column: x => x.DocumentClassFieldTypeId,
                        principalTable: "DocumentClassFieldTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DocumentsPerCompanies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentClassId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedAt = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedAt = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentsPerCompanies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentsPerCompanies_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentsPerCompanies_DocumentClasses_DocumentClassId",
                        column: x => x.DocumentClassId,
                        principalTable: "DocumentClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OCREnginesDocumentClasses",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentClassId = table.Column<int>(type: "int", nullable: false),
                    OCREngineId = table.Column<int>(type: "int", nullable: false),
                    OCREngineDocumentClassCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OCREnginesDocumentClasses", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OCREnginesDocumentClasses_DocumentClasses",
                        column: x => x.DocumentClassId,
                        principalTable: "DocumentClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OCREnginesDocumentClasses_OCREngines",
                        column: x => x.OCREngineId,
                        principalTable: "OCREngines",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoleScreenColumns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ScreenColumnId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedAt = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedAt = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleScreenColumns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleScreenColumns_ScreenColumns_ScreenColumnId",
                        column: x => x.ScreenColumnId,
                        principalTable: "ScreenColumns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleScreenColumns_SystemRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "SystemRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleScreenElements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ScreenElementId = table.Column<int>(type: "int", nullable: false),
                    Privilege = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedAt = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedAt = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleScreenElements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleScreenElements_ScreenElements_ScreenElementId",
                        column: x => x.ScreenElementId,
                        principalTable: "ScreenElements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleScreenElements_SystemRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "SystemRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BatchItemPages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BatchItemId = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    OriginalName = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedAt = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedAt = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BatchItemPages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BatchItemPages_BatchItems",
                        column: x => x.BatchItemId,
                        principalTable: "BatchItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BatchItemFields",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BatchItemId = table.Column<int>(type: "int", nullable: false),
                    DocumentClassFieldId = table.Column<int>(type: "int", nullable: false),
                    DictionaryValueId = table.Column<int>(type: "int", nullable: true),
                    IsLast = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))"),
                    RegisteredFieldValue = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    DictionaryValueId_old = table.Column<int>(type: "int", nullable: true),
                    RegisteredFieldValue_old = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsUpdated = table.Column<bool>(type: "bit", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedAt = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedAt = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BatchItemFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BatchItemFields_BatchItems",
                        column: x => x.BatchItemId,
                        principalTable: "BatchItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BatchItemFields_BopDictionaries",
                        column: x => x.DictionaryValueId,
                        principalTable: "BopDictionaries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BatchItemFields_DocumentClassFields",
                        column: x => x.DocumentClassFieldId,
                        principalTable: "DocumentClassFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BatchMeta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BatchId = table.Column<int>(type: "int", nullable: false),
                    DocumentClassFieldId = table.Column<int>(type: "int", nullable: false),
                    DictionaryValueId = table.Column<int>(type: "int", nullable: true),
                    FieldValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedAt = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedAt = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BatchMeta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BatchMeta_Batches",
                        column: x => x.BatchId,
                        principalTable: "Batches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BatchMeta_Dictionaries",
                        column: x => x.DictionaryValueId,
                        principalTable: "BopDictionaries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BatchMeta_DocumentClassFields",
                        column: x => x.DocumentClassFieldId,
                        principalTable: "DocumentClassFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdvancedSignatureCallHistory_CompanyId",
                table: "AdvancedSignatureCallHistory",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers",
                table: "AspNetUsers",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_1",
                table: "AspNetUsers",
                column: "Email",
                unique: true,
                filter: "([Email] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_SystemUserId",
                table: "AspNetUsers",
                column: "SystemUserId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Batches_BatchSourceId",
                table: "Batches",
                column: "BatchSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Batches_BatchStatusId",
                table: "Batches",
                column: "BatchStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Batches_BusinessUnitId",
                table: "Batches",
                column: "BusinessUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Batches_CompanyId",
                table: "Batches",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Batches_CustomerId",
                table: "Batches",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Batches_LockedByNavigationId",
                table: "Batches",
                column: "LockedByNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_BatchItemFields_BatchItemId",
                table: "BatchItemFields",
                column: "BatchItemId");

            migrationBuilder.CreateIndex(
                name: "IX_BatchItemFields_DictionaryValueId",
                table: "BatchItemFields",
                column: "DictionaryValueId");

            migrationBuilder.CreateIndex(
                name: "IX_BatchItemFields_DocumentClassFieldId",
                table: "BatchItemFields",
                column: "DocumentClassFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_BatchItemPages_BatchItemId",
                table: "BatchItemPages",
                column: "BatchItemId");

            migrationBuilder.CreateIndex(
                name: "UX_BatchItemPages_FileName",
                table: "BatchItemPages",
                columns: new[] { "FileName", "BatchItemId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BatchItems_BatchId",
                table: "BatchItems",
                column: "BatchId");

            migrationBuilder.CreateIndex(
                name: "IX_BatchItems_BatchItemStatusId",
                table: "BatchItems",
                column: "BatchItemStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_BatchItems_DocumentClassId",
                table: "BatchItems",
                column: "DocumentClassId");

            migrationBuilder.CreateIndex(
                name: "IX_BatchItems_ParentId",
                table: "BatchItems",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_BatchMeta_BatchId",
                table: "BatchMeta",
                column: "BatchId");

            migrationBuilder.CreateIndex(
                name: "IX_BatchMeta_DictionaryValueId",
                table: "BatchMeta",
                column: "DictionaryValueId");

            migrationBuilder.CreateIndex(
                name: "IX_BatchMeta_DocumentClassFieldId",
                table: "BatchMeta",
                column: "DocumentClassFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_BatchMetaHistory_BatchId",
                table: "BatchMetaHistory",
                column: "BatchId");

            migrationBuilder.CreateIndex(
                name: "IX_BatchMetaHistory_SystemUserId",
                table: "BatchMetaHistory",
                column: "SystemUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BatchSourceDocumentsSpecifications_BatchSourceId",
                table: "BatchSourceDocumentsSpecifications",
                column: "BatchSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_BatchSourceDocumentsSpecifications_DocumentClassId",
                table: "BatchSourceDocumentsSpecifications",
                column: "DocumentClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Dictionaries",
                table: "BopDictionaries",
                columns: new[] { "DictionaryTypeId", "Value" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ColumnPreferences_ScreenId",
                table: "ColumnPreferences",
                column: "ScreenId");

            migrationBuilder.CreateIndex(
                name: "IX_ColumnPreferences_SystemUserId",
                table: "ColumnPreferences",
                column: "SystemUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyAbbyTemplateFields_CompanyFlowId",
                table: "CompanyAbbyTemplateFields",
                column: "CompanyFlowId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyAbbyTemplates_CompanyID",
                table: "CompanyAbbyTemplates",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyAbbyTemplates_DocumentClassID",
                table: "CompanyAbbyTemplates",
                column: "DocumentClassID");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_AFM",
                table: "Customers",
                column: "AFM",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DictionaryTypes",
                table: "DictionaryTypes",
                column: "DictionaryType",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DocumentClasses_DocumentTypeId",
                table: "DocumentClasses",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentClassFields_DictionaryTypeId",
                table: "DocumentClassFields",
                column: "DictionaryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentClassFields_DocumentClassFieldTypeId",
                table: "DocumentClassFields",
                column: "DocumentClassFieldTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentClassFields_DocumentClassId",
                table: "DocumentClassFields",
                column: "DocumentClassId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentRejectionReasonCompany_CompanyId",
                table: "DocumentRejectionReasonCompany",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentRejectionReasonCompany_DocumentRejectionReasonId",
                table: "DocumentRejectionReasonCompany",
                column: "DocumentRejectionReasonId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentsPerCompanies_CompanyId",
                table: "DocumentsPerCompanies",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentsPerCompanies_DocumentClassId",
                table: "DocumentsPerCompanies",
                column: "DocumentClassId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentSubCategories_DocumentCategoryId",
                table: "DocumentSubCategories",
                column: "DocumentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_OCREnginesDocumentClasses_DocumentClassId",
                table: "OCREnginesDocumentClasses",
                column: "DocumentClassId");

            migrationBuilder.CreateIndex(
                name: "IX_OCREnginesDocumentClasses_OCREngineId",
                table: "OCREnginesDocumentClasses",
                column: "OCREngineId");

            migrationBuilder.CreateIndex(
                name: "IX_PasswordHistory_SystemUserId",
                table: "PasswordHistory",
                column: "SystemUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleScreenColumns_RoleId",
                table: "RoleScreenColumns",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleScreenColumns_ScreenColumnId",
                table: "RoleScreenColumns",
                column: "ScreenColumnId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleScreenElements_RoleId",
                table: "RoleScreenElements",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleScreenElements_ScreenElementId",
                table: "RoleScreenElements",
                column: "ScreenElementId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleScreens_ScreenId",
                table: "RoleScreens",
                column: "ScreenId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleScreens_SystemRoleId",
                table: "RoleScreens",
                column: "SystemRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ScreenColumns_ScreenId",
                table: "ScreenColumns",
                column: "ScreenId");

            migrationBuilder.CreateIndex(
                name: "IX_ScreenElements_ScreenId",
                table: "ScreenElements",
                column: "ScreenId");

            migrationBuilder.CreateIndex(
                name: "IX_StationVariables",
                table: "StationVariables",
                columns: new[] { "StationId", "StationVariableTypeId" },
                unique: true,
                filter: "([StationId] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_StationVariables_StationVariableTypeId",
                table: "StationVariables",
                column: "StationVariableTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemRoles_CompanyId",
                table: "SystemRoles",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "UC_SystemRoles_Name",
                table: "SystemRoles",
                columns: new[] { "Name", "CompanyId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SystemUserCountries_CountryId",
                table: "SystemUserCountries",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemUserCountries_SystemUserId",
                table: "SystemUserCountries",
                column: "SystemUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemUserRoles_SystemRoleId",
                table: "SystemUserRole",
                column: "SystemRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemUserRoles_SystemUserId",
                table: "SystemUserRole",
                column: "SystemUserId");

            migrationBuilder.CreateIndex(
                name: "UC_SystemUsers_Email",
                table: "SystemUsers",
                column: "Email",
                unique: true,
                filter: "([Email] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_UserCompanies_CompanyId",
                table: "UserCompanies",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCompanies_SystemUserId",
                table: "UserCompanies",
                column: "SystemUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPreferences_SystemUserId",
                table: "UserPreferences",
                column: "SystemUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSessions_SystemUserId",
                table: "UserSessions",
                column: "SystemUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdvancedLogging");

            migrationBuilder.DropTable(
                name: "AdvancedSignatureCallHistory");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Audits");

            migrationBuilder.DropTable(
                name: "BatchesCount");

            migrationBuilder.DropTable(
                name: "BatchItemFields");

            migrationBuilder.DropTable(
                name: "BatchItemPages");

            migrationBuilder.DropTable(
                name: "BatchMeta");

            migrationBuilder.DropTable(
                name: "BatchMetaHistory");

            migrationBuilder.DropTable(
                name: "BatchSourceDocumentsSpecifications");

            migrationBuilder.DropTable(
                name: "BopConfigs");

            migrationBuilder.DropTable(
                name: "CatalogNameProducts");

            migrationBuilder.DropTable(
                name: "ColumnPreferences");

            migrationBuilder.DropTable(
                name: "CompanyAbbyTemplateFields");

            migrationBuilder.DropTable(
                name: "CompanyAbbyTemplates");

            migrationBuilder.DropTable(
                name: "CompanySigningDocuments");

            migrationBuilder.DropTable(
                name: "Configurations");

            migrationBuilder.DropTable(
                name: "DashboardMenu");

            migrationBuilder.DropTable(
                name: "DocumentRejectionReasonCompany");

            migrationBuilder.DropTable(
                name: "DocumentsPerCompanies");

            migrationBuilder.DropTable(
                name: "DocumentSubCategories");

            migrationBuilder.DropTable(
                name: "Licenses");

            migrationBuilder.DropTable(
                name: "LivenessTokens");

            migrationBuilder.DropTable(
                name: "NLog");

            migrationBuilder.DropTable(
                name: "OCREnginesDocumentClasses");

            migrationBuilder.DropTable(
                name: "PasswordHistory");

            migrationBuilder.DropTable(
                name: "Programme_List");

            migrationBuilder.DropTable(
                name: "RoleScreenColumns");

            migrationBuilder.DropTable(
                name: "RoleScreenElements");

            migrationBuilder.DropTable(
                name: "RoleScreens");

            migrationBuilder.DropTable(
                name: "Rules_xref");

            migrationBuilder.DropTable(
                name: "ServiceLastExcecution");

            migrationBuilder.DropTable(
                name: "StationVariables");

            migrationBuilder.DropTable(
                name: "SystemUserCountries");

            migrationBuilder.DropTable(
                name: "SystemUserRole");

            migrationBuilder.DropTable(
                name: "tblMissingTemplateFields");

            migrationBuilder.DropTable(
                name: "TempBatchPages");

            migrationBuilder.DropTable(
                name: "UserCompanies");

            migrationBuilder.DropTable(
                name: "UserPreferences");

            migrationBuilder.DropTable(
                name: "UserSessions");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "BatchItems");

            migrationBuilder.DropTable(
                name: "BopDictionaries");

            migrationBuilder.DropTable(
                name: "DocumentClassFields");

            migrationBuilder.DropTable(
                name: "CompanyFlows");

            migrationBuilder.DropTable(
                name: "DocumentRejectionReasons");

            migrationBuilder.DropTable(
                name: "DocumentCategories");

            migrationBuilder.DropTable(
                name: "OCREngines");

            migrationBuilder.DropTable(
                name: "ScreenColumns");

            migrationBuilder.DropTable(
                name: "ScreenElements");

            migrationBuilder.DropTable(
                name: "Stations");

            migrationBuilder.DropTable(
                name: "StationVariableTypes");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "SystemRoles");

            migrationBuilder.DropTable(
                name: "SystemUsers");

            migrationBuilder.DropTable(
                name: "Batches");

            migrationBuilder.DropTable(
                name: "BatchItemStatuses");

            migrationBuilder.DropTable(
                name: "DictionaryTypes");

            migrationBuilder.DropTable(
                name: "DocumentClasses");

            migrationBuilder.DropTable(
                name: "DocumentClassFieldTypes");

            migrationBuilder.DropTable(
                name: "Screens");

            migrationBuilder.DropTable(
                name: "BatchSources");

            migrationBuilder.DropTable(
                name: "BatchStatuses");

            migrationBuilder.DropTable(
                name: "BUs");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "DocumentTypes");
        }
    }
}
