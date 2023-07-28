using Restaurant.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAL.Repositories.Contracts
{
    public interface ICategoryRepository : IRepositoryAsync<Category>
    {
    }
}
