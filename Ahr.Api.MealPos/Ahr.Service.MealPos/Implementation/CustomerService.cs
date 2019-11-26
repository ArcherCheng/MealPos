using Ahr.Data.MealPos;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ahr.Service.MealPos
{
    public class CustomerService : BaseService, ICustomerService
    {
        private readonly IMapper _mapper;

        public CustomerService(IMapper mapper)
        {
            this._mapper = mapper;
        }

        public async Task<CustomerDto> CreateCustomer(CustomerDto model)
        {
            using(var db = NewDb())
            {
                var customer = _mapper.Map<CustomerDto, Customer>(model);
                db.Customer.Add(customer);
                await db.SaveChangesAsync();
                var dto = _mapper.Map<Customer, CustomerDto>(customer);
                return dto;
            }
        }

        public async Task<IEnumerable<CustomerDto>> CustomerList()
        {
            using (var db = NewDb())
            {
                var customers = await db.Customer.ToListAsync();
                var dtos = _mapper.Map<IEnumerable<CustomerDto>>(customers);
                var service = services.GetService<IMapper>();
                var order = await service.OrderList();
                return dtos;
            }
        }

        public async Task DeleteCustomer(int id)
        {
            using (var db = NewDb())
            {
                var customer = await db.Customer.FindAsync(id);
                if (customer == null)
                    return;

                db.Customer.Remove(customer);
                await db.SaveChangesAsync();
            }
        }

        public async Task<CustomerDto> GetCustomer(int id)
        {
            using (var db = NewDb())
            {
                var customer = await db.Customer.FindAsync(id);
                if (customer == null)
                    return null;

                var dto = _mapper.Map<Customer, CustomerDto>(customer);
                return dto;
            }
        }

        public async Task<CustomerDto> UpdateCustomer(int id, CustomerDto model)
        {
            using (var db = NewDb())
            {
                var customer = await db.Customer.FindAsync(id);
                if (customer == null)
                    return null;

                _mapper.Map<CustomerDto, Customer>(model, customer);
                db.Customer.Update(customer);
                await db.SaveChangesAsync();
                var dto = _mapper.Map<Customer, CustomerDto>(customer);
                return dto;
            }
        }
    }
}
