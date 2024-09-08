using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.MainWearHouse;
using Repository;
using Standard.Entities;
using Repository.SubWearHouse;
using Standard.DTOs;
using Repository.VMainWearhouseItem;

namespace API.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SubWearhouseController : ControllerBase
    {
        private readonly IGenericRepository<SubWearhouse> _repo;
        private readonly ISWHRepository _swh;
        private readonly IVWHIRepository _vwh;
        private readonly IMapper _mapper;
        public SubWearhouseController(IGenericRepository<SubWearhouse> repo,
            ISWHRepository sWH, IVWHIRepository vWH, IMapper mapper)
        {
            _repo = repo;
            _swh = sWH;
            _vwh = vWH;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ViewWearhouseItemDTO>>> GetSubWearhouse()
        {

            var subwearhouse = await _vwh.GetAllSubWearHouse();

            var vmhiDtos = _mapper.Map<List<ViewWearhouseItemDTO>>(subwearhouse);

            // Return the list of DTOs
            return Ok(vmhiDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ViewWearhouseItemDTO  ?>> GetSubWearhouseById(int id)
        {

            var subwearhouse = await _vwh.GetSubWearHouseById(id);


            if (subwearhouse == null)
            {
                return NotFound("SubWearhouse not found or has been deleted.");
            }

            // Map the entity to a DTO and return it
            return Ok(_mapper.Map<ViewWearhouseItemDTO>(subwearhouse));
        }

        [HttpPost]
        public async Task<ActionResult> CreateNewSubWearhouse(SubWearHouseDTO subwearhouse)
        {
            var swh = _mapper.Map<SubWearhouse>(subwearhouse);

            swh.SubCreatedat = DateTime.Now;
            swh.SubUpdatedat = null;
            swh.Delet = false;

            await _repo.CreateNew(swh);
            return Ok("SubWearHouse Created Successfully");

        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateSubWearHouse(int id, [FromForm] SubWearHouseDTO subWearhouseDTO)
        {
            try
            {
                var existingItem = await _repo.GetById(id);

                if (existingItem == null)
                {
                    return NotFound($"SubWearHouse with ID {id} not found");
                }
                // Check if the Delet field is true and return NotFound
                if (subWearhouseDTO.Delet == true)
                {
                    return NotFound($"MainWearHouse with ID {id} not found");
                }

                // Update only the fields that are provided in the DTO
                if (!string.IsNullOrEmpty(subWearhouseDTO.SubName))
                {
                    existingItem.SubName = subWearhouseDTO.SubName;
                }

                if (!string.IsNullOrEmpty(subWearhouseDTO.SubDescription))
                {
                    existingItem.SubDescription = subWearhouseDTO.SubDescription;
                }

                if (!string.IsNullOrEmpty(subWearhouseDTO.SubAddress))
                {
                    existingItem.SubAddress = subWearhouseDTO.SubAddress;
                }

                if (subWearhouseDTO.Delet.HasValue)
                {
                    existingItem.Delet = false;
                }

                existingItem.SubUpdatedat = DateTime.Now;

                await _repo.Update(existingItem);

                return Ok("SubWearHouse updated successfully");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                return BadRequest($"Error updating SubWearHouse: {ex.Message}");
            }
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> SoftDeleteSubWearHouse(int id)
        {
            try
            {
                var existingItem = await _repo.GetById(id);

                if (existingItem == null)
                {
                    return NotFound($"SubWearHouse with ID {id} not found");
                }

                // Set the Delet attribute to true
                existingItem.Delet = true;
                existingItem.SubUpdatedat = DateTime.Now; // Update the timestamp

                await _repo.Update(existingItem);

                return Ok("SubWearHouse deleted successfully");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                return BadRequest($"Error deleting SubWearHouse: {ex.Message}");
            }
        }

    }
}
