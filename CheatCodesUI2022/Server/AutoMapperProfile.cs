using AutoMapper;
using CheatCodesUI2022.Shared;
using Dtos;

namespace CheatCodesUI2022.Server
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CategoryDto, CategoryVM>();
        }
    }
}
