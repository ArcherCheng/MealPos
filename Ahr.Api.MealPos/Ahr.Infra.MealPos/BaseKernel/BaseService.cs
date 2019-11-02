using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ahr
{
    public class BaseService  //: IBaseService
    {
        //protected AppDbContext NewDb()
        //{
        //    return new AppDbContext();
        //}

        //public async Task<T> AddAsync<T>(T entity) where T : BaseEntity
        //{
        //    using (var db = NewDb())
        //    {
        //        //db.Add(entity);
        //        db.Set<T>().Add(entity);
        //        await db.SaveChangesAsync();
        //        return entity;
        //    }
        //}

        //public async Task DeleteAsync<T>(T entity) where T : BaseEntity
        //{
        //    using (var db = new AppDbContext())
        //    {
        //        ////db.Remove(entity);
        //        //db.Set<T>().Remove(entity);
        //        //db.SaveChangesAsync();

        //        var entry = db.Entry(entity);
        //        if (entry.State == Microsoft.EntityFrameworkCore.EntityState.Detached)
        //        {
        //            db.Set<T>().Attach(entity);
        //        }
        //        entry.State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
        //        db.Set<T>().Remove(entity);
        //        await db.SaveChangesAsync();
        //    }
        //}

        //public async Task UpdateAsync<T>(T entity) where T : BaseEntity
        //{
        //    using (var db = new AppDbContext())
        //    {
        //        //db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        //        //db.SaveChangesAsync();

        //        var entry = db.Entry(entity);
        //        if (entry.State == Microsoft.EntityFrameworkCore.EntityState.Detached)
        //        {
        //            db.Set<T>().Attach(entity);
        //        }
        //        entry.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        //        await db.SaveChangesAsync();
        //    }
        //}

        //public T GetById<T>(int id) where T : BaseEntity
        //{
        //    using (var db = new AppDbContext())
        //    {
        //        //var Set = db.Set<T>();
        //        //var TResult = Set.Find(id);
        //        //return TResult;
        //        return db.Set<T>().Find(id);
        //    }
        //}

        //public async Task<T> GetByIdAsync<T>(int id) where T : BaseEntity
        //{
        //    using (var db = new AppDbContext())
        //    {
        //        //var Set = db.Set<T>();
        //        //var TResult = Set.FindAsync(id);
        //        //return TResult;
        //        return await db.Set<T>().FindAsync(id);
        //    }
        //}

        //public List<T> GetList<T>() where T : BaseEntity
        //{
        //    using (var db = new AppDbContext())
        //    {
        //        var result = db.Set<T>().ToList();
        //        return result;
        //    }
        //}

        //public async Task<List<T>> GetListAsync<T>() where T : BaseEntity
        //{
        //    using (var db = new AppDbContext())
        //    {
        //        var result = await db.Set<T>().ToListAsync();
        //        return result;
        //    }
        //}
    }
}
