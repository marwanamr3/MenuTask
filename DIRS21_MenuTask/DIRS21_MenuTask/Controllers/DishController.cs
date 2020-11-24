using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DIRS21_MenuTask.Interfaces;
using DIRS21_MenuTask.Models;
using System.Linq;
using System;

namespace DIRS21_MenuTask.Controllers
{
    [Produces("application/json")]
    [Route("api/dish")]
    public class DishController : Controller
    {
        private readonly IDishRepository _dishRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IAvailabilityRepository _availabilityRepository;

        public DishController(IDishRepository dishRepository, ICategoryRepository categoryRepository, IAvailabilityRepository availabilityRepository)
        {
            _dishRepository = dishRepository;
            _categoryRepository = categoryRepository;
            _availabilityRepository = availabilityRepository;
        }

        // GET: api/dish
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return new ObjectResult(await _dishRepository.GetAllAsync());
        }

        // GET: api/dish/available
        [HttpGet("available", Name = "GetAvailable")]
        public async Task<IActionResult> GetAllAvailable()
        {
            return new ObjectResult(await _dishRepository.GetAvailableDishes());
        }

        // GET: api/dish/id
        [HttpGet("{id:length(24)}", Name = "Get")]
        public async Task<IActionResult> GetById(string id)
        {
            var dish = await _dishRepository.GetByIdAsync(id);

            if (dish == null)
                return NotFound("Dish Not Found");

            return new ObjectResult(dish);
        }

        [HttpGet("stringfield")]
        public async Task<IActionResult> FindDishBy(string field, string value)
        {
            var dishes = await _dishRepository.FindByAsync(field.Trim().ToLower(), value.Trim().ToLower());
            return new ObjectResult(dishes);
        }

        // GET: api/dish/available/{string}
        [HttpGet("period/", Name = "GetAllPeriod")]
        public async Task<IActionResult> GetAllInPeriod(string period)
        {
            var dishes = await _dishRepository.GetDishesByAvailability(period);
            if (!dishes.Any())
                return NotFound("Please enter a valid period");
            return new ObjectResult(dishes);
        }

        //GET: api/dish/category/{name}
        [HttpGet("category/", Name = "GetByCategory")]
        public async Task<IActionResult> GetDishesByCategoryName(string category_name)
        {
            var category = await _categoryRepository.FindOneByAsync("Name", category_name.Trim().ToLower());
            if (category == null)
                return NotFound("category not found.");
            var dishes = await _dishRepository.GetDishesByCategory(category.Id);
            return new ObjectResult(dishes);

        }

        // POST: api/dish
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateDishModel dish)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(v => v.Errors));

            // Check if category id is valid
            var categoryExist = _categoryRepository.FindOneByAsync("Id", dish.Category);
            if (categoryExist.Result == null)
                return NotFound("category does not exist.");

            // Check if period array elements exists
            var periods = await _availabilityRepository.GetAllAsync();
            var availabStringArr = periods.Select(_ => _.Name).ToList();
            var checkNotExist = availabStringArr.Intersect(dish.AvailableTime).Count() == dish.AvailableTime.Count;
            if (!checkNotExist)
                return NotFound("some of the chosen periods are invalid");

            var _dish = new Dish(dish)
            {
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now
            };
            await _dishRepository.InsertAsync(_dish);
            return new OkObjectResult(_dish);
        }

        // PUT: api/dish/2
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Put(string id, [FromBody] CreateDishModel dish)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(v => v.Errors));

            var dishFromDb = await _dishRepository.GetByIdAsync(id);

            if (dishFromDb == null)
                return NotFound("Dish not found.");

            // Check if category id is valid
            var categoryExist = _categoryRepository.FindOneByAsync("Id", dish.Category);
            if (categoryExist.Result == null)
                return NotFound("category does not exist.");

            // Check if period array elements exists
            var periods = await _availabilityRepository.GetAllAsync();
            var availabStringArr = periods.Select(_ => _.Name).ToList();
            var checkNotExist = availabStringArr.Intersect(dish.AvailableTime).Count() == dish.AvailableTime.Count;
            if (!checkNotExist)
                return NotFound("some of the chosen periods are invalid");

            var _dish = new Dish(dish)
            {
                Id = dishFromDb.Id,
                DateCreated = dishFromDb.DateCreated,
                DateUpdated = DateTime.Now
            };
            await _dishRepository.UpdateAsync(_dish);
            return new OkObjectResult(dish);
        }

        // DELETE: api/dish/2
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var dishFromDb = await _dishRepository.GetByIdAsync(id);

            if (dishFromDb == null)
                return NotFound("Dish not found");

            await _dishRepository.DeleteAsync(id);
            return new OkResult();
        }
    }
}