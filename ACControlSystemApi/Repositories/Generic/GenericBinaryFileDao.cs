using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using ACControlSystemApi.Utils;
using System.Linq.Expressions;

namespace ACControlSystemApi.Repositories.Generic
{
    public class GenericBinaryFileDao<T> : IDao<T> where T : class
    {
        private FileStream _filestream;

        public GenericBinaryFileDao()
        {
            var pathToFile = GlobalSettings.PathToKeepFilesWithData + nameof(T) + ".bin";
            _filestream = new FileStream(pathToFile, FileMode.Append);
        }


        public void Add(T obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(T obj)
        {
            throw new NotImplementedException();
        }

        public T Get(int id)
        {
            throw new NotImplementedException(); //todo: zwracac null jesli nic nie ma
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(T obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> expr)
        {
            throw new NotImplementedException();
        }

        public void SaveData()
        {
            throw new NotImplementedException();
        }

        private byte[] Serialize()
        {
            throw new NotImplementedException(); //todo: update to .net core 2.0 and do this
        }

        private T Deserialize()
        {
            throw new NotImplementedException(); //todo: update to .net core 2.0 and do this
        }
    }
}
