using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ahr.Service.MealPos
{
    public interface IOrderService : IBaseService
    {
        Task<OrderDto> CreateOrder(OrderDto model);
        Task<OrderDto> UpdateOrder(int id, OrderDto model);
        Task DeleteOrder(int id);
        Task<OrderDto> GetOrder(int id);
        Task<IEnumerable<OrderDto>> OrderList();
        Task<int> GetMaxOrderNo(DateTime orderDate, string orderType);
 
    }
}
