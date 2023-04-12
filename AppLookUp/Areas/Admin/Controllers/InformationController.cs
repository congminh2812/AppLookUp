using AppLookUp.Data.Repository.IRepository;
using AppLookUp.Models;
using AppLookUp.Models.RequestModels;
using AppLookUp.Models.ViewModels;
using AppLookUp.Utility.Constants;
using AppLookUp.Utility.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AppLookUp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = RoleConstant.Role_Admin)]
    public class InformationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public InformationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork=unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            var data = await _unitOfWork.Information.GetUpsert(id);

            var typeInfos = await _unitOfWork.TypeInfo.GetAll();

            var informationVM = new InformationVM
            {
                Information = data ?? new Information(),
                ListTypeInfo = typeInfos.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name })
            };

            return View(informationVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(InformationVM viewModel)
        {
            if (ModelState.IsValid)
            {
                var obj = viewModel.Information;
                if (obj.Id is 0)
                {
                    obj.CreatedBy = User.GetUserId();
                    _unitOfWork.Information.Add(obj);
                    TempData["success"] = "Tạo loại thông tin thành công";
                }
                else
                {
                    obj.UpdatedBy = User.GetUserId();
                    _unitOfWork.Information.Update(obj);
                    TempData["success"] = "Cập nhật loại thông tin thành công";
                }

                await _unitOfWork.SaveAsync();

                return View("Index");
            }

            return View(viewModel);
        }

        #region APIS CALL

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _unitOfWork.Information.GetAll(includeProperties: "TypeInfo");
            data = data.Where(s => !s.IsDeleted);

            return Json(new { data });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var information = await _unitOfWork.Information.GetFirstOrDefault(x => x.Id == id);

            if (information is null)
                return Json(new { success = false, message = "Không tìm thấy người dùng" });

            await _unitOfWork.Delete(information);
            await _unitOfWork.SaveAsync();

            return Json(new { success = true, message = "Xóa thành công" });
        }

        #endregion
    }
}
