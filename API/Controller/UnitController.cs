using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.UnitRepo;
using Standard.DTOs.UnitDtos;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitController : ControllerBase
    {
        private readonly IUnitRepository _unit;

        public UnitController(IUnitRepository unit)
        {
            _unit = unit;
        }
        [HttpGet("GetUnitsNames")]
        public async Task<ActionResult<List<DisplayUnitNameDto>>> GetUnitsNames()
        {
            var unitNames = await _unit.GetUnitsNames();

            if (unitNames == null || unitNames.Count == 0)
            {
                return NotFound("No units found.");
            }

            return Ok(unitNames);
        }
    }
}
