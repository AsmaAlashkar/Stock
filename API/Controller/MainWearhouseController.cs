using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.MainWearHouse;
using Repository.VMainWearhouseItem;
using Repository.VWearhouseWithSubHierarchy;
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
        private readonly IVWHIRepository _vwh;
        private readonly IVWHIWHRepository _vwhw;
        private readonly IMapper _mapper;
        public MainWearhouseController(IGenericRepository<MainWearhouse> repo,
            IMWHRepository mWH, IVWHIRepository vWH,
            IVWHIWHRepository vwhw,
            IMapper mapper)
        { 
            _repo = repo;
            _mapper = mapper;
            _mwh = mWH;
            _vwh = vWH;
            _vwhw = vwhw;
        }

        [HttpGet]
        public async Task<ActionResult<List<ViewWearhouseWithSubHierarchyDTO>>> GetMainWearhouse()
        {

            var vwhi = await _vwhw.GetAllMainWearHouse();

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
            return Ok(_mapper.Map<List<ViewWearhouseWithSubHierarchyDTO>>(mainwearhouse));
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
        public async Task<ActionResult> UpdateMainWearHouse(int id, [FromBody] MainWearhouseDTO mainWearhouseDTO)
        {
            try
            {
                // Retrieve the existing MainWearhouse from the repository by its ID
                var existingItem = await _repo.GetById(id);

                // If the item doesn't exist, return a NotFound response
                if (existingItem == null)
                {
                    return NotFound($"MainWearHouse with ID {id} not found");
                }

                // Check if the Delet field is true and return NotFound if it is
                if (mainWearhouseDTO.Delet == true)
                {
                    return NotFound($"MainWearHouse with ID {id} has been marked for deletion");
                }

                // Set Delet to false during the update, just like in CreateNewMainWearhouse
                existingItem.Delet = false;

                // Update only the fields that are provided in the DTO, preserving existing values where not specified

                // Update MainNameEn if a value is provided in the DTO
                if (!string.IsNullOrEmpty(mainWearhouseDTO.MainNameEn))
                {
                    existingItem.MainNameEn = mainWearhouseDTO.MainNameEn;
                }

                // Update MainNameAr if a value is provided in the DTO
                if (!string.IsNullOrEmpty(mainWearhouseDTO.MainNameAr))
                {
                    existingItem.MainNameAr = mainWearhouseDTO.MainNameAr; // Corrected here
                }

                // Update MainDescriptionEn if a value is provided in the DTO
                if (!string.IsNullOrEmpty(mainWearhouseDTO.MainDescriptionEn))
                {
                    existingItem.MainDescriptionEn = mainWearhouseDTO.MainDescriptionEn;
                }

                // Update MainDescriptionAr if a value is provided in the DTO
                if (!string.IsNullOrEmpty(mainWearhouseDTO.MainDescriptionAr))
                {
                    existingItem.MainDescriptionAr = mainWearhouseDTO.MainDescriptionAr; // Corrected here
                }

                // Update MainAdderess if a value is provided in the DTO
                if (!string.IsNullOrEmpty(mainWearhouseDTO.MainAdderess))
                {
                    existingItem.MainAdderess = mainWearhouseDTO.MainAdderess;
                }

                // If the Delet flag is set to null, ensure it is not updated
                if (mainWearhouseDTO.Delet.HasValue)
                {
                    existingItem.Delet = false; // Keep Delet as false if it's not null
                }

                // Set the update timestamp
                existingItem.MainUpdatedat = DateTime.Now;

                // Save the updated entity to the repository
                await _repo.Update(existingItem);

                // Return a success response
                return Ok("MainWearHouse updated successfully");
            }
            catch (Exception ex)
            {
                // Catch any exceptions and return a BadRequest with the error message
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
