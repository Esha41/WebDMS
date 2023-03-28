using AutoMapper;
using Intelli.DMS.Api.DTO;
using Intelli.DMS.Domain.Core.Repository;
using Intelli.DMS.Domain.Database;
using Intelli.DMS.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Intelli.DMS.Api.Services.ConfigFromDB
{
    /// <summary>
    /// The Config From Database Service.
    /// </summary>
    public class ConfigFromDBService : IConfigFromDBService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Domain.Model.BopConfig> _repositoryConfig;
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigFromDBService"/> class.
        /// </summary>
        /// <param name="context">Instance of <see cref="DMSContext"/> will be injected</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/> will be injected</param>
        /// <param name="logger">Instance of <see cref="ILogger"/> will be injected</param>
        public ConfigFromDBService(DMSContext context, IMapper mapper)
        {
            _mapper = mapper;
            _repositoryConfig = new GenericRepository<Domain.Model.BopConfig>(context);
        }

        /// <summary>
        /// Get Config From Database.
        /// </summary>
        /// <param name="EnumValue">The Enum Value.</param>
        /// <returns>A Task Config .</returns>

        public async Task<BopConfigsDTO> GetConfigFromDataBase(string EnumValue)
        {
            var query =_repositoryConfig.Query(x => x.EnumValue == EnumValue);
            var result =await  query.FirstOrDefaultAsync();
            return _mapper.Map<BopConfigsDTO>(result);
        }
    }
}
