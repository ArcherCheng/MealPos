using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ahr
{
    public interface IBaseService
    {
        Task<T> AddAsync<T>(T entity) where T : BaseEntity;

        Task UpdateAsync<T>(T entity) where T : BaseEntity;

        Task DeleteAsync<T>(T entity) where T : BaseEntity;

        T GetById<T>(int id) where T : BaseEntity;

        Task<T> GetByIdAsync<T>(int id) where T : BaseEntity;

        List<T> GetList<T>() where T : BaseEntity;

        Task<List<T>> GetListAsync<T>() where T : BaseEntity;
    }
}
