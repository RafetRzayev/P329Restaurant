using Microsoft.EntityFrameworkCore;
using Restaurant.DAL.DataContext;
using Restaurant.DAL.Entites;
using Restaurant.DAL.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAL.Repositories.Concrete
{
    public class IngridientRepository : EfCoreRepository<Ingridient>, IIngridientRepository
    {
        private readonly AppDbContext _dbContext;

        public IngridientRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async override Task<ICollection<Ingridient>> GetAllAsync()
        {
            var result = await _dbContext.Ingridients
               .Include(x => x.FoodIngridients)
               .ThenInclude(x => x.Food)
               .ToListAsync();

            return result;
        }
    }
}
