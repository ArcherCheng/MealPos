using Ahr.Data.MealPos;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ahr.Service.MealPos
{
    public static class AutoMapperExtensions
    {
        static readonly IMapper mapper;

        public static CustomerDto ToDto(this Customer entity)
        {
            return mapper.Map<Customer, CustomerDto>(entity);
            //var dto = db.Customer.Where(exp).ToDto;
        }

        public static Customer ToEntity(this CustomerDto dto)
        {
            return mapper.Map<CustomerDto, Customer>(dto);
            //var entity = dto.ToEntity();
        }

        public static Customer ToEntity(this CustomerDto dto, Customer entity)
        {
            return mapper.Map<CustomerDto, Customer>(dto, entity);
            //dto.ToEntity(entity);
        }
    }
}
