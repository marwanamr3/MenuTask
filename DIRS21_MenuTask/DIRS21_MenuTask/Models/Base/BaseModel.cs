using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace DIRS21_MenuTask.Models.Base
{
    public abstract class BaseModel
    {
        [BsonId]
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public virtual string Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("created_at")]
        public DateTime DateCreated { get; set; }
        [BsonElement("updated_at")]
        public DateTime DateUpdated { get; set; }

    }
}
