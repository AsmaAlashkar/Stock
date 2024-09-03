using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository;
using Standard.DTOs;
using Standard.Entities;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainWearhouseController : ControllerBase
    {
        private readonly IGenericRepository<MainWearhouse> _repo;
        private readonly IMapper _mapper;
        public MainWearhouseController(IGenericRepository<MainWearhouse> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<ActionResult<List<MainWearhouseDTO>>> GetMainWearhouse() {

            var mainwearhouses = await _repo.GetAll();

            return Ok(_mapper.Map<IReadOnlyList<MainWearhouseDTO>>(mainwearhouses));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MainWearhouseDTO?>> GetmainById(int id)
        {

            var mainwearhouse = await _repo.GetById(id);
            return Ok(_mapper.Map<MainWearhouseDTO>(mainwearhouse));

        }

        [HttpPost]
        public async Task<ActionResult> CreateNewMainWearhouse(MainWearhouseDTO mainwearhouse)
        {
            var mwh = _mapper.Map<MainWearhouse>(mainwearhouse);

            mwh.MainCreatedat = DateTime.Now;
            mwh.MainUpdatedat = null; 
            mwh.Delet = false;

            await _repo.CreateNew(mwh);
            return Ok("MainWearHouse Created Successfully");

        }
    }
}
