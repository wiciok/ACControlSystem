﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ACControlSystemApi.Repositories.Generic
{
    public interface IDao<T> where T: class
    {
        void Add(T obj);
        void Update(T obj);
        void Delete(T obj);
        T Get(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> expr);

        void SaveData();
    }
}
