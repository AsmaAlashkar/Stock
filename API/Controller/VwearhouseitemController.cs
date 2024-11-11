using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.MainWearHouse;
using Repository;
using Standard.Entities;
using Standard.DTOs;
using Repository.VMainWearhouseItem;
using Repository.VWearhouseWithSubHierarchy;
using System.Collections.Generic;
using System.Threading.Tasks;

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
       
        public async Task<ActionResult<List<ViewWearhouseWithSubHierarchyDTO>>>  getmainwearhouse()
        {
            var vwhi = await _vwh.GetAllMainWearHouse();

            var vmhidtos = _mapper.Map<List<ViewWearhouseWithSubHierarchyDTO>>(vwhi);

            // return the list of dtos
            return Ok(vmhidtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<ViewWearhouseWithSubHierarchyDTO>>> getmainwearhousebyid(int id)
        {

            var mainwearhouse = await _vwhw.GetMainWearHouseById(id);


            if (mainwearhouse == null)
            {
                return NotFound("mainwearhouse not found or has been deleted.");
            }

            // map the entity to a dto and return it
            return Ok(_mapper.Map<ViewWearhouseWithSubHierarchyDTO>(mainwearhouse));
        }

        [HttpGet]
        public async Task<ActionResult<List<ViewWearhouseWithSubHierarchyDTO>>> getsubwearhouse()
        {
            var vwhi = await _vwhw.GetAllSubWearHouse();

            var vmhidtos = _mapper.Map<List<ViewWearhouseWithSubHierarchyDTO>>(vwhi);

            // return the list of dtos
            return Ok(vmhidtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<ViewWearhouseWithSubHierarchyDTO>>> getsubwearhousebyid(int id)
        {

            var subwearhouse = await _vwhw.GetSubWearHouseById(id);


            if (subwearhouse == null)
            {
                return NotFound("subwearhouse not found or has been deleted.");
            }

            // map the entity to a dto and return it
            return Ok(_mapper.Map<ViewWearhouseWithSubHierarchyDTO>(subwearhouse));
        }
    }
}
