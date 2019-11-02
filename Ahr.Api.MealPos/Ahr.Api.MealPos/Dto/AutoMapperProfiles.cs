using Ahr.Data.MealPos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ahr.Api.MealPos
{
    public class AutoMapperProfiles : AutoMapper.Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, UserListDto>().ReverseMap();
            CreateMap<RegisterDto, AppUser>();
            CreateMap<AppUser, UserToReturnDto>();
            //    CreateMap<Member, MemberEditDto>()
            //    .ForMember(dest => dest.Introduction, opt => opt.MapFrom(src => src.MemberDetail.Introduction))
            //    .ForMember(dest => dest.LikeCondition, opt => opt.MapFrom(src => src.MemberDetail.LikeCondition))
            //    .ReverseMap();
        }
    }
}
