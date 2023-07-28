using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Logging;
using Restaurant.AdminPanel.Models.Food;
using Restaurant.AdminPanel.Models.FoodIngridient;
using Restaurant.AdminPanel.Models.Ingridient;
using Restaurant.DAL.Entites;
using Restaurant.DAL.Repositories.Contracts;

namespace Restaurant.AdminPanel.Controllers
{
    public class FoodIngridientController : Controller
    {
        private readonly IFoodRepository _foodRepository;
        private readonly IIngridientRepository _ingridientRepository;
        private readonly IFoodIngridientRepository _foodIngridientRepository;

        public FoodIngridientController(IFoodRepository foodRepository, IIngridientRepository ingridientRepository, IFoodIngridientRepository foodIngridientRepository)
        {
            _foodRepository = foodRepository;
            _ingridientRepository = ingridientRepository;
            _foodIngridientRepository = foodIngridientRepository;
        }

        public async Task<IActionResult> Index()
        {
            var foods = await _foodRepository.GetAllAsync();

            var model = new List<FoodIngridientViewModel>();

            foreach (var item in foods)
            {
                var foodViewModel = new FoodViewModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                };

                var ingridientViewModels = new List<IngridientViewModel>();

                foreach (var foodIngridient in item.FoodIngridients)
                {
                    ingridientViewModels.Add(new IngridientViewModel
                    {
                        Name = foodIngridient.Ingridient.Name
                    });
                }

                model.Add(new FoodIngridientViewModel
                {
                    FoodViewModel = foodViewModel,
                    IngridientViewModels = ingridientViewModels
                });
            }

            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            var foods = await _foodRepository.GetAllAsync();
            var ingridients = await _ingridientRepository.GetAllAsync();

            var foodSelectListItems = new List<SelectListItem>();
            var ingridientSelectListItems = new List<SelectListItem>();

            foods.ToList().ForEach(x => foodSelectListItems.Add(new SelectListItem(x.Name, x.Id.ToString())));
            ingridients.ToList().ForEach(x => ingridientSelectListItems.Add(new SelectListItem(x.Name, x.Id.ToString())));

            var model = new FoodIngridientCreateViewModel
            {
                Foods = foodSelectListItems,
                Ingridients = ingridientSelectListItems,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FoodIngridientCreateViewModel model)
        {
            var foods = await _foodRepository.GetAllAsync();
            var ingridients = await _ingridientRepository.GetAllAsync();

            var foodSelectListItems = new List<SelectListItem>();
            var ingridientSelectListItems = new List<SelectListItem>();

            foods.ToList().ForEach(x => foodSelectListItems.Add(new SelectListItem(x.Name, x.Id.ToString())));
            ingridients.ToList().ForEach(x => ingridientSelectListItems.Add(new SelectListItem(x.Name, x.Id.ToString())));

            var viewModel = new FoodIngridientCreateViewModel
            {
                Foods = foodSelectListItems,
                Ingridients = ingridientSelectListItems,
            };

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var foodIngridients = new List<FoodIngridient>();

            foreach (var ingridientId in model.IngridientIds)
            {
                foodIngridients.Add(new FoodIngridient
                {
                    FoodId = model.FoodId,
                    IngridientId = ingridientId
                });
            }

            _foodIngridientRepository.InsertRange(foodIngridients);

            return RedirectToAction("index", "food");
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();

            var food = await _foodRepository.GetByIdAsync(id);

            if (food == null) return NotFound();

            var ingridientSelectListItems = new List<SelectListItem>();

            food.FoodIngridients?.ToList().ForEach(x => ingridientSelectListItems.Add(new SelectListItem(x.Ingridient.Name, x.Ingridient.Id.ToString())));

            var ingridients = await _ingridientRepository.GetAllAsync();
            var newIngridientSelectListItems = new List<SelectListItem>();
            ingridients.ToList().ForEach(x => newIngridientSelectListItems.Add(new SelectListItem(x.Name, x.Id.ToString())));

            var model = new FoodIngridientUpdateViewModel
            {
                FoodName = food.Name,
                Ingridents = ingridientSelectListItems,
                NewIngridents = newIngridientSelectListItems
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, FoodIngridientUpdateViewModel model)
        {
            if (id == null) return NotFound();

            var food = await _foodRepository.GetByIdAsync(id);

            if (food == null) return NotFound();

            var ingridientSelectListItems = new List<SelectListItem>();

            food.FoodIngridients?.ToList().ForEach(x => ingridientSelectListItems.Add(new SelectListItem(x.Ingridient.Name, x.Ingridient.Id.ToString())));

            var ingridients = await _ingridientRepository.GetAllAsync();
            var newIngridientSelectListItems = new List<SelectListItem>();
            ingridients.ToList().ForEach(x => newIngridientSelectListItems.Add(new SelectListItem(x.Name, x.Id.ToString())));

            var viewModel = new FoodIngridientUpdateViewModel
            {
                FoodName = food.Name,
                Ingridents = ingridientSelectListItems,
                NewIngridents = newIngridientSelectListItems
            };

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            //remove ingridients

            foreach (var item in model.RemovedIngridientIds)
            {
                var foodIngridient = await _foodIngridientRepository.GetByConditionAsync(x => x.FoodId == food.Id && x.IngridientId == item);

                if (foodIngridient == null) continue;

                await _foodIngridientRepository.Delete(foodIngridient);
            }

            //add ingridients

            foreach (var item in model.NewIngridentIds)
            {
                var isExist = await _foodIngridientRepository.GetByConditionAsync(x => x.FoodId == food.Id && x.IngridientId == item);

                if (isExist != null) continue;

                var foodIngridient = new FoodIngridient
                {
                    FoodId = food.Id,
                    IngridientId = item
                };

                await _foodIngridientRepository.Insert(foodIngridient);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var foodGradients = await _foodIngridientRepository.GetAllByConditionAsync(x => x.FoodId == id);

            _foodIngridientRepository.DeleteRange(foodGradients);

            return RedirectToAction(nameof(Index));
        }
    }
}
