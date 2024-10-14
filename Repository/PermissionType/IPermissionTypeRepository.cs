﻿using Standard.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.PermissionType
{
    public interface IPermissionTypeRepository
    {
        Task<List<Standard.Entities.PermissionType>> GetPermissionsTypes();
    }
}
