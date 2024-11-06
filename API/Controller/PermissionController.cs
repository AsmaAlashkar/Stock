using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Standard.Entities;
using Repository.PermissionRepo;
using Standard.DTOs.PermissionDto;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IGenericRepository<Permission> _repo;
        private readonly IPermissionRepository _permission;
        private readonly IMapper _mapper;

        public PermissionController(
            IGenericRepository<Permission> repo,
            IPermissionRepository permission,
            IMapper mapper)
        {
            _repo = repo;
            _permission = permission;
            _mapper = mapper;
        }
        [HttpGet("GetAllPermissions")]
        public async Task<ActionResult<List<PermissionDto>>> GetAllPermissions()
        {
            var permissions = await _permission.GetAllPermissions();
            if (permissions == null || permissions.Count == 0)
            {
                return NotFound("No Permissions Found!");
            }
            return Ok(permissions);
        }
        
        [HttpGet("GetAllPermissionsWithPagination")]
        public async Task<IActionResult> GetAllPermissionsWithPagination(int pageNumber = 1, int pageSize = 10)
        {
            var paginatedPermissions = await _permission.GetAllPermissionsWithPagination(pageNumber, pageSize);

            return Ok(paginatedPermissions);
        }

        [HttpGet("GetPermissionsByDate/{date}")]
        public async Task<ActionResult<List<PermissionDto>>> GetPermissionsByDate(DateTime date)
        {
            var permissions = await _permission.GetPermissionsByDate(date);
            if (permissions == null || permissions.Count == 0)
            {
                return NotFound("No Permissions Found!");
            }
            return Ok(permissions);
        }

        [HttpGet("GetPermissionByTypeId/{typeId}")]
        public async Task<ActionResult<List<PermissionDto>>> GetPermissionByTypeId(int typeId)
        {
            var permissions = await _permission.GetPermissionByTypeId(typeId);
            if (permissions == null || permissions.Count == 0)
            {
                return NotFound("No Permissions Found!");
            }
            return Ok(permissions);
        }

        [HttpGet("GetPermissionById/{id}")]
        public async Task<ActionResult<PermissionDto>> GetPermissionById(int id)
        {
            var permission = await _permission.GetPermissionById(id);
            if (permission == null)
            {
                return NotFound("No Permission with this id Found!");
            }
            return Ok(permission);
        }

        [HttpGet("GenerateNextPermissionCode")]
        public async Task<ActionResult<string>> GetNextPermissionCode()
        {
            var nextCode = await _permission.GenerateNextPermissionCode();
            return Ok(nextCode);
        }

        [HttpPost("CreatePermission")]
        public async Task<ActionResult> CreatePermission(PermissionDto permission)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _permission.AddPermission(permission);
                return Ok(new { Message = "Permission processed successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"Error: {ex.Message}" });
            }
        }

    }
}
