using eShop.DataAccess.Repository.IRepository;
using eShop.Models;
using Microsoft.AspNetCore.Mvc;


namespace eShopWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ColorController : Controller
    {
        public IUnitOfWork _unitOfWork;
        public ColorController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Color> colors = _unitOfWork.Color.GetAll();
            return View(colors);
        }
        public IActionResult Upsert(int? id)
        {
            Color color = new();
            if(id == 0 || id==null)
            {
                return View(color);
            }else
            {
                color = _unitOfWork.Color.GetFirstOrDefault(x => x.Id == id);
                return View(color);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Color obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Id == 0)
                {
                    _unitOfWork.Color.Add(obj);
                    TempData["success"] = "Color added successfully!";
                }
                else
                {
                    _unitOfWork.Color.Update(obj);
                    TempData["success"] = "Color edited successfully!";
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View();
        }
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var colorList = _unitOfWork.Color.GetAll();
            return Json(new { data = colorList });
        }
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = _unitOfWork.Color.GetFirstOrDefault(x => x.Id == id);
            if (obj != null)
            {
                _unitOfWork.Color.Remove(obj);
                _unitOfWork.Save();
                return Json(new { success = true, message = "Color deleted!" });
            }
            else
            {
                return Json(new { success = false, message = "Color not found!" });
            }
        }
        #endregion
    }
}
