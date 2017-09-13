using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ACControlSystemApi.Repositories.Generic
{
    public class BaseRepository<T>: IRepository<T> where T: class 
    {
        private IDao<T> _dao;

        public BaseRepository(IDao<T> dao)
        {
            _dao = dao;
        }


        public void Add(T obj)
        {
            _dao.Add(obj);
            _dao.SaveData();
        }

        public void Delete(T obj)
        {
            _dao.Delete(obj);
            _dao.SaveData();
        }

        public T Get(int id)
        {
            return _dao.Get(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _dao.GetAll();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> fun)
        {
            return _dao.Find(fun);
        }

        public void Update(T obj)
        {
            _dao.Update(obj);
            _dao.SaveData();
        }
    }
}
