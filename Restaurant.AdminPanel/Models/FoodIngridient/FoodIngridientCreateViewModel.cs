using Microsoft.AspNetCore.Mvc.Rendering;

namespace Restaurant.AdminPanel.Models.FoodIngridient
{
    public class FoodIngridientCreateViewModel
    {
        public List<SelectListItem>? Foods {  get; set; }
        public int FoodId { get; set; } 
        public List<SelectListItem>? Ingridients { get; set; }
        public List<int> IngridientIds { get; set; }
    }
}
