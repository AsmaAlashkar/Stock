using Standard.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.PermissionRepo
{
    public interface IPermissionRepository
    {
        Task AddPermissionAsync(PermissionDto permissionDto);
    }
}
