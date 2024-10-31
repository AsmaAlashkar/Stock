using Standard.DTOs.PermissionDto;
using Standard.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.PermissionRepo
{
    public interface IPermissionRepository
    {
        Task AddPermission(PermissionDto permissionDto);
        Task<List<PermissionDto>> GetAllPermissions();
        Task<List<PermissionDto>> GetPermissionsByDate(DateTime date);
        Task<PermissionDto> GetPermissionById(int id);
        Task<List<PermissionDto>> GetPermissionByTypeId(int typeId);

    }
}
