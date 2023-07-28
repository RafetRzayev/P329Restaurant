using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Restaurant.DAL.DataContext;
using Restaurant.DAL.Entites;

namespace Restaurant.AdminPanel.Data
{
    public class DataInitializer
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _dbContext;

        public DataInitializer(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, AppDbContext dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _dbContext = dbContext;
        }

        public async Task SeedData()
        {
            await _dbContext.Database.MigrateAsync();

            var createdRoles = new List<string> { Constants.AdminRole, Constants.MemberRole };

            foreach (var roleName in createdRoles)
            {
                var isExistRole = await _roleManager.FindByNameAsync(roleName);

                if (isExistRole != null)
                    continue;

                var roleIdentityResult = await _roleManager.CreateAsync(new IdentityRole
                {
                    Name = roleName,
                });

                if (!roleIdentityResult.Succeeded)
                {
                    //logging
                }
            }

            var user = new AppUser
            {
                Firstname = "Admin",
                UserName = "admin",
                Email = "admin@code.edu.az"
            };

            var isExistUser = await _userManager.FindByNameAsync(user.UserName);

            if (isExistUser != null)
            {
                return;
            }

            var result = await _userManager.CreateAsync(user, "123456");

            if (!result.Succeeded)
            {
                //logging

                return;
            }

            result = await _userManager.AddToRoleAsync(user, Constants.AdminRole);

            if (!result.Succeeded)
            {
                //logging
            }
        }
    }
}
