using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.MainWearHouse;
using Repository;
using Standard.Entities;
using Standard.DTOs;
using Repository.VMainWearhouseItem;

namespace API.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VwearhouseitemController : ControllerBase
    {
        private readonly IGenericRepository<ViewWearhouseItem> _repo;
        private readonly IVWHIRepository _vwh;
        private readonly IMapper _mapper;
        public VwearhouseitemController(IGenericRepository<ViewWearhouseItem> repo,
            IVWHIRepository vWH, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
            _vwh = vWH;
        }

        [HttpGet]
        public async Task<ActionResult<List<ViewWearhouseItemDTO>>> GetMainWearhouse()
        {
            var vwhi= await _vwh.GetAllMainWearHouse();

            var vmhiDtos = _mapper.Map<List<ViewWearhouseItemDTO>>(vwhi);

            // Return the list of DTOs
            return Ok(vmhiDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ViewWearhouseItemDTO?>> GetMainWearhouseById(int id)
        {

            var mainwearhouse = await _vwh.GetMainWearHouseById(id);


            if (mainwearhouse == null)
            {
                return NotFound("MainWearhouse not found or has been deleted.");
            }

            // Map the entity to a DTO and return it
            return Ok(_mapper.Map<ViewWearhouseItemDTO>(mainwearhouse));
        }

        [HttpGet]
        public async Task<ActionResult<List<ViewWearhouseItemDTO>>> GetSubWearhouse()
        {
            var vwhi = await _vwh.GetAllSubWearHouse();

            var vmhiDtos = _mapper.Map<List<ViewWearhouseItemDTO>>(vwhi);

            // Return the list of DTOs
            return Ok(vmhiDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ViewWearhouseItemDTO?>> GetSubWearhouseById(int id)
        {

            var subwearhouse = await _vwh.GetSubWearHouseById(id);


            if (subwearhouse == null)
            {
                return NotFound("SubWearhouse not found or has been deleted.");
            }

            // Map the entity to a DTO and return it
            return Ok(_mapper.Map<ViewWearhouseItemDTO>(subwearhouse));
        }
    }
}
