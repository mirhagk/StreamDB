using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamDB
{
    public interface IDatabaseConnection
    {
        Stream Stream { get; }
    }
    public class InMemoryDatabaseConnection : IDatabaseConnection
    {
        public Stream Stream { get; }
    }
}
