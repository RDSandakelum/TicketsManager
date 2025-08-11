using AutoMapper;
using TicketsManager.Business.Actions.Users;
using TicketsManager.Common.Dto;
using TicketsManager.Common.Entity;

namespace TicketsManager.Business.MappingConfig
{
    public class MappingConfigurations : Profile
    {
        public MappingConfigurations() 
        {
            CreateMap<UserEntity, UserDto>();
            CreateMap<UserEntity, LoginUserResponseDto>();
            CreateMap<UpdateUserCommand, UserEntity>();
        }
    }
}
