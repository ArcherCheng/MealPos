using Ahr.Data.MealPos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

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

            CreateMap<Meal, MealDto>()
                .ForMember(dest => dest.MealAddOnDtos, 
                opt => opt.MapFrom(src => src.MealAddOnRela.Select(rela =>rela.AddOn )));
            //new MealAddOnDto { Id = mr.AddOnId, AddOnName = mr.AddOn.AddOnName, AddPrice = mr.AddOn.AddPrice }

            CreateMap<MealDto, Meal>()
                .ForMember(dest => dest.MealAddOnRela, opt => opt.MapFrom(src => src.MealAddOnDtos))
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<MealAddOn, MealAddOnDto>()
                .ReverseMap();

            CreateMap<OrderMaster, OrderDto>()
                .ForMember(dest => dest.orderDetailDtos, opt => opt.MapFrom(src => src.OrderDetail))
                .ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<OrderDetail, OrderDetailDto>()
                .ReverseMap();

        }
    }
}
