using ACControlSystemApi.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ACControlSystemApi.Repositories.Generic
{
    public class BaseRepository<T>: IRepository<T> where T: class, IACControlSystemSerializableClass
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

        public void Add(IList<T> obj)
        {
            foreach(var el in obj)
            {
                _dao.Add(el);
            }
            _dao.SaveData();
        }

        public void Delete(T obj)
        {
            _dao.Delete(obj.Id);
            _dao.SaveData();
        }

        public void Delete(int id)
        {
            _dao.Delete(id);
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

        public IEnumerable<T> Find(Func<T, bool> fun)
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
