using AppLookUp.Data.Data;
using AppLookUp.Data.DbInitializer;
using AppLookUp.Models;
using AppLookUp.Utility.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AppMVC.DataAccess.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;
        public DbInitializer(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
        }

        public void Initialize()
        {
            // migrations if they are not applied
            try
            {
                if (_db.Database.GetPendingMigrations().Any())
                    _db.Database.Migrate();
            }
            catch (Exception)
            {
            }

            // create roles if they are not created
            if (!_roleManager.RoleExistsAsync(RoleConstant.Role_Admin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(RoleConstant.Role_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(RoleConstant.Role_User_Indi)).GetAwaiter().GetResult();

                // if roles are not created, then we will create admin user as well
                _userManager.CreateAsync(new AppUser
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    Name = "Admin"
                }, "Admin@123").GetAwaiter().GetResult();

                var user = _db.AppUsers.First(s => s.Email == "admin@gmail.com");

                _userManager.AddToRoleAsync(user, RoleConstant.Role_Admin).GetAwaiter().GetResult();
            }

            return;
        }
    }
}
