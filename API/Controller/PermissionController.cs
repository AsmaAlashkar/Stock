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
