using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ahr.Service.MealPos
{
    public interface ICustomerService : IBaseService
    {
        Task<CustomerDto> CreateCustomer(CustomerDto model);
        Task<CustomerDto> UpdateCustomer(int id, CustomerDto model);
        Task DeleteCustomer(int id);
        Task<CustomerDto> GetCustomer(int id);
        Task<IEnumerable<CustomerDto>> CustomerList();
    }
}
