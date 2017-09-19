using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using ACControlSystemApi.Utils;
using System.Linq.Expressions;
using ACControlSystemApi.Model.Interfaces;

namespace ACControlSystemApi.Repositories.Generic
{
    public class GenericBinaryFileDao<T> : IDao<T> where T : class, IACControlSystemSerializableClass
    {
        private List<T> _objectsList;
        private static readonly string pathToFile = GlobalSettings.PathToKeepFilesWithData + nameof(T) + ".bin";
        private bool isCacheUpToDate = false;

        public GenericBinaryFileDao()
        {
            _objectsList = new List<T>();
        }


        public void Add(T obj)
        {
            _objectsList.Add(obj);

            SaveToFile();
        }

        public void Delete(T obj)
        {
            RefreshCacheIfNecessary();

            if (!_objectsList.Remove(obj))
                throw new InvalidOperationException("Object to delete doesn't exist.");

            SaveToFile();
        }

        public T Get(int id)
        {
            RefreshCacheIfNecessary();

            return _objectsList.Where(x => x.Id.Equals(id)).SingleOrDefault();
        }

        public IEnumerable<T> GetAll()
        {
            RefreshCacheIfNecessary();

            return _objectsList;
        }

        public void Update(T obj)
        {
            RefreshCacheIfNecessary();

            var updatedObj = _objectsList.Where(x => x.Equals(obj)).FirstOrDefault();
            updatedObj = updatedObj ?? obj;

            if (updatedObj == null)
                throw new InvalidOperationException("Updated object doesn't exist.");

            SaveToFile();
        }

        public IEnumerable<T> Find(Func<T, bool> expr)
        {
            RefreshCacheIfNecessary();
            return _objectsList.Where(expr);
        }


        public void SaveData()
        {
            
        }

        private void RefreshCacheIfNecessary()
        {
            if (!isCacheUpToDate)
            {
                ReadFromFile();
                isCacheUpToDate = true;
            }
        }



        private void SaveToFile()
        {
            //todo: check if this gonna work (if dispose closes stream or not)

            byte[] data = Serialize(_objectsList);

            using (var writer = new BinaryWriter(new FileStream(pathToFile, FileMode.Append)))
            {
                writer.Write(data);
            }
        }

        private void ReadFromFile()
        {
            byte[] buffer;

            using (var ms = new MemoryStream())
            {
                using (var fs = new FileStream(pathToFile, FileMode.Open))
                {
                    fs.CopyTo(ms);
                }
                    
                buffer = ms.ToArray();
            }

            _objectsList = Deserialize(buffer);
        }


        private byte[] Serialize(List<T> obj)
        {
            return MessagePack.MessagePackSerializer.Serialize<List<T>>(obj);
        }

        private List<T> Deserialize(byte[] rawObj)
        {
            return MessagePack.MessagePackSerializer.Deserialize<List<T>>(rawObj);
        }
    }
}
