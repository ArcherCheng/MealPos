//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Ahr.Service.MealPos
//{
//    public class AutoMapperProfiles : AutoMapper.Profile
//    {
//        public AutoMapperProfiles()
//        {
//            CreateMap<Member, MemberListDto>().ReverseMap();

//            CreateMap<RegisterDto, Member>();

//            CreateMap<Member, UserToReturnDto>();

//            CreateMap<Member, MemberEditDto>()
//            .ForMember(dest => dest.Introduction, opt => opt.MapFrom(src => src.MemberDetail.Introduction))
//            .ForMember(dest => dest.LikeCondition, opt => opt.MapFrom(src => src.MemberDetail.LikeCondition))
//            .ReverseMap();
//        }
//    }
//}
