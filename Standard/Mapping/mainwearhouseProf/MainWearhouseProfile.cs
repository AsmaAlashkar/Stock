using AutoMapper;
using Standard.DTOs;
using Standard.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Standard.Mapping.mainwearhouseProf
{
    public class MainWearhouseProfile : Profile
    {
        public MainWearhouseProfile() {

            CreateMap<MainWearhouse, MainWearhouseDTO>().ReverseMap();
            CreateMap<MainWearhouseDTO, MainWearhouse>().ReverseMap();


        }
    }
}
