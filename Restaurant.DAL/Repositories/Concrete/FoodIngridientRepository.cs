using Microsoft.EntityFrameworkCore;
using Restaurant.DAL.DataContext;
using Restaurant.DAL.Entites;
using Restaurant.DAL.Repositories.Contracts;

namespace Restaurant.DAL.Repositories.Concrete
{
    public class FoodIngridientRepository : EfCoreRepository<FoodIngridient>, IFoodIngridientRepository
    {
        private readonly AppDbContext _dbContext;

        public FoodIngridientRepository(AppDbContext dbContext) : base(dbContext)
        {
                _dbContext = dbContext;
        }

        public override async Task<ICollection<FoodIngridient>> GetAllAsync()
        {
            var result = await _dbContext.FoodIngridients.Include(x => x.Ingridient).Include(x => x.Food).ToListAsync();

            return result;
        }
    }
}
