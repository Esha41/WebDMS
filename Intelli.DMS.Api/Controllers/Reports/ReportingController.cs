using Intelli.DMS.Api.DTO.Reporting;
using Intelli.DMS.Domain.Database;
using Intelli.DMS.Domain.Model.StoredProceduresOutput;
using Intelli.DMS.Shared.Mvc.Controllers;
using Intelli.DMS.Shared.Mvc.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Intelli.DMS.Api.Controllers.v1
{
    /// <summary>
    /// The Reporting Controller.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ReportingController : BaseController
    {
        private readonly DMSContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportingController"/> class.
        /// </summary>
        /// <param name="context">Instance of <see cref="DMSContext"/> will be injected</param>
        public ReportingController(DMSContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns batches count report.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns>A Reports collection response type.</returns>
        [HttpPost]
        public async Task<IActionResult> BatchesCount([FromBody] BatchesCountDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(MsgKeys.InvalidInputParameters);

            var createDateFrom = DateTimeOffset.FromUnixTimeSeconds(dto.CreateDateFrom).DateTime.ToString("yyyy-MM-dd");
            var createDateTo = DateTimeOffset.FromUnixTimeSeconds(dto.CreateDateTo).DateTime.ToString("yyyy-MM-dd");

            var query = _context.BatchesCount.FromSqlInterpolated(
                            $"EXEC dbo.batches_count {dto.CompanyId}, {createDateFrom}, {createDateTo}");

            var list = await query.ToListAsync();

            var graphDto = GetGraphData(list);

            return Ok(graphDto);
        }

        /// <summary>
        /// Gets the graph data.
        /// </summary>
        /// <param name="list">The list of batches result.</param>
        /// <returns>A GraphDto.</returns>
        private static ChartDto GetGraphData(List<BatchesCount> list)
        {
            var dto = new ChartDto
            {
                Labels = new List<string>(),
                Datasets = new List<ChartDatasetDto>
                {
                    new ChartDatasetDto
                    {
                        Label = "Batches",
                        Data = new List<int>(),
                    }
                }
            };

            foreach (var item in list)
            {
                dto.Labels.Add(item.CreatedDate);
                dto.Datasets[0].Data.Add(item.Count);
            }

            return dto;
        }
    }
}
