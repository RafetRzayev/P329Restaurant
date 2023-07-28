
namespace Restaurant.DAL.Entites
{
    public class FoodIngridient : Entity
    {
        public int FoodId { get; set; }
        public Food Food { get; set; }
        public int IngridientId { get; set; }
        public Ingridient Ingridient { get; set; }
    }
}
