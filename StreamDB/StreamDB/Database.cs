using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamDB
{
    public class Database
    {
        [ProtoBuf.ProtoContract]
        class TableHeader
        {
            [ProtoBuf.ProtoMember(1)]
            public string Name { get; set; }
        }
        private IEnumerable<Table> GetTables()
        {
            foreach (var property in this.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
            {
                if (!property.PropertyType.IsGenericType || property.PropertyType.GetGenericTypeDefinition() != typeof(Table<>))
                    continue;
                var table = property.GetValue(this) as Table;
                yield return table;
            }

        }
        public bool AutoSync { get; set; } = true;
        public Database(IDatabaseConnection target, IDatabaseConnection source = null)
        {
            TargetConnection = target;
            SourceConnection = source;
            foreach (var table in GetTables())
                table.Initialize(this);
        }
        internal void QueueUpdate<T>(T item)
        {
            if (AutoSync)
            {
                var header = new TableHeader { Name = typeof(T).Name };
                var writer = new System.IO.StreamWriter(TargetConnection.Stream);
                writer.WriteLine("Hello World");
                writer.Flush();
                ProtoBuf.Serializer.Serialize(TargetConnection.Stream, header);
                ProtoBuf.Serializer.Serialize(TargetConnection.Stream, item);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
        internal void Refresh()
        {
            if (AutoSync&&IsSink)
            {
                var reader = new System.IO.StreamReader(SourceConnection.Stream);
                var test = reader.ReadLine();
                var header = ProtoBuf.Serializer.Deserialize<TableHeader>(SourceConnection.Stream);
                //var table = GetTables().Single(t => t.Name == header.Name);
                //var item = typeof(ProtoBuf.Serializer).GetMethod("Deserialize").MakeGenericMethod(table.Type).Invoke(null, new object[] { SourceConnection.Stream }) as ITableItem;
                //table.GetType().GetMethod("Attach").MakeGenericMethod(table.Type).Invoke(null, new object[] { item });
                var item = ProtoBuf.Serializer.Deserialize<TableItem>(SourceConnection.Stream);

            }
        }
        public IDatabaseConnection TargetConnection { get; }
        public IDatabaseConnection SourceConnection { get; }
        public bool IsSink => SourceConnection != null;
        public bool IsSource => TargetConnection != null;
        public void SaveChanges()
        {
        }
    }
}
