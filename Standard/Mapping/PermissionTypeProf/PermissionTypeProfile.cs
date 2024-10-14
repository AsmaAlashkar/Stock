using AutoMapper;
using Standard.DTOs;
using Standard.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Standard.Mapping.PermissionTypeProf
{
    public class PermissionTypeProfile : Profile
    {
        public PermissionTypeProfile()
        {
            CreateMap<PermissionType, PermissionTypeDto>().ReverseMap();
            CreateMap<PermissionTypeDto, PermissionType>().ReverseMap();
        }
    }
}
