using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Standard.Entities;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodeFormatController : ControllerBase
    {
        private readonly StockContext _context;

        public CodeFormatController(StockContext context)
        {
            _context = context;
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateCodeFormat([FromBody] CodeFormat codeFormat)
        {
            if (string.IsNullOrEmpty(codeFormat.Format))
            {
                return BadRequest("Invalid code format.");
            }
            var existingFormat = await _context.CodeFormats
                .AnyAsync(cf => cf.Format == codeFormat.Format);

            if (existingFormat)
            {
                return Conflict("This code format already exists.");
            }

            bool hasActiveFormat = await _context.CodeFormats.AnyAsync(cf => cf.IsActive);

            codeFormat.IsActive = !hasActiveFormat;

            _context.CodeFormats.Add(codeFormat);
            await _context.SaveChangesAsync();

            return Ok("Code format created successfully.");
        }

        [HttpPost("SetActiveCodeFormat/{id}")]
        public async Task<IActionResult> SetActiveCodeFormat(int id)
        {
            var allFormats = await _context.CodeFormats.ToListAsync();
            foreach (var format in allFormats)
            {
                format.IsActive = format.Id == id; 
            }

            await _context.SaveChangesAsync();
            return Ok("Active code format updated successfully.");
        }


    }
}
