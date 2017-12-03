using Newtonsoft.Json;
using SetupMeetings.Infrastructure.EventSourcing;
using SetupMeetings.Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace SetupMeetings.WebApi.Infrastracture.EventSourcing
{
    public class EventSourcedRepository<T> : IEventSourcedRepository<T>
        where T : class, IEventSourced
    {
        private static readonly string sourceType = typeof(T).Name;
        private readonly IEventBus eventBus;
        private readonly Func<Guid, IEnumerable<IVersionedEvent>, T> entityFactory;
        private readonly Dictionary<Tuple<Type, Guid>, string> filenameByTypeAndId;
        private readonly Dictionary<Guid, T> entityById;
        private JsonTextSerializer serializer = new JsonTextSerializer();

        public EventSourcedRepository(IEventBus eventBus)
        {
            this.eventBus = eventBus;

            var constructor = typeof(T).GetConstructor(new[] { typeof(Guid), typeof(IEnumerable<IVersionedEvent>) });
            if (constructor == null)
            {
                throw new InvalidCastException("Type T must have a constructor with the following signature: .ctor(Guid, IEnumerable<IVersionedEvent>)");
            }
            entityFactory = (id, events) => (T)constructor.Invoke(new object[] { id, events });

            entityById = new Dictionary<Guid, T>();
            filenameByTypeAndId = new Dictionary<Tuple<Type, Guid>, string>();
        }

        public T Find(Guid id)
        {
            if (entityById.TryGetValue(id, out var value))
            {
                return value;
            }

            var filename = CreateFilename(typeof(T), id);
            if (!File.Exists(filename))
            {
                return null;
            }

            using (var stream = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                var doc = XDocument.Load(stream);
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<EventData>));
                using (var reader = doc.CreateReader())
                {
                    var list = (List<EventData>)xmlSerializer.Deserialize(reader);
                    return entityFactory.Invoke(id, list.Select(ed => Deserialize(ed)));
                }
            }
        }

        private string CreateFilename(Type type, Guid id)
        {
            var filename = string.Format("{0}_{1}.xml", type.Name, id);
            var dataDirectory = (string)AppDomain.CurrentDomain.GetData("DataDirectory");
            if (string.IsNullOrEmpty(dataDirectory))
            {
                return filename;
            }
            return Path.Combine(dataDirectory, filename);
        }

        public T Get(Guid id)
        {
            var entity = Find(id);
            if (entity == null)
            {
                throw new EntityNotFoundException(id, sourceType);
            }

            return entity;
        }

        public void Save(T eventSourced, Guid correlationId)
        {
            var events = eventSourced.Events.ToArray();
            this.eventBus.Publish(events.Select(e => new Envelope<IEvent>(e) { CorrelationId = correlationId.ToString() }));
        }

        private IVersionedEvent Deserialize(EventData ed)
        {
            using (var reader = new StringReader(ed.Payload))
            {
                return (IVersionedEvent)this.serializer.Deserialize(reader);
            }
        }

        private EventData Serialize(IVersionedEvent e, string correlationId)
        {
            EventData serialized;
            using (var writer = new StringWriter())
            {
                serializer.Serialize(writer, e);
                serialized = new EventData
                {
                    SourceId = e.SourceId.ToString(),
                    SourceType = sourceType,
                    Version = e.Version,
                    Payload = writer.ToString(),
                    CorrelationId = correlationId,
                };
            }
            return serialized;
        }
    }

    public class JsonTextSerializer
    {
        private readonly JsonSerializer serializer;

        public JsonTextSerializer()
            : this(JsonSerializer.Create(new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
            }))
        {
        }

        public JsonTextSerializer(JsonSerializer serializer)
        {
            this.serializer = serializer;
        }

        public void Serialize(TextWriter writer, object graph)
        {
            var jsonWriter = new JsonTextWriter(writer);
#if DEBUG
            jsonWriter.Formatting = Formatting.Indented;
#endif

            serializer.Serialize(jsonWriter, graph);
            writer.Flush();
        }

        public object Deserialize(TextReader reader)
        {
            var jsonReader = new JsonTextReader(reader);

            try
            {
                return serializer.Deserialize(jsonReader);
            }
            catch (JsonSerializationException e)
            {
                throw new SerializationException(e.Message, e);
            }
        }
    }

    [Serializable]
    internal class EntityNotFoundException : Exception
    {
        private Guid id;
        private string sourceType;

        public EntityNotFoundException()
        {
        }

        public EntityNotFoundException(string message) : base(message)
        {
        }

        public EntityNotFoundException(Guid id, string sourceType)
        {
            this.id = id;
            this.sourceType = sourceType;
        }

        public EntityNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EntityNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
