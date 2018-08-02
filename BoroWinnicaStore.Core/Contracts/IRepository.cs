using BoroWinnicaStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoroWinnicaStore.Core.Contracts
{
    public interface IRepository<T> where T: BaseEntity
    {
        IQueryable<T> ItemsCollection();
        void Comit();
        void Delete(string Id);
        T Find(string Id);
        void Insert(T t);
        void Update(T t);
        
    }
}
