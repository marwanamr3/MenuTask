using DIRS21_MenuTask.Models.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DIRS21_MenuTask.Interfaces.Base
{
    public interface IBaseRepository<T> where T : BaseModel
    {
        Task<T> GetByIdAsync(string id);
        Task<IEnumerable<T>> GetAllAsync();
        Task InsertAsync(T obj);
        Task UpdateAsync(T obj);
        Task DeleteAsync(string id);
        Task<IEnumerable<T>> FindByAsync(string key, string value);
        Task<T> FindOneByAsync(string key, string value);
    }
}
