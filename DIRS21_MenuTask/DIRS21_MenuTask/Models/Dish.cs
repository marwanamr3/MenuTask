using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using DIRS21_MenuTask.Models.Base;

namespace DIRS21_MenuTask.Models
{
    public class Dish : BaseModel
    {
        public Dish(CreateDishModel createDish) => (Name, Description, Price, Category, AvailableTime, IsAvailable, PrepTime, Macros) = (createDish.Name, createDish.Description, createDish.Price, createDish.Category, createDish.AvailableTime, createDish.IsAvailable, createDish.PrepTime, createDish.Macros);

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("price")]
        public double Price { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("category_id")]
        public string Category { get; set; }

        [BsonElement("availability_time")]
        public List<string> AvailableTime { get; set; }

        [BsonElement("available")]
        public bool IsAvailable { get; set; }

        [BsonElement("preparation_time")]
        public int PrepTime { get; set; }

        [BsonElement("macros")]
        public string Macros { get; set; }
    }
}
