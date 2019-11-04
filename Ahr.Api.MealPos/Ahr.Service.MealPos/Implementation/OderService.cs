using Ahr.Data.MealPos;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ahr.Service.MealPos
{
    public class OrderService : AppBaseService, IOrderService
    {
        private readonly IMapper _mapper;

        public OrderService(IMapper mapper)
        {
            this._mapper = mapper;
        }
        public async Task<OrderDto> CreateOrder(OrderDto model)
        {
            using(var db = NewDb())
            {
                var order = _mapper.Map<OrderDto, OrderMaster>(model);
                db.OrderMaster.Add(order);
                await db.SaveChangesAsync();
                var dto = _mapper.Map<OrderMaster,OrderDto>(order);
                return dto;
            }
        }

        public async Task DeleteOrder(int id)
        {
            using(var db = NewDb())
            {
                var order = await db.OrderMaster.FindAsync(id);
                if (order == null)
                    return;

                db.OrderDetail.RemoveRange(order.OrderDetail);
                db.OrderMaster.Remove(order);
                await db.SaveChangesAsync();
            }
        }

        public async Task<OrderDto> GetOrder(int id)
        {
            using(var db= NewDb())
            {
                var order = await db.OrderMaster.Include(x=>x.OrderDetail).FirstAsync(x=>x.Id==id);
                var dto = _mapper.Map<OrderMaster, OrderDto>(order);
                return dto;
            }
        }

        public async Task<IEnumerable<OrderDto>> OrderList()
        {
            using(var db = NewDb())
            {
                var orders = await db.OrderMaster.Include(x=>x.OrderDetail).ToListAsync();
                var dtos = _mapper.Map<IEnumerable<OrderMaster>, IEnumerable<OrderDto>>(orders);
                return dtos;
            }
        }

        public async Task<OrderDto> UpdateOrder(int id, OrderDto model)
        {
            using(var db = NewDb())
            {
                var order = await db.OrderMaster.FindAsync(id);
                _mapper.Map<OrderDto, OrderMaster>(model, order);
                db.OrderDetail.UpdateRange(order.OrderDetail);
                db.OrderMaster.Update(order);
                await db.SaveChangesAsync();
                var dto = _mapper.Map<OrderMaster, OrderDto>(order);
                return dto;
            }
        }
    }
}
