using DIRS21_MenuTask.Interfaces.Base;
using DIRS21_MenuTask.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DIRS21_MenuTask.Interfaces
{
    public interface IDishRepository : IBaseRepository<Dish>
    {
        Task<IEnumerable<Dish>> GetDishesByCategory(string category_id);
        Task<IEnumerable<Dish>> GetDishesByAvailability(string available_time);
        Task<IEnumerable<Dish>> GetAvailableDishes();
    }
}
