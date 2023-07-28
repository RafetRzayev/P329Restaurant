using Microsoft.AspNetCore.Mvc.Rendering;
using Restaurant.AdminPanel.Models.Food;

namespace Restaurant.AdminPanel.Models.FoodIngridient
{
    public class FoodIngridientUpdateViewModel
    {
        public string? FoodName { get; set; }

        public List<SelectListItem>? Ingridents { get; set; }
        public List<int>? RemovedIngridientIds { get; set; } = new List<int>();
        public List<SelectListItem>? NewIngridents { get; set; }
        public List<int>? NewIngridentIds { get; set; } = new List<int>();
    }
}
