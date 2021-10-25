using DataProvider.Model.Abstractions;
using DataProvider.Models.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Models.MongoDb
{
    public class ToDo : ICollection
    {
        [BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public long? SysTrace { get; set; }
        public string Category { get; set; }
        public string Activity { get; set; }
        public ToDoStatus Status { get; set; }
        public ToDoPriority Priority { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
