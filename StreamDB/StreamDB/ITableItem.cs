using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamDB
{
    public class Table
    {
        protected Database db { get; set; }
        public string Name { get; private set; }
        public Type Type { get; protected set; }
        public void Initialize(Database db)
        {
            this.db = db;
            Name = this.GetType().Name;
        }

    }
    public class Table<T>: Table, IEnumerable<T> where T : ITableItem
    {
        public Table()
        {
            this.Type = typeof(T);
        }
        List<T> contents { get; set; } = new List<T>();
        internal void Attach(T item)
        {
            contents.Add(item);
        }
        public void Add(T item)
        {
            contents.Add(item);
            item.RowVersion = 1;
            item.RowGuid = Guid.NewGuid();
            db.QueueUpdate(item);
        }
        public void Update(T item) { }
        public void Remove(T item) { }

        public IEnumerator<T> GetEnumerator()
        {
            db.Refresh();
            return contents.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
    public interface ITableItem
    {
        int RowVersion { get; set; }
        Guid RowGuid { get; set; }
    }
    [ProtoBuf.ProtoContract]
    public class TableItem : ITableItem
    {
        [ProtoBuf.ProtoMember(1)]
        public int RowVersion { get; set; }
        [ProtoBuf.ProtoMember(2)]
        public Guid RowGuid { get; set; }
    }
}
