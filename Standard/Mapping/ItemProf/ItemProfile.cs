using AutoMapper;
using Standard.DTOs;
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
            CreateMap<Item, ItemDto>()
                .ForMember(dest => dest.CatNameEn, opt => opt.MapFrom(src => src.CatFkNavigation.CatNameEn))
                .ForMember(dest => dest.UnitName, opt => opt.MapFrom(src => src.UniteFkNavigation.UnitName))
                .ForMember(dest => dest.SubName, opt => opt.MapFrom(src => src.SubFkNavigation.SubName))
                .ReverseMap();
            //CreateMap<Item, ItemDto>().ReverseMap();
            //CreateMap<ItemDto, Item>().ReverseMap();
        }
    }
}
