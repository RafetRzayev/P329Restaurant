using Microsoft.AspNetCore.Mvc;
using Restaurant.DAL.Repositories.Contracts;
using Restaurant.MVC.Models;

namespace Restaurant.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFoodRepository _foodRepository;
        private readonly ICategoryRepository _categoryRepository;

        public HomeController(IFoodRepository foodRepository, ICategoryRepository categoryRepository)
        {
            _foodRepository = foodRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            var foods = await _foodRepository.GetAllAsync();
            var categories = await _categoryRepository.GetAllAsync();

            var model = new HomeViewModel
            {
                Foods = foods.ToList(),
                Categories = categories.ToList(),
            };

            return View(model);
        }
    }
}
