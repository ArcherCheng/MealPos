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

            CreateMap<CustomerDto, Customer>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Meal, MealDto>()
                .ForMember(dest => dest.MealAddOnDtos, 
                opt => opt.MapFrom(src => src.MealAddOnRela.Select(rela =>rela.AddOn )));
            //new MealAddOnDto { Id = mr.AddOnId, AddOnName = mr.AddOn.AddOnName, AddPrice = mr.AddOn.AddPrice }

            CreateMap<MealDto, Meal>()
                ///map 的方法一
                .ForMember(dest => dest.MealAddOnRela, opt => opt.MapFrom(src => src.MealAddOnDtos.Select(x=>new MealAddOnRela{ AddOnId = x.Id})))
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<MealAddOn, MealAddOnDto>()
                .ReverseMap();

            CreateMap<OrderMaster, OrderDto>()
                .ForMember(dest => dest.orderDetailDtos, opt => opt.MapFrom(src => src.OrderDetail));

            CreateMap<OrderDetail, OrderDetailDto>()
                .ReverseMap();

            CreateMap<OrderDto, OrderMaster>()
                .ForMember(dest => dest.OrderDetail, opt => opt.MapFrom(scr => scr.orderDetailDtos));
        }
    }
}
