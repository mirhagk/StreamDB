﻿using ProtoBuf;
using StreamDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamDBTest.TestDatabases
{
    [ProtoContract]
    class Person : ITableItem
    {
        [ProtoMember(0)]
        public int RowVersion { get; set; }
        [ProtoMember(1)]
        public Guid RowGuid { get; set; }
        [ProtoMember(2)]
        public string FirstName { get; set; }
        [ProtoMember(3)]
        public string LastName { get; set; }
        
    }
    class CustomerDatabase : Database
    {
        public Table<Person> People { get; set; }
    }
}
