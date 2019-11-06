using Ahr.Data.MealPos;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
                var no = await GetMaxOrderNo(order.OrderDate, order.OrderType);
                order.OrderNo = no;
                order.CookStatus = 0;

                order.CaculateOrderAmt();

                db.OrderMaster.Add(order);
                await db.SaveChangesAsync();
                
                return await GetOrder(order.Id);
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

        public async Task<int> GetMaxOrderNo(DateTime orderDate, string orderType)
        {
            using (var db = NewDb())
            {
                var order = await db.OrderMaster
                    .Where(x => x.OrderDate == orderDate && x.OrderType == orderType)
                    .Select(x => x.OrderNo)
                    .DefaultIfEmpty(0)
                    .MaxAsync();
                var orderNo = order + 1;
                return orderNo;
            }
        }

        public async Task<OrderDto> GetOrder(int id)
        {
            using(var db= NewDb())
            {
                var order = await db.OrderMaster
                    .Include(x=>x.OrderDetail)
                    .FirstAsync(x=>x.Id==id);
                var dto = _mapper.Map<OrderMaster, OrderDto>(order);
                return dto;
            }
        }

        public async Task<List<string>> GetOrderType()
        {
            var result = await base.GetAppKeyValue("OrderType");
            return result.ToList();
        }

        public async Task<List<string>> GetSeat()
        {
            var result = await base.GetAppKeyValue("OrderType");
            return result.ToList();
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

        public async Task<OrderDto> UpdateOrder(int orderId, OrderDto model)
        {
            using(var db = NewDb())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    var idList = model.orderDetailDtos.Select(x => x.Id).ToList();

                    var deleteDetails = await db.OrderDetail
                        .Where(x => x.MasterId == orderId && !idList.Contains(x.Id))
                        .ToListAsync();

                    db.OrderDetail.RemoveRange(deleteDetails);
                    await db.SaveChangesAsync();

                    //db.Database.ExecuteSqlCommand("Delete OrderDetail Where MasterId={0} and Id Not in({1})", orderId, String.Join(",", idList));

                    //_mapper.Map<IEnumerable<OrderDetailDto>,IEnumerable<OrderDetail>>(orderDetailDto, orderDetail);

                    //db.OrderDetail.UpdateRange(orderDetail);

                    //orderDetail = await db.OrderDetail
                    //    .Where(x => x.MasterId == orderId)
                    //    .ToListAsync();

                    var order = await db.OrderMaster
                        .Include(x => x.OrderDetail)
                        .FirstAsync(x => x.Id == orderId);

                    _mapper.Map<OrderDto, OrderMaster>(model, order);

                    order.CaculateOrderAmt();
                    //////db.OrderDetail.UpdateRange(order.OrderDetail);
                    db.OrderMaster.Update(order);
                    await db.SaveChangesAsync();
                    transaction.Commit();
                    return await GetOrder(orderId);
                }

            }
        }
    }
}
