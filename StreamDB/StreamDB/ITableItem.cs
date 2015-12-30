using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamDB
{
    public class Table<T>: IEnumerable<T> where T : ITableItem
    {
        List<T> contents { get; set; }
        public void Add(T item) { }
        public void Update(T item) { }
        public void Remove(T item) { }

        public IEnumerator<T> GetEnumerator() => contents.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
    public interface ITableItem
    {
        int RowVersion { get; set; }
        Guid RowGuid { get; set; }
    }
}
