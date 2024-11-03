using Repository.Pagination;
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
        Task<List<DisplayPermissionsDto>> GetAllPermissions();
        Task<PaginatedResult<DisplayPermissionsDto>> GetAllPermissionsWithPagination(int pageNumber, int pageSize);
        Task<List<DisplayPermissionsDto>> GetPermissionsByDate(DateTime date);
        Task<DisplayPermissionsDto> GetPermissionById(int id);
        Task<List<DisplayPermissionsDto>> GetPermissionByTypeId(int typeId);

    }
}
