using AutoMapper;
using Standard.DTOs;
using Standard.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Standard.Mapping.ViewWearhouseWithSubHierarchyProf
{
    public class ViewWearhouseWithSubHierarchyProfile :Profile
    {
        public ViewWearhouseWithSubHierarchyProfile()
        {
            CreateMap<ViewMainWearhouseWithSubWearhouseHierarchy, ViewWearhouseWithSubHierarchyDTO>().ReverseMap();
            CreateMap<ViewWearhouseWithSubHierarchyDTO, ViewMainWearhouseWithSubWearhouseHierarchy>().ReverseMap();
        }
    }
}
