using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace DIRS21_MenuTask.Models
{
    public class CreateDishModel
    {
        [BsonElement("name")]
        [StringLength(100, ErrorMessage = "Name length can't be more than 100.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [BsonElement("description")]
        [StringLength(200, ErrorMessage = "Description cannot be longer than 200 characters")]
        public string Description { get; set; }
        [BsonElement("price")]
        [Required(ErrorMessage = "Dish price is required")]
        [Range(0.5, 999.99, ErrorMessage = "Price range must be between 0.5 and 999.99")]
        public double Price { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("category_id")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Category Id is required")]
        public string Category { get; set; }
        [BsonElement("availability_time")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Dish availability times are required")]
        public List<string> AvailableTime { get; set; }
        [BsonElement("available")]
        [Required(ErrorMessage = "Dish status is required")]
        public bool IsAvailable { get; set; }
        [BsonElement("preparation_time")]
        [Required(ErrorMessage = "Dish preparation time is required")]
        [Range(0, 120, ErrorMessage = "Preparation time can't be more than two hours.")]
        public int PrepTime { get; set; }
        [BsonElement("macros")]
        [StringLength(20, ErrorMessage = "Macros cannot be longer than 20 characters")]
        public string Macros { get; set; }
    }
}
