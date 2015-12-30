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
        public Database(IDatabaseConnection connection)
        {
            Connection = connection;
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

            }
            else
            {
                throw new NotImplementedException();
            }
        }
        public IDatabaseConnection Connection { get; }
        public void SaveChanges()
        {
        }
    }
}
