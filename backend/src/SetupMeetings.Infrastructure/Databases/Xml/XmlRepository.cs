using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace SetupMeetings.Infrastructure.Databases.Xml
{
    class XmlRepository<T, TId>
        where T : class
    {
        private string _filePath;
        private Func<T, TId> _selector;
        private Stopwatch _watch = new Stopwatch();
        private Dictionary<TId, T> _entities = new Dictionary<TId, T>();

        protected XmlRepository(string filePath, Func<T, TId> selector)
        {
            _filePath = filePath;
            _selector = selector;
        }

        public IQueryable<T> Queryable
        {
            get
            {
                SyncIfNeeded();
                return new EnumerableQuery<T>(_entities.Values);
            }
        }

        public T FindById(TId id)
        {
            SyncIfNeeded();
            if (_entities.TryGetValue(id, out var value))
            {
                return value;
            }
            return null;
        }

        public void Save(T obj)
        {
            _entities.Add(_selector(obj), obj);
            SaveToXml(_entities);
        }

        private void SaveToXml(Dictionary<TId, T> entities)
        {
            try
            {
                var doc = new XDocument();
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<T>));
                using (var writer = doc.Root.CreateWriter())
                {
                    entities.Values
                        .ToList()
                        .ForEach(v => xmlSerializer.Serialize(writer, v));
                }
                using (var stream = new FileStream(_filePath, FileMode.Create, FileAccess.Read))
                {
                    doc.Save(stream);
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message, "ERROR");
                return null;
            }
        }

        protected void SyncIfNeeded()
        {
            if (!_watch.IsRunning && _watch.Elapsed > TimeSpan.FromSeconds(10))
            {
                var entities = LoadFromXml();
                if (entities != null)
                {
                    _entities = entities;
                    _watch.Reset();
                    _watch.Start();
                }
            }
        }

        private Dictionary<TId, T> LoadFromXml()
        {
            try
            {
                if (!File.Exists(_filePath))
                {
                    return null;
                }
                using (var stream = new FileStream(_filePath, FileMode.Open, FileAccess.Read))
                {
                    var doc = XDocument.Load(stream);

                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<T>));
                    using (var reader = doc.Root.CreateReader())
                    {
                        var list = (List<T>)xmlSerializer.Deserialize(reader);
                        return list.ToDictionary(v => _selector(v), v => v);
                    }
                }
            }
            catch(Exception ex)
            {
                Trace.WriteLine(ex.Message, "ERROR");
                return null;
            }
        }
    }
}
