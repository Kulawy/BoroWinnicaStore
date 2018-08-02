using BoroWinnicaStore.Core.Contracts;
using BoroWinnicaStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoroWinnicaStore.DataAccess.SQL
{
    public class SQLRepository<T> : IRepository<T> where T : BaseEntity 
    {
        public DataContext context;
        public DbSet<T> dbSet;

        public SQLRepository(DataContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }


        public void Comit()
        {
            context.SaveChanges();
        }

        public void Delete(string Id)
        {
            var t = Find(Id);
            if (context.Entry(t).State == EntityState.Detached)
                dbSet.Attach(t);
            dbSet.Remove(t);
        }

        public T Find(string Id)
        {
            return dbSet.Find(Id);
        }

        public void Insert(T t)
        {
            dbSet.Attach(t);
            context.Entry(t).State = EntityState.Modified;
        }

        public IQueryable<T> ItemsCollection()
        {
            return dbSet;
        }

        public void Update(T t)
        {
            dbSet.Attach(t);
            context.Entry(t).State = EntityState.Modified;
        }
    }
}
