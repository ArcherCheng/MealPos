using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ahr.Service.MealPos
{
    public interface IAppBaseService : IBaseService
    {
        Task<IEnumerable<string>> GetAppKeyValue(string keyGroup);
    }
}
