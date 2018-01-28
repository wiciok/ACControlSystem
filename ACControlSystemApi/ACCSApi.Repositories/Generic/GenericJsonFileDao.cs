using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ACCSApi.Model.Interfaces;
using ACCSApi.Repositories.Models;
using Autofac;
using Newtonsoft.Json;

namespace ACCSApi.Repositories.Generic
{
    public class GenericJsonFileDao<T> : IDao<T> where T : class, IACCSSerializable
    {
        private List<T> _objectsList;
        private static readonly string pathToFile = GlobalConfig.PathToKeepFilesWithData + typeof(T).Name + ".json";
        private int _lastClassUniqueId = 0;

        public GenericJsonFileDao()
        {
            _objectsList = new List<T>();
            Initialize();
        }

        public void Initialize()
        {
            ReadFromFile();
        }


        public int Add(T obj)
        {
            if (obj.Id == 0)
                obj.Id = ++_lastClassUniqueId;

            _objectsList.Add(obj);
            return obj.Id;
        }

        public void Delete(int id)
        {
            var obj = _objectsList.SingleOrDefault(x => x.Id == id);

            if (obj == null)
                throw new InvalidOperationException("Object to delete doesn't exist.");
            _objectsList.Remove(obj);
        }

        public T Get(int id)
        {
            return _objectsList.SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<T> GetAll()
        {
            return _objectsList;
        }

        public void Update(T obj)
        {
            var updatedObj = _objectsList.SingleOrDefault(x => x.Id == obj.Id);
            updatedObj = updatedObj ?? obj;

            if (updatedObj == null)
                throw new InvalidOperationException("Updated object doesn't exist.");
        }

        public IEnumerable<T> Find(Func<T, bool> expr)
        {
            return _objectsList.Where(expr);
        }

        public void SaveData()
        {
            SaveToFile();
        }

        #region private methods
        private void SaveToFile()
        {
            using (StreamWriter file = File.CreateText(pathToFile))
            {
                var serializer = JsonSerializer.Create(new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects });
                serializer.Serialize(file, _objectsList);
            }
        }

        private void ReadFromFile()
        {
            var t = GlobalConfig.Container.Resolve(typeof(T)).GetType();

            Type genericListType = typeof(List<>).MakeGenericType(t);
            var list = (IList)Activator.CreateInstance(genericListType);

            try
            {
                using (JsonReader file = new JsonTextReader(File.OpenText(pathToFile)))
                {
                    var serializer = JsonSerializer.Create(new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects });
                    var d = (IList)serializer.Deserialize(file, list.GetType());
                    _objectsList = d.Cast<T>().ToList();
                }
                if(_objectsList.Count!=0)
                    _lastClassUniqueId = _objectsList.Select(x => x.Id).Max();
            }
            catch (FileNotFoundException e)
            {
                //todo: logging
            }
        }
        #endregion private methods
    }
}