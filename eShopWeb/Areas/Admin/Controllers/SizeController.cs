using eShop.DataAccess.Repository.IRepository;
using eShop.Models;
using Microsoft.AspNetCore.Mvc;


namespace eShopWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SizeController : Controller
    {
        public IUnitOfWork _unitOfWork;
        public SizeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Size> materials = _unitOfWork.Size.GetAll();
            return View(materials);
        }
        public IActionResult Upsert(int? id)
        {
            Size material = new();
            if(id == 0 || id==null)
            {
                return View(material);
            }else
            {
                material = _unitOfWork.Size.GetFirstOrDefault(x => x.Id == id);
                return View(material);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Size obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Id == 0)
                {
                    _unitOfWork.Size.Add(obj);
                    TempData["success"] = "Size added successfully!";
                }
                else
                {
                    _unitOfWork.Size.Update(obj);
                    TempData["success"] = "Size edited successfully!";
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
            var materialList = _unitOfWork.Size.GetAll();
            return Json(new { data = materialList });
        }
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = _unitOfWork.Size.GetFirstOrDefault(x => x.Id == id);
            if (obj != null)
            {
                _unitOfWork.Size.Remove(obj);
                _unitOfWork.Save();
                return Json(new { success = true, message = "Size deleted!" });
            }
            else
            {
                return Json(new { success = false, message = "Size not found!" });
            }
        }
        #endregion
    }
}
