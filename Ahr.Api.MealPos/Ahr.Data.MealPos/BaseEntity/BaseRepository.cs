using Ahr.Data.MealPos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ahr.Data
{
    /// <summary>
    /// 目前決定先不用 Respository Design Pattern 的模式
    /// 因為在Service層又要通過這一層(Respository)才能對EF存取,太複雜了
    /// 因為以下這些設定全部都在 EntityFramework 中都有了,就是再去多包一層而已,
    /// 最重要是EntityFramework 已經是微軟做好的 Respository了,實在是不想要自己再去做一個來包裝
    /// 我自己感覺是自己把系統複雜化了,故不採用
    /// </summary>
    /// <typeparam name="T"></typeparam>

    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity, new()
    {
        protected AppDbContext NewDb()
        {
            return new AppDbContext();
        }

        public void Add(T entity)
        {
            using (var db = NewDb())
            {
                //db.Add(entity);
                db.Set<T>().Add(entity);
                db.SaveChanges();
            }
        }

        public void Update(T entity)
        {
            using (var db = NewDb())
            {
                //db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                //db.SaveChanges();

                var entry = db.Entry(entity);
                if (entry.State == Microsoft.EntityFrameworkCore.EntityState.Detached)
                {
                    db.Set<T>().Attach(entity);
                }
                entry.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void Delete(T entity)
        {
            using (var db = NewDb())
            {
                ////db.Remove(entity);
                //db.Set<T>().Remove(entity);
                //db.SaveChanges();

                var entry = db.Entry(entity);
                if (entry.State == Microsoft.EntityFrameworkCore.EntityState.Detached)
                {
                    db.Set<T>().Attach(entity);
                }
                entry.State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                db.Set<T>().Remove(entity);
                db.SaveChanges();
            }
        }

        public int GetCount(Func<T, bool> exp)
        {
            using (var db = NewDb())
            {
                return db.Set<T>().Where(exp).ToList().Count();

            }
        }

        public T GetFirst(Func<T, bool> exp)
        {
            using (var db = NewDb())
            {
                return db.Set<T>().Where(exp).FirstOrDefault();
            }
        }

        public IEnumerable<T> GetList(Func<T, bool> exp)
        {
            using (var db = NewDb())
            {
                return db.Set<T>().Where(exp).ToList();
            }
        }

        public IEnumerable<T> GetListForPaging(int pageNumber, int pageSize, Func<T, string> orderName, string sortOrder, Func<T, bool> exp)
        {
            using (var db = NewDb())
            {
                if (sortOrder == "asc")
                {
                    return db.Set<T>().Where(exp).OrderBy(orderName).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                }
                else
                {
                    return db.Set<T>().Where(exp).OrderByDescending(orderName).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                }
            }
        }
    }
}
