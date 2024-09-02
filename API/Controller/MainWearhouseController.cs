using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainWearhouseController : ControllerBase
    {
        [HttpGet]
        public string GetWearhouse() {
            return "this is the test";
        }
    }
}
