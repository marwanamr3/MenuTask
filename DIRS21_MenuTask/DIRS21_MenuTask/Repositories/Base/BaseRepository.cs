using DIRS21_MenuTask.Interfaces.Base;
using DIRS21_MenuTask.Models.Base;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DIRS21_MenuTask.Repositories.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseModel
    {
        private const string DATABASE = "MenuDb";
        private readonly IMongoClient _mongoClient;
        private readonly IClientSessionHandle _clientSessionHandle;
        private readonly string _collection;

        public BaseRepository(IMongoClient mongoClient, IClientSessionHandle clientSessionHandle, string collection)
        {
            (_mongoClient, _clientSessionHandle, _collection) = (mongoClient, clientSessionHandle, collection);

            if (!_mongoClient.GetDatabase(DATABASE).ListCollectionNames().ToList().Contains(collection))
                _mongoClient.GetDatabase(DATABASE).CreateCollection(collection);
        }

        protected virtual IMongoCollection<T> Collection =>
            _mongoClient.GetDatabase(DATABASE).GetCollection<T>(_collection);

        public async Task<T> GetByIdAsync(string id)
        {
            return await Collection.Find(_ => _.Id == id).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<T>> GetAllAsync() => await Collection.Find(_ => true).ToListAsync();

        public async Task InsertAsync(T obj) =>
            await Collection.InsertOneAsync(_clientSessionHandle, obj);

        public async Task UpdateAsync(T obj)
        {
            Expression<Func<T, string>> func = f => f.Id;
            var value = (string)obj.GetType().GetProperty(func.Body.ToString().Split(".")[1]).GetValue(obj, null);
            var filter = Builders<T>.Filter.Eq(func, value);

            if (obj != null)
                await Collection.ReplaceOneAsync(_clientSessionHandle, filter, obj);
        }

        public async Task DeleteAsync(string id) =>
            await Collection.DeleteOneAsync(_clientSessionHandle, f => f.Id == id);


        public async Task<IEnumerable<T>> FindByAsync(string key, string value)
        {
            var field = new StringFieldDefinition<T, string>(key);
            FilterDefinition<T> filter = Builders<T>.Filter.Eq(field, value);
            var objList = await Collection.Find(filter).ToListAsync();
            return objList;
        }

        public async Task<T> FindOneByAsync(string key, string value)
        {
            var field = new StringFieldDefinition<T, string>(key);
            FilterDefinition<T> filter = Builders<T>.Filter.Eq(field, value);
            var obj = await Collection.Find(filter).FirstOrDefaultAsync();
            return obj;
        }

    }
}
