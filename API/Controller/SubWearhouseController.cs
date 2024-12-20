﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.MainWearHouse;
using Repository;
using Standard.Entities;
using Repository.SubWearHouse;
using Standard.DTOs;
using Repository.VMainWearhouseItem;
using Repository.VWearhouseWithSubHierarchy;
using Standard.DTOs.ItemDtos;
using System.Security.Policy;
using Standard.DTOs.SubDto;

namespace API.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SubWearhouseController : ControllerBase
    {
        private readonly IGenericRepository<SubWearhouse> _repo;
        private readonly ISWHRepository _swh;
        private readonly IVWHIRepository _vwh;
        private readonly IVWHIWHRepository _vwhw;
        private readonly IMapper _mapper;
        public SubWearhouseController(IGenericRepository<SubWearhouse> repo,
            ISWHRepository sWH, IVWHIRepository vWH,
             IVWHIWHRepository vwhw
            , IMapper mapper)
        {
            _repo = repo;
            _swh = sWH;
            _vwh = vWH;
            _vwhw = vwhw;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ViewWearhouseWithSubHierarchyDTO>>> GetSubWearhouse()
        {

            var subwearhouse = await _vwhw.GetAllSubWearHouse();

            var vmhiDtos = _mapper.Map<List<ViewWearhouseWithSubHierarchyDTO>>(subwearhouse);

            // Return the list of DTOs
            return Ok(vmhiDtos);
        }
        [HttpGet("GetSubNames")]
        public async Task<ActionResult<List<SubNamesDto>>> GetSubsNames()
        {
            var subNames = await _swh.GetSubsNames();

            if (subNames == null || subNames.Count == 0)
            {
                return NotFound("No Subs found.");
            }

            return Ok(subNames);
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

        [HttpGet("{id}")]
        public async Task<ActionResult<ViewWearhouseWithSubHierarchyDTO?>> GetSubWearhouseByMainId(int id)
        {

            var subwearhouse = await _vwhw.GetAllSubByMainId(id);


            if (subwearhouse == null)
            {
                return NotFound("SubWearhouse not found or has been deleted.");
            }

            // Map the entity to a DTO and return it
            return Ok(_mapper.Map<List<ViewWearhouseWithSubHierarchyDTO>>(subwearhouse));
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

        [HttpGet]
        public async Task<ActionResult<List<SubWearHouseDTO>>> GetSubNamesAndParentIdsByMainFk(int mainFk)
        {
            var result = await _swh.GetSubNamesAndParentIdsByMainFk(mainFk);

            if (result == null || !result.Any())
            {
                return NotFound("No sub-warehouses found for the given MainFk.");
            }

            return Ok(result);
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateSubWearHouse(int id, [FromBody] SubWearHouseDTO subWearHouseDTO)
        {
            try
            {
                var existingItem = await _repo.GetById(id);

                if (existingItem == null)
                {
                    return NotFound($"SubWearHouse with ID {id} not found");
                }

                // Update only the fields that are provided in the DTO
                if (!string.IsNullOrEmpty(subWearHouseDTO.SubNameEn))
                {
                    existingItem.SubNameEn = subWearHouseDTO.SubNameEn;
                }
                if (!string.IsNullOrEmpty(subWearHouseDTO.SubNameAr))
                {
                    existingItem.SubNameAr = subWearHouseDTO.SubNameAr;
                }

                if (!string.IsNullOrEmpty(subWearHouseDTO.SubDescriptionEn))
                {
                    existingItem.SubDescriptionEn = subWearHouseDTO.SubDescriptionEn;
                }
                if (!string.IsNullOrEmpty(subWearHouseDTO.SubDescriptionAr))
                {
                    existingItem.SubDescriptionAr = subWearHouseDTO.SubDescriptionAr;
                }

                if (!string.IsNullOrEmpty(subWearHouseDTO.SubAddressEn))
                {
                    existingItem.SubAddressEn = subWearHouseDTO.SubAddressEn;
                }
                if (!string.IsNullOrEmpty(subWearHouseDTO.SubAddressAr))
                {
                    existingItem.SubAddressAr = subWearHouseDTO.SubAddressAr;
                }

                if (subWearHouseDTO.Delet.HasValue)
                {
                    existingItem.Delet = false;
                }

                existingItem.SubUpdatedat = DateTime.Now;

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
