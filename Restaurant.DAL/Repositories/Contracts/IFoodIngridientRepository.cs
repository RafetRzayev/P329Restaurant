using Restaurant.DAL.Entites;

namespace Restaurant.DAL.Repositories.Contracts
{
    public interface IFoodIngridientRepository : IRepositoryAsync<FoodIngridient>, IRepository<FoodIngridient>
    {
    }
}
