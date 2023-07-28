using Microsoft.EntityFrameworkCore;
using Restaurant.DAL.DataContext;
using Restaurant.DAL.Entites;
using Restaurant.DAL.Repositories.Contracts;

namespace Restaurant.DAL.Repositories.Concrete
{
    public class FoodRepository : EfCoreRepository<Food>, IFoodRepository
    {
        private readonly AppDbContext _dbContext;

        public FoodRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<Food?> GetByIdAsync(int? id)
        {
            var result = await _dbContext.Foods.Include(x => x.FoodIngridients).ThenInclude(x => x.Ingridient).FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public override async Task<ICollection<Food>> GetAllAsync()
        {
            var result = await _dbContext.Foods
                .Include(x=>x.Category)
                .Include(x => x.FoodIngridients)
                .ThenInclude(x => x.Ingridient)
                .ToListAsync();

            return result;
        }
    }
}
