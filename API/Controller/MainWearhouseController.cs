using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Standard.Entities;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainWearhouseController : ControllerBase
    {
        private readonly StockContext _context;
        public MainWearhouseController(StockContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<MainWearhouse>>> GetWearhouse() {

            var mainwearhouse = await _context.MainWearhouses.ToListAsync();

            return mainwearhouse;
        }
    }
}
