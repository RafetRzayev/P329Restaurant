using Restaurant.DAL.DataContext;
using Restaurant.DAL.Entites;
using Restaurant.DAL.Repositories.Contracts;

namespace Restaurant.DAL.Repositories.Concrete
{
    public class CategoryRepository : EfCoreRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext dbContext) : base(dbContext)
        {
                
        }
    }
}
