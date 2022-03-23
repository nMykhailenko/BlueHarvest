using AutoMapper;
using BlueHarvest.Modules.Users.Core.Application.Users.Models.RequestModels;
using BlueHarvest.Modules.Users.Core.Application.Users.Models.ResponseModels;
using BlueHarvest.Modules.Users.Core.Domain.Entities;

namespace BlueHarvest.Modules.Users.Core.Application.Users.Mappings;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<CreateUserRequest, User>(MemberList.Destination)
            .ForMember(
                dest => dest.Id, 
                options => options.Ignore());

        CreateMap<User, UserResponse>();
    }
}