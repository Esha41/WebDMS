using Intelli.DMS.Api.Services.ConfigFromDB;
using Intelli.DMS.Shared.Mvc.Controllers;
using Intelli.DMS.Shared.Mvc.Resources;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Intelli.DMS.Api.Controllers.ConfigFromDB
{
    /// <summary>
    /// The Config From DB Controller.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ConfigFromDBController : BaseController
    {
        private readonly IConfigFromDBService _configFromDBService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigFromDBController"/> class.
        /// </summary>
        /// <param name="configFromDBService">Instance of <see cref="ConfigFromDBService"/> will be injected</param>
        public ConfigFromDBController(IConfigFromDBService configFromDBService)
        {
            _configFromDBService = configFromDBService;
        }

        /// <summary>
        /// Gets the config from DB.
        /// </summary>
        /// <returns>BopConfigsDTO.</returns>
        [HttpGet]
        public async Task<IActionResult> Get(string enumValue)
        {
            // Checking if the passed Parameter is valid
            if (!ModelState.IsValid)
                return BadRequest(MsgKeys.InvalidInputParameters);
 
            return Ok(await _configFromDBService.GetConfigFromDataBase(enumValue));
        }
    }
}
