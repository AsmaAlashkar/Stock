using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Standard.DTOs;
using Standard.Entities;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionTypeController : ControllerBase
    {
        private readonly GenericRepository<PermissionType> _permt;
        private readonly IMapper _mapper;
        public PermissionTypeController(GenericRepository<PermissionType> permt, IMapper mapper)
        {
            _permt = permt;
            _mapper = mapper;
        }

        [HttpGet("GetAllPermissionTypes")]
        public async Task<ActionResult<IEnumerable<PermissionTypeDto>>> GetAllPermissionTypes()
        {
            var permissionTypes = await _permt.GetAll();

            var permissionTypeDtos = _mapper.Map<IEnumerable<PermissionTypeDto>>(permissionTypes);

            return Ok(permissionTypeDtos);
        }
    }

}
