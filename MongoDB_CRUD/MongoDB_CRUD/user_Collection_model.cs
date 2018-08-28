using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDB_CRUD
{
    class user_Collection_model
    {
        [BsonElement("_id")]
        public ObjectId Id { set; get; }

        [BsonExtraElements]
        public BsonDocument CatchAll { get; set; }
    }
}
