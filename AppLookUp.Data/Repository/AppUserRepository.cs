using AppLookUp.Data.Data;
using AppLookUp.Models;
using AppLookUp.Models.RequestModels;
using AppLookUp.Models.ViewModels;
using AppMVC.DataAccess.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AppLookUp.Data.Repository.IRepository
{
    public class AppUserRepository : Repository<AppUser>, IAppUserRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AppUserRepository(
            ApplicationDbContext db,
              UserManager<IdentityUser> userManager,
              RoleManager<IdentityRole> roleManager) : base(db)
        {
            _db = db;
            _userManager = userManager;
            _roleManager=roleManager;
        }

        public async Task<IEnumerable<UserVM>> GetList()
        {
            var listUser = await _db.AppUsers.ToListAsync();

            var listUserVM = listUser.Select(s =>
            {
                var roles = _userManager.GetRolesAsync(s).GetAwaiter().GetResult();
                return new UserVM
                {
                    Id = s.Id,
                    Name = s.Name,
                    Email = s.Email,
                    Role = string.Join(',', roles).Trim(','),
                };
            });

            return listUserVM;
        }

        public async Task<UserReqModel> GetUpsert(string? id)
        {
            var userReqModel = new UserReqModel
            {
                RoleList = _roleManager.Roles.Select(x => new SelectListItem { Text = x.Name, Value = x.Name })
            };

            if (id is not null)
            {
                var user = await GetFirstOrDefault(x => x.Id == id);
                if (user is not null)
                {
                    userReqModel.Id = id;
                    userReqModel.Name = user.Name;
                    userReqModel.Email = user.Email;
                    userReqModel.Role = _userManager.GetRolesAsync(user).GetAwaiter().GetResult().First();
                }
            }

            return userReqModel;
        }

        public AppUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<AppUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                    $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }
    }
}
