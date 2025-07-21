using AutoMapper;
using TicketsManager.Common.Dto;
using TicketsManager.Common.Entity;

namespace TicketsManager.Business.MappingConfig
{
    public class MappingConfigurations : Profile
    {
        public MappingConfigurations() 
        {
            CreateMap<UserEntity, UserDto>();
        }
    }
}
