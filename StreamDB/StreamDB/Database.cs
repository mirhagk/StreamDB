using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamDB
{
    public class Database
    {
        public Database(IDatabaseConnection connection)
        {
            Connection = connection;
        }
        public void QueueUpdate<T>(T item)
        {

        }
        private IDatabaseConnection Connection { get; }
        public void SaveChanges()
        {
        }
    }
}
