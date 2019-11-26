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
    public class OrderService : BaseService, IOrderService
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
                    ////方法一
                    //var newIdList = model.orderDetailDtos.Select(x => x.Id).ToList();
                    //var oldIdList = db.OrderDetail.Where(x=>x.MasterId == orderId).Select(x => x.Id).ToList();

                    //var deleteDetails = await db.OrderDetail
                    //    .Where(x => x.MasterId == orderId && !newIdList.Contains(x.Id))
                    //    .ToListAsync();
                    //if (deleteDetails.Count > 0)
                    //    db.OrderDetail.RemoveRange(deleteDetails);
                    //var dtos = model.orderDetailDtos
                    //    .Where(x => oldIdList.Contains(x.Id))
                    //    .ToList();
                    //if (dtos.Count > 0)
                    //{
                    //    var updateDetails = _mapper.Map<IEnumerable<OrderDetailDto>, IEnumerable<OrderDetail>>(dtos);
                    //    db.OrderDetail.UpdateRange(updateDetails);
                    //}
                    //dtos = model.orderDetailDtos.Where(x => x.Id == 0).ToList();
                    //if (dtos.Count > 0)
                    //{
                    //    var insertDetails = _mapper.Map<IEnumerable<OrderDetailDto>, IEnumerable<OrderDetail>>(dtos);
                    //    db.OrderDetail.AddRange(insertDetails);
                    //}
                    //await db.SaveChangesAsync();


                    ////// 方法二
                    //var newIdList = model.orderDetailDtos.Select(x => x.Id).ToList();
                    //var orderDetails = db.OrderDetail.Where(x => x.MasterId == orderId);
                    //foreach (var od in orderDetails)
                    //{
                    //    if (newIdList.Contains(od.Id))
                    //    {
                    //        var dto = model.orderDetailDtos.FirstOrDefault(x => x.Id == od.Id);
                    //        _mapper.Map<OrderDetailDto, OrderDetail>(dto,od);
                    //        db.OrderDetail.Update(od);
                    //    }
                    //    else  //if (!newIdList.Contains(od.Id))
                    //    {
                    //        db.OrderDetail.Remove(od);
                    //    }
                    //}
                    //foreach (var dto in model.orderDetailDtos)
                    //{
                    //    if (dto.Id == 0)
                    //    {
                    //        var insertDetail = _mapper.Map<OrderDetailDto, OrderDetail>(dto);
                    //        db.OrderDetail.Add(insertDetail);
                    //    }
                    //}
                    //await db.SaveChangesAsync();


                    ////方法三
                    ////全部刪除
                    //var orderDetails = db.OrderDetail.Where(x => x.MasterId == orderId);
                    //db.OrderDetail.RemoveRange(orderDetails);
                    ////全部新增
                    //var insertDetails = _mapper.Map<IEnumerable<OrderDetailDto>, IEnumerable<OrderDetail>>(model.orderDetailDtos);
                    //db.OrderDetail.AddRange(insertDetails);
                    ////存檔
                    //await db.SaveChangesAsync();

                    ////方法四
                    //在DTO物件加入一個欄位 CrudStatus,記錄每一筆明細修改的變化
                    foreach (var dto in model.orderDetailDtos)
                    {
                        switch (dto.CrudStatus)
                        {
                            case CrudStatus.Create:
                                var insertDetail = _mapper.Map<OrderDetailDto, OrderDetail>(dto);
                                db.OrderDetail.Add(insertDetail);
                                break;
                            case CrudStatus.Update:
                                var updateDetail = _mapper.Map<OrderDetailDto, OrderDetail>(dto);
                                db.OrderDetail.Update(updateDetail);
                                break;
                            case CrudStatus.Delete:
                                var deleteDetail = await db.OrderDetail.FindAsync(dto.Id);
                                db.OrderDetail.Remove(deleteDetail);
                                break;
                            default:
                                //no changed,none do
                                break;
                        }
                    }
                    await db.SaveChangesAsync();

                    //重新計算訂單主檔金額及稅額
                    var order = await db.OrderMaster
                        .Include(x => x.OrderDetail)
                        .FirstAsync(x => x.Id == orderId);
                    order.CaculateOrderAmt();
                    await db.SaveChangesAsync();
                    //commit 寫入到資料庫
                    transaction.Commit();
                    return await GetOrder(orderId);
                }
            }
        }
    }
}
