using System;
using System.Collections.Generic;

namespace ACControlSystemApi.Repositories.Generic
{
    public interface IRepository<T> where T: class
    {
        void Add(T obj);
        void Add(IList<T> obj);
        void Update(T obj);
        void Delete(T obj);
        void Delete(int id);
        T Get(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Func<T, bool> fun);
    }
}
