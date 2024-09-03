using AutoMapper;
using Standard.DTOs;
using Standard.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Standard.Mapping.SubwearhouseProf
{
    public class SubWearhouseProfile : Profile
    {
        public SubWearhouseProfile()
        {
            CreateMap<SubWearhouse, SubWearHouseDTO>().ReverseMap();
            CreateMap<SubWearHouseDTO, SubWearhouse>().ReverseMap();
        }
    }
}
