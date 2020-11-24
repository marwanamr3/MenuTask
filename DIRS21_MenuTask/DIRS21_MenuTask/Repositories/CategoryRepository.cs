using DIRS21_MenuTask.Interfaces;
using DIRS21_MenuTask.Models;
using DIRS21_MenuTask.Repositories.Base;
using MongoDB.Driver;

namespace DIRS21_MenuTask.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(IMongoClient mongoClient, IClientSessionHandle clientSessionHandle) : base(mongoClient, clientSessionHandle, "categories")
        {
        }
    }
}
