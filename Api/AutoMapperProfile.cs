using AutoMapper;
using CheatCodesUI2022.Shared;
using Dtos;

namespace Api
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CategoryDto, CategoryVM>();
        }
    }
}
