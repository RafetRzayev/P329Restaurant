using Restaurant.AdminPanel.Models.Food;
using Restaurant.AdminPanel.Models.Ingridient;

namespace Restaurant.AdminPanel.Models.FoodIngridient
{
    public class FoodIngridientViewModel
    {
        public FoodViewModel FoodViewModel { get; set; }
        public List<IngridientViewModel> IngridientViewModels { get; set; }
    }
}
