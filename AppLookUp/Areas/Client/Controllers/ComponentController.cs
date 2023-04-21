using AppLookUp.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace AppLookUp.Web.Areas.Client.Controllers
{
    [Area("Client")]
    public class ComponentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ComponentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork=unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        #region CALL APIS

        public async Task<IActionResult> GetList(string? keyword)
        {
            var data = await _unitOfWork.Component.GetTop10(keyword);

            return Json(new { data });
        }

        #endregion
    }
}