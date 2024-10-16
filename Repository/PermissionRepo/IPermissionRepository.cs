﻿using Standard.DTOs;
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

        // Task AddPermissionAsync(PermissionDto permissionDto);
    }
}
