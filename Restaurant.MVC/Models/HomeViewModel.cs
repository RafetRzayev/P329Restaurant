using Restaurant.DAL.Entites;

namespace Restaurant.MVC.Models
{
    public class HomeViewModel
    {
        public List<Food> Foods = new List<Food>();
        public List<Category> Categories = new List<Category>();
    }
}
