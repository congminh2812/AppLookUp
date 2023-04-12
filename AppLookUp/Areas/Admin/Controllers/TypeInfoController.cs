using AppLookUp.Data.Repository.IRepository;
using AppLookUp.Models;
using AppLookUp.Models.RequestModels;
using AppLookUp.Utility.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AppLookUp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = RoleConstant.Role_Admin)]
    public class TypeInfoController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public TypeInfoController(IUnitOfWork unitOfWork)
        {
            _unitOfWork=unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            var data = await _unitOfWork.TypeInfo.GetUpsert(id);

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(TypeInfo obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Id is 0)
                {
                    _unitOfWork.TypeInfo.Add(obj);
                    TempData["success"] = "Tạo loại thông tin thành công";
                }
                else
                {
                    _unitOfWork.TypeInfo.Update(obj);
                    TempData["success"] = "Cập nhật loại thông tin thành công";
                }

                await _unitOfWork.SaveAsync();

                return View("Index");
            }

            return View(obj);
        }

        #region APIS CALL

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _unitOfWork.TypeInfo.GetAll();

            return Json(new { data });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var TypeInfo = await _unitOfWork.TypeInfo.GetFirstOrDefault(x => x.Id == id);

            if (TypeInfo is null)
                return Json(new { success = false, message = "Không tìm thấy người dùng" });

            await _unitOfWork.Delete(TypeInfo);
            await _unitOfWork.SaveAsync();

            return Json(new { success = true, message = "Xóa thành công" });
        }

        #endregion
    }
}
