using AppLookUp.Data.Repository.IRepository;
using AppLookUp.Models;
using AppLookUp.Utility.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppLookUp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = RoleConstant.Role_Admin)]
    public class ComponentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ComponentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork=unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            var data = await _unitOfWork.Component.GetUpsert(id);

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Component obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Id is 0)
                {
                    _unitOfWork.Component.Add(obj);
                    TempData["success"] = "Tạo component thành công";
                }
                else
                {
                    _unitOfWork.Component.Update(obj);
                    TempData["success"] = "Cập nhật component thành công";
                }

                await _unitOfWork.SaveAsync();

                return RedirectToAction("Index");
            }

            return View(obj);
        }

        #region APIS CALL

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _unitOfWork.Component.GetAll();

            return Json(new { data });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var Component = await _unitOfWork.Component.GetFirstOrDefault(x => x.Id == id);

            if (Component is null)
                return Json(new { success = false, message = "Không tìm thấy component" });

            await _unitOfWork.Delete(Component);
            await _unitOfWork.SaveAsync();

            return Json(new { success = true, message = "Xóa thành công" });
        }

        #endregion
    }
}
