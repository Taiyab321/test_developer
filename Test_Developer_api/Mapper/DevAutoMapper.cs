using AutoMapper;
using Developer_Test_Api.Dto;

namespace Test_Developer_api.Mapper
{
    public class DevAutoMapper : Profile
    {
        public DevAutoMapper()
        {
            CreateMap<AutocompletePostcodeDto, ReturnObjectDto>()
                .ForMember(dest => dest.Result, opts => opts.MapFrom(src => src.Result));
            CreateMap<LookupPostcodeDto, ReturnObjectDto>()
                .ForMember(dest => dest.Result, opts => opts.MapFrom(src => src.Result));
        }
    }
}
