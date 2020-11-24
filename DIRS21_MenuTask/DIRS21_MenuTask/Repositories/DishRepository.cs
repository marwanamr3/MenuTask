using DIRS21_MenuTask.Models;
using DIRS21_MenuTask.Interfaces;
using DIRS21_MenuTask.Repositories.Base;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace DIRS21_MenuTask.Repositories
{
    public class DishRepository : BaseRepository<Dish>, IDishRepository
    {
        public DishRepository(IMongoClient mongoClient, IClientSessionHandle clientSessionHandle) : base(mongoClient, clientSessionHandle, "dishes")
        {
        }
        public async Task<IEnumerable<Dish>> GetAvailableDishes()
        {
            return await Collection.Find(m => m.IsAvailable == true).ToListAsync();
        }

        public async Task<IEnumerable<Dish>> GetDishesByAvailability(string period)
        {
            var field = new StringFieldDefinition<Dish, string>("availability_time");
            FilterDefinition<Dish> filter = Builders<Dish>.Filter.AnyEq(field, period.Trim().ToLower());
            var dishes = await Collection.Find(filter).ToListAsync();
            return dishes;
        }

        public async Task<IEnumerable<Dish>> GetDishesByCategory(string category_id)
        {
            var catId = ObjectId.Parse(category_id);
            var field = new StringFieldDefinition<Dish, ObjectId>("category_id");
            FilterDefinition<Dish> filter = Builders<Dish>.Filter.Eq(field, catId);
            var objList = await Collection.Find(filter).ToListAsync();
            return objList;
        }
    }
}
