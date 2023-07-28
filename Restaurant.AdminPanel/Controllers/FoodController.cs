using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using Restaurant.AdminPanel.Models.Food;
using Restaurant.DAL.Entites;
using Restaurant.DAL.Repositories.Contracts;

namespace Restaurant.AdminPanel.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class FoodController : Controller
    {
        private readonly IFoodRepository _foodRepository;

        public FoodController(IFoodRepository foodRepository)
        {
            _foodRepository = foodRepository;
        }

        public async Task<IActionResult> Index()
        {
            var foods = await _foodRepository.GetAllAsync();
            var foodViewModel = new List<FoodViewModel>();

           foreach (var food in foods)
           {
                foodViewModel.Add(new FoodViewModel
                {
                    Id = food.Id,
                    Name = food.Name,
                    Description = food.Description,
                    Price = food.Price,
                });
           }

           return View(foodViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FoodCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View();

            var isExist = await _foodRepository.GetByConditionAsync(x => x.Name.ToLower().Equals(model.Name.ToLower()));

            if (isExist != null)
            {
                ModelState.AddModelError("Name", "Bu adda yemek movcuddur");

                return View();
            }

            var createdFood = new Food
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
            };

            await _foodRepository.Insert(createdFood);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();

            var food = await _foodRepository.GetByIdAsync(id);

            if (food == null) return NotFound();

            var model = new FoodUpdateViewModel
            {
                Name = food.Name,
                Description = food.Description,
                Price = food.Price,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id,  FoodUpdateViewModel model)
        {
            if (id == null) return NotFound();

            var isExist = await _foodRepository.GetByConditionAsync(x => x.Name.ToLower().Equals(model.Name.ToLower()));

            if (isExist != null)
            {
                ModelState.AddModelError("Name", "Bu adda yemek movcuddur");

                return View();
            }

            var updatedFood = await _foodRepository.GetByIdAsync(id);

            if (updatedFood == null) return NotFound();

            updatedFood.Name = model.Name;
            updatedFood.Description= model.Description;
            updatedFood.Price = model.Price;

            await _foodRepository.Update(updatedFood);

            return RedirectToAction(nameof(Index));
        }
    }
}
