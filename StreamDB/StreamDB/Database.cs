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
        private IDatabaseConnection Connection { get; }
        public void SaveChanges()
        {
        }
    }
}
