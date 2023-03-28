using Intelli.DMS.Api.DTO;
using Intelli.DMS.Api.Services;
using Intelli.DMS.Shared.Mvc.Controllers;
using Intelli.DMS.Shared.Mvc.Resources;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Intelli.DMS.Api.Controllers.AlertPanel
{
    /// <summary>
    /// The Alert api controller.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AlertController : BaseController
    {
        private readonly IAlertsService _alertsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AlertController"/> class.
        /// </summary>
        /// <param name="alertsService">Instance of <see cref="AlertsService"/> will be injected</param>
        public AlertController(IAlertsService alertsService)
        {
            _alertsService = alertsService;
        }

        /// <summary>
        /// Gets the Alerts.
        /// </summary>
        /// <param name="systemUserId">The User id.</param>
        /// <returns>The AlertDto List.</returns>
        [HttpGet("{systemUserId}")]
        public IActionResult Get(int systemUserId)
        {
            // Checking if the passed Parameter is valid
            if (!ModelState.IsValid)
                return BadRequest(MsgKeys.InvalidInputParameters);

            var items =  _alertsService.GetAllAlertsBySystemUserId(systemUserId);

            return Ok(new { Items = items });
        }

        /// <summary>
        /// Creates the Alert.
        /// </summary>
        /// <param name="dto">The Alert dto.</param>
        /// <returns>An IActionResult.</returns>
        [HttpPost]
        public  IActionResult Post(AlertDTO dto)
        {
            // Checking if the passed Parameter is valid
            if (!ModelState.IsValid || dto == null)
                return BadRequest(MsgKeys.InvalidInputParameters);

            return Ok( _alertsService.AddAlert(dto));
        }

        /// <summary>
        /// Mark Alert As Read
        /// </summary>
        /// <param name="id">The Alert id.</param>
        /// <returns>An IActionResult.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id)
        {
            // Checking if the passed Parameter is valid
            if (!ModelState.IsValid)
                return BadRequest(MsgKeys.InvalidInputParameters);

            return Ok(await _alertsService.UpdateAlert(id));
        }

        /// <summary>
        /// Mark All Alert As Read
        /// </summary>
        /// <param name="id">The Alert id.</param>
        /// <returns>An IActionResult.</returns>
        [HttpPut("ReadAllAlerts")]
        public  IActionResult ReadAllAlerts()
        {
            try
            {
                // Checking if the passed Parameter is valid
                _alertsService.UpdateAlertReadAllAlerts(UserId);
                return Ok("All Alerts Reads .");
            }
            catch(Exception ex)
            {
                return BadRequest(new
                {
                    Errors = ex,
                    ex.Message
                });
            }
        }
    }
}
