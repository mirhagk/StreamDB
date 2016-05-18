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
    public class FileStreamDatabaseConnection : IDatabaseConnection
    {
        public Stream Stream { get; }
        public FileStreamDatabaseConnection(string path)
        {
            Stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
        }
    }
    public class InMemoryDatabaseConnection : IDatabaseConnection
    {
        public Stream Stream { get; }
        MemoryStream MemoryStream { get; }
        public InMemoryDatabaseConnection()
        {
            Stream = MemoryStream = new MemoryStream();
        }
    }
}
