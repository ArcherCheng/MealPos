using System;
using System.Collections.Generic;
using System.Text;

namespace Ahr.Data
{
    /// <summary>
    /// 目前決定先不用 Respository Design Pattern 的模式
    /// 因為在Service層又要通過這一層(Respository)才能對資料庫存取,太複雜了
    /// 因為以下這些設定全部都在 EntityFramework 中都有了,就是再去多包一層而已,
    /// 最重要是EntityFramework 已經是微軟做好的 Respository了,實在是不想要自己再去做一個來包裝
    /// 我自己感覺是自己把系統複雜化了,故不採用
    /// </summary>
    /// <typeparam name="T"></typeparam>
    interface IBaseRepository<T> where T : BaseEntity
    {
        T GetFirst(Func<T, bool> exp);

        IEnumerable<T> GetList(Func<T, bool> exp);

        IEnumerable<T> GetListForPaging(int pageNumber, int pageSize,
            Func<T, string> orderName, string sortOrder, Func<T, bool> exp);

        int GetCount(Func<T, bool> exp);
        
        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
