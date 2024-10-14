using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Repository.PermissionType;
using Repository.SubWearHouse;
using Standard.DTOs;
using Standard.Entities;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionTypeController : ControllerBase
    {
       
        private readonly IPermissionTypeRepository _pt;
        private readonly IMapper _mapper;
        public PermissionTypeController(IMapper mapper,
             IPermissionTypeRepository pt)
        {
            
            _mapper = mapper;
            _pt = pt;
        }

        [HttpGet("GetAllPermissionTypes")]
        public async Task<ActionResult<IList<PermissionTypeDto>>> GetAllPermissionTypes()
        {
            var permissiontype = await _pt.GetPermissionsTypes();

            var permissiontypeDtos = _mapper.Map<List<PermissionTypeDto>>(permissiontype);

            return Ok(permissiontypeDtos);
        }
    }

}
