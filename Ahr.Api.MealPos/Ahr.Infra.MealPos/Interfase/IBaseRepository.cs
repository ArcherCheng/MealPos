using System;
using System.Collections.Generic;
using System.Text;

namespace Ahr
{
    //目前決定先不用 Respository Design Pattern 的模式
    //因為在Service層又要通過這一層(Respository)才能對資料庫存取,太複雜了
    //而且要更換資料庫(EntityFramework)的機會太少了
    //最重要是EntityFramework 已經是微軟做好的 Respository了,實在是不想要自己再去做一個來包裝
    interface IBaseRepository
    {
        T GetById<T>(int id) where T : BaseEntity;
        List<T> List<T>() where T : BaseEntity;
        T Add<T>(T entity) where T : BaseEntity;
        void Update<T>(T entity) where T : BaseEntity;
        void Delete<T>(T entity) where T : BaseEntity;
    }
}
