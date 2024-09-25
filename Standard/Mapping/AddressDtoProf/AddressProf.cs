using AutoMapper;
using Standard.DTOs;
using Standard.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Standard.Mapping.AddressDtoProf
{
    public class AddressProf : Profile
    {
        public AddressProf()
        {
            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}
