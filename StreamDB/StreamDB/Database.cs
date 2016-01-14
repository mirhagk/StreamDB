using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamDB
{
    public class Database
    {
        public bool AutoSync { get; set; } = true;
        public Database(IDatabaseConnection target, IDatabaseConnection source = null)
        {
            TargetConnection = target;
            SourceConnection = source;
            foreach(var property in this.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
            {
                if (!property.PropertyType.IsGenericType || property.PropertyType.GetGenericTypeDefinition() != typeof(Table<>))
                    continue;
                var table = property.GetValue(this) as Table;
                table.Initialize(this);
            }
        }
        public void QueueUpdate<T>(T item)
        {
            if (AutoSync)
            {
                ProtoBuf.Serializer.Serialize(TargetConnection.Stream, item);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
        public IDatabaseConnection TargetConnection { get; }
        public IDatabaseConnection SourceConnection { get; }
        public bool IsSink => TargetConnection != null;
        public bool IsSource => SourceConnection != null;
        public void SaveChanges()
        {
        }
    }
}
