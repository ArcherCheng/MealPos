using Ahr.Data.MealPos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ahr.Service.MealPos
{
    public class AutoMapperProfiles : AutoMapper.Profile
    {
        public AutoMapperProfiles()
        {
            //CreateMap<TSource, TDestination>
            CreateMap<AppUser, UserListDto>().ReverseMap();
            CreateMap<RegisterDto, AppUser>();
            CreateMap<AppUser, UserToReturnDto>();

            //CreateMap<Customer, CustomerDto>()
            //    .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<CustomerDto, Customer>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ReverseMap();

            //    CreateMap<Member, MemberEditDto>()
            //    .ForMember(dest => dest.Introduction, opt => opt.MapFrom(src => src.MemberDetail.Introduction))
            //    .ForMember(dest => dest.LikeCondition, opt => opt.MapFrom(src => src.MemberDetail.LikeCondition))
            //    .ReverseMap();
        }
    }
}
