using AppLookUp.Data.Repository.IRepository;
using AppLookUp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AppLookUp.Web.Areas.Client.Controllers
{
    [Area("Client")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork=unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var listInfo = await _unitOfWork.Information.GetAll(s => !s.IsDeleted);
            return View(listInfo);
        }

        #region CALL APIS

        public async Task<IActionResult> GetList(string? keyword)
        {
            var data = await _unitOfWork.Information.GetTop10(keyword);

            return Json(new { data });
        }

        #endregion
    }
}