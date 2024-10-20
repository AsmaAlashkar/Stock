using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.MainWearHouse;
using Repository;
using Standard.Entities;
using Standard.DTOs;
using Repository.VMainWearhouseItem;
using Repository.VWearhouseWithSubHierarchy;

namespace API.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VwearhouseitemController : ControllerBase
    {
        private readonly IGenericRepository<ViewWearhouseItem> _repo;
        private readonly IMWHRepository _vwh;
        private readonly IVWHIWHRepository _vwhw;
        private readonly IMapper _mapper;
        public VwearhouseitemController(IGenericRepository<ViewWearhouseItem> repo,
            IMWHRepository vWH,
             IVWHIWHRepository vwhw
            , IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
            _vwh = vWH;
            _vwhw = vwhw;
        }

        [HttpGet]
        public async Task<ActionResult<List<ViewWearhouseWithSubHierarchyDTO>>> GetMainWearhouse()
        {
            var vwhi = await _vwh.GetAllMainWearHouse();

            var vmhiDtos = _mapper.Map<List<ViewWearhouseWithSubHierarchyDTO>>(vwhi);

            // Return the list of DTOs
            return Ok(vmhiDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ViewWearhouseWithSubHierarchyDTO?>> GetMainWearhouseById(int id)
        {

            var mainwearhouse = await _vwhw.GetMainWearHouseById(id);


            if (mainwearhouse == null)
            {
                return NotFound("MainWearhouse not found or has been deleted.");
            }

            // Map the entity to a DTO and return it
            return Ok(_mapper.Map<ViewWearhouseWithSubHierarchyDTO>(mainwearhouse));
        }

        [HttpGet]
        public async Task<ActionResult<List<ViewWearhouseWithSubHierarchyDTO>>> GetSubWearhouse()
        {
            var vwhi = await _vwhw.GetAllSubWearHouse();

            var vmhiDtos = _mapper.Map<List<ViewWearhouseWithSubHierarchyDTO>>(vwhi);

            // Return the list of DTOs
            return Ok(vmhiDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ViewWearhouseWithSubHierarchyDTO?>> GetSubWearhouseById(int id)
        {

            var subwearhouse = await _vwhw.GetSubWearHouseById(id);


            if (subwearhouse == null)
            {
                return NotFound("SubWearhouse not found or has been deleted.");
            }

            // Map the entity to a DTO and return it
            return Ok(_mapper.Map<ViewWearhouseWithSubHierarchyDTO>(subwearhouse));
        }
    }
}
