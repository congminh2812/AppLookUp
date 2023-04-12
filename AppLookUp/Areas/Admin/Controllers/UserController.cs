using AppLookUp.Data.Repository.IRepository;
using AppLookUp.Models.RequestModels;
using AppLookUp.Utility.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AppLookUp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = RoleConstant.Role_Admin)]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;

        public UserController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager
            )
        {
            _unitOfWork=unitOfWork;
            _userManager=userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(string? id)
        {
            var data = await _unitOfWork.AppUser.GetUpsert(id);

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(UserReqModel userReqModel)
        {
            if (ModelState.IsValid)
            {
                var user = _unitOfWork.AppUser.CreateUser();

                user.UserName = userReqModel.Email;
                user.Email = userReqModel.Email;
                user.Name = userReqModel.Name;

                var result = await _userManager.CreateAsync(user, userReqModel.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, userReqModel.Role ?? RoleConstant.Role_User_Indi);

                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(userReqModel);
        }

        #region APIS CALL

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _unitOfWork.AppUser.GetList();

            return Json(new { data });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _unitOfWork.AppUser.GetFirstOrDefault(x => x.Id == id);

            if (user is null)
                return Json(new { success = false, message = "Không tìm thấy người dùng" });

            _unitOfWork.AppUser.Remove(user);
            await _unitOfWork.SaveAsync();

            return Json(new { success = true, message = "Xóa thành công" });
        }

        #endregion
    }
}
