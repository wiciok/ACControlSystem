using System;
using System.Collections.Generic;
using ACCSApi.Model.Interfaces;

namespace ACCSApi.Repositories.Generic
{
    public class BaseRepository<T>: IRepository<T> where T: class, IACCSSerializable
    {
        private readonly IDao<T> _dao;

        public BaseRepository(IDao<T> dao)
        {
            _dao = dao;
        }

        public int Add(T obj)
        {
            var retId = _dao.Add(obj);
            _dao.SaveData();
            return retId;
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
