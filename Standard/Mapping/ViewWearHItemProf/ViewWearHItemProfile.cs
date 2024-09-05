using AutoMapper;
using Standard.DTOs;
using Standard.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Standard.Mapping.ViewWearHItemProf
{
    public class ViewWearHItemProfile : Profile
    {
        public ViewWearHItemProfile()
        {
            CreateMap<ViewWearhouseItem, ViewWearhouseItemDTO>().ReverseMap();
            CreateMap<ViewWearhouseItemDTO, ViewWearhouseItem>().ReverseMap();
        }
    }
}
