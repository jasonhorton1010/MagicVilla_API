using AutoMapper;
using MagicVilla_WebProject.Models;
using MagicVilla_WebProject.Models.Dto;
using MMagicVilla_WebProjectModels.Dto;

namespace MagicVilla_WebProject;

public class MappingConfig : Profile
{
    public MappingConfig()
    {

        CreateMap<VillaDTO, VillaCreateDTO>().ReverseMap();
        CreateMap<VillaDTO, VillaUpdateDTO>().ReverseMap();

        CreateMap<VillaNumberDTO, VillaNumberCreateDTO>().ReverseMap();
        CreateMap<VillaNumberDTO, VillaNumberUpdateDTO>().ReverseMap();
    }
}