using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.MainWearHouse;
using Standard.DTOs;
using Standard.Entities;

namespace API.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MainWearhouseController : ControllerBase
    {
        private readonly IGenericRepository<MainWearhouse> _repo;
        private readonly IMWHRepository _mwh;
        private readonly IMapper _mapper;
        public MainWearhouseController(IGenericRepository<MainWearhouse> repo,
            IMWHRepository mWH,IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
            _mwh = mWH;
        }

        [HttpGet]
        public async Task<ActionResult<List<MainWearhouseDTO>>> GetMainWearhouse()
        {
            
            var mainwearhouses = await _mwh.GetAllMainWearHouse();

         
            var mainwearhouseDtos = _mapper.Map<List<MainWearhouseDTO>>(mainwearhouses);

            // Return the list of DTOs
            return Ok(mainwearhouseDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MainWearhouseDTO?>> GetMainWearhouseById(int id)
        {
           
            var mainwearhouse = await _mwh.GetMainWearHouseById(id);

            
            if (mainwearhouse == null)
            {
                return NotFound("MainWearhouse not found or has been deleted.");
            }

            // Map the entity to a DTO and return it
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


        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateMainWearHouse(int id, [FromForm] MainWearhouseDTO mainWearhouseDTO)
        {
            try
            {
                var existingItem = await _repo.GetById(id);

                if (existingItem == null )
                {
                    return NotFound($"MainWearHouse with ID {id} not found");
                }
                // Check if the Delet field is true and return NotFound
                if (mainWearhouseDTO.Delet == true)
                {
                    return NotFound($"MainWearHouse with ID {id} not found");
                }

                // Update only the fields that are provided in the DTO
                if (!string.IsNullOrEmpty(mainWearhouseDTO.MainName))
                {
                    existingItem.MainName = mainWearhouseDTO.MainName;
                }

                if (!string.IsNullOrEmpty(mainWearhouseDTO.MainDescription))
                {
                    existingItem.MainDescription = mainWearhouseDTO.MainDescription;
                }

                if (!string.IsNullOrEmpty(mainWearhouseDTO.MainAdderess))
                {
                    existingItem.MainAdderess = mainWearhouseDTO.MainAdderess;
                }

                if (mainWearhouseDTO.Delet.HasValue)
                {
                    existingItem.Delet = false;
                }

                existingItem.MainUpdatedat = DateTime.Now;

                await _repo.Update(existingItem);

                return Ok("MainWearHouse updated successfully");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                return BadRequest($"Error updating MainWearHouse: {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> SoftDeleteMainWearHouse(int id)
        {
            try
            {
                var existingItem = await _repo.GetById(id);

                if (existingItem == null)
                {
                    return NotFound($"MainWearHouse with ID {id} not found");
                }

                // Set the Delet attribute to true
                existingItem.Delet = true;
                existingItem.MainUpdatedat = DateTime.Now; // Update the timestamp

                await _repo.Update(existingItem);

                return Ok("MainWearHouse deleted successfully");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                return BadRequest($"Error deleting MainWearHouse: {ex.Message}");
            }
        }




    }



}
