using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using ACControlSystemApi.Model.Interfaces;
using ACControlSystemApi.Utils;

namespace ACControlSystemApi.Repositories.Generic
{
    public class GenericBinaryFileDao<T> : IDao<T> where T : class, IACCSSerializable
    {
        private List<T> _objectsList;
        private static readonly string pathToFile = GlobalSettings.PathToKeepFilesWithData + nameof(T) + ".bin";
        private int _lastClassUniqueId = 1;

        public GenericBinaryFileDao()
        {
            _objectsList = new List<T>();
            Initialize();
        }

        public void Initialize()
        {
            ReadFromFile();
        }


        public void Add(T obj)
        {
            if (obj.Id == 0)
                obj.Id = _lastClassUniqueId++;

            _objectsList.Add(obj);
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
            return _objectsList.Where(x => x.Id == id).SingleOrDefault();
        }

        public IEnumerable<T> GetAll()
        {
            return _objectsList;
        }

        public void Update(T obj)
        {
            var updatedObj = _objectsList.Where(x => x.Id == obj.Id).SingleOrDefault();
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
            //todo: check if this gonna work (if dispose closes stream or not)

            byte[] data = MessagePack.MessagePackSerializer.Serialize<List<T>>(_objectsList);

            using (var writer = new BinaryWriter(new FileStream(pathToFile, FileMode.Create)))
            {
                writer.Write(_lastClassUniqueId);
                writer.Write(data);
            }
        }

        private void ReadFromFile()
        {
            try
            {
                byte[] buffer;
                using (var reader = new BinaryReader(new FileStream(pathToFile, FileMode.Open)))
                {
                    _lastClassUniqueId = reader.ReadInt32();
                    buffer = reader.ReadBytes(int.MaxValue); //?? does this work?
                }
                _objectsList = MessagePack.MessagePackSerializer.Deserialize<List<T>>(buffer);
            }

            catch (FileNotFoundException) //todo: think about removing control via exeptions
            {
                //log info about lack of file
            }
        }
        #endregion private methods
    }
}
