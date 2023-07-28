using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.DAL.DataContext;
using Restaurant.DAL.Repositories.Concrete;
using Restaurant.DAL.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAL
{
    public static class DalServiceRegistrations
    {
        public static IServiceCollection AddDalServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), x =>
                {
                    x.MigrationsAssembly("Restaurant.DAL");
                });
            });

            services.AddScoped<IFoodRepository, FoodRepository>();
            services.AddScoped<IIngridientRepository, IngridientRepository>();
            services.AddScoped<IFoodIngridientRepository, FoodIngridientRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            return services;
        }
    }
}
