using AutoMapper;
using Standard.DTOs.ItemDtos;
using Standard.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Standard.Mapping.ItemProf
{
    public class ItemProfile:Profile
    {
        public ItemProfile()
        {
            CreateMap<Item, CreateItemDto>().ReverseMap();
            CreateMap<CreateItemDto, Item>().ReverseMap();
        }
    }
}
