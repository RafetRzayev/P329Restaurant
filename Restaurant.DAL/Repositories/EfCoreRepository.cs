using Microsoft.EntityFrameworkCore;
using Restaurant.DAL.DataContext;
using Restaurant.DAL.Entites;
using Restaurant.DAL.Repositories.Contracts;
using System.Linq.Expressions;
namespace Restaurant.DAL.Repositories
{
    public class EfCoreRepository<T> : IRepositoryAsync<T>, IRepository<T> where T : Entity
    {
        private readonly AppDbContext _dbContext;

        public EfCoreRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<T> Insert(T entity)
        {
            var result = await _dbContext.Set<T>().AddAsync(entity);

            await _dbContext.SaveChangesAsync();

            return result.Entity;
        }

        public virtual async Task<T> Delete(int id)
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);

            if (entity == null)
                throw new Exception("Entity not found");

            var result = _dbContext.Set<T>().Remove(entity);

            await _dbContext.SaveChangesAsync();

            return result.Entity;
        }

        public virtual async Task<T> Delete(T entity)
        {
            var result = _dbContext.Set<T>().Remove(entity);

            await _dbContext.SaveChangesAsync();

            return result.Entity;
        }

        public void DeleteRange(ICollection<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);

            _dbContext.SaveChanges();
        }

        public virtual async Task<ICollection<T>> GetAllAsync()
        {
            var result = await _dbContext.Set<T>().ToListAsync();

            return result;
        }

        public async Task<ICollection<T>> GetAllByConditionAsync(Expression<Func<T, bool>> condition)
        {
            var result = await _dbContext.Set<T>().Where(condition).ToListAsync();

            return result;
        }

        public virtual async Task<T?> GetByConditionAsync(Expression<Func<T, bool>> condition)
        {
            var result = await _dbContext.Set<T>().Where(condition).ToListAsync();

            return result.FirstOrDefault();
        }

        public virtual async Task<T?> GetByIdAsync(int? id)
        {
            if (id == null)
            {
                throw new Exception("Id is null");
            }

            var result = await _dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public void InsertRange(params T[] entities)
        {
            _dbContext.Set<T>().AddRange(entities);

            _dbContext.SaveChanges();
        }

        public virtual async Task<T> Update(T entity)
        {
            var result = _dbContext.Set<T>().Update(entity);

            await _dbContext.SaveChangesAsync();

            return result.Entity;
        }

        public void InsertRange(ICollection<T> entities)
        {
            _dbContext.Set<T>().AddRange(entities);

            _dbContext.SaveChanges();
        }
    }
}
