
namespace Restaurant.DAL.Entites
{
    public class Food : TimeStample
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }   

        public Category? Category { get; set; }
        public int? CategoryId { get; set; }
        public ICollection<FoodIngridient>? FoodIngridients { get; set; }
    }
}
