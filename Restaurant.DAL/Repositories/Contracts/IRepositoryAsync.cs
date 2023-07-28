
using Restaurant.DAL.Entites;
using System.Linq.Expressions;

namespace Restaurant.DAL.Repositories.Contracts
{
    public interface IRepositoryAsync<T> where T : Entity
    {
        Task<T?> GetByIdAsync(int? id);
        Task<T?> GetByConditionAsync(Expression<Func<T, bool>> condition);
        Task<ICollection<T>> GetAllByConditionAsync(Expression<Func<T, bool>> condition);
        Task<ICollection<T>> GetAllAsync(); 
        Task<T> Insert(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(int id);
        Task<T> Delete(T entity);
    }

    public interface IRepository<T> where T : Entity
    {
        void DeleteRange(ICollection<T> entities);

        void InsertRange(params T[] entities);

        void InsertRange(ICollection<T> entities);
    }
}
