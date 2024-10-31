using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.ReportRepo;
using Standard.Entities;
using System;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportRepository _report;

        public ReportController(IReportRepository report)
        {
            _report = report;
        }
        [HttpGet("getAllItemsQuantitiesInAllSubs")]
        public async Task<ActionResult<List<Quantity>>> getAllItemsQuantitiesInAllSubs()
        {
            var quantities = await _report.getAllItemsQuantitiesInAllSubs();
            if (quantities == null || quantities.Count == 0)
            {
                return NotFound("No quantities Found!");
            }
            return Ok(quantities);
        }
        [HttpGet("getAllItemsQuantitiesBySubId")]
        public async Task<ActionResult<List<Quantity>>> getAllItemsQuantitiesBySubId(int subId)
        {
            var quantities = await _report.getAllItemsQuantitiesBySubId(subId);
            if (quantities == null || quantities.Count == 0)
            {
                return NotFound("No quantities Found!");
            }
            return Ok(quantities);
        }

    }
}
