using eShop.DataAccess.Repository.IRepository;
using eShop.Models;
using eShop.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections;

namespace eShopWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TshirtController : Controller
    {
        public IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        public TshirtController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            IEnumerable<Tshirt> tshirts = _unitOfWork.Tshirt.GetAll();
            return View(tshirts);
        }
      
        public IActionResult Upsert(int? id)
        {
            TshirtVM tshirtVM = new()
            {
                Tshirt = new(),
                ColorList = _unitOfWork.Color.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Type,
                    Value = i.Id.ToString()
                }),
                SizeList = _unitOfWork.Size.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Type,
                    Value = i.Id.ToString()
                })
            };
            if(id == 0 || id==null)
            {
                return View(tshirtVM);
            }else
            {
                tshirtVM.Tshirt = _unitOfWork.Tshirt.GetFirstOrDefault(x => x.Id == id);
                return View(tshirtVM);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(TshirtVM obj, IFormFile? file)
        {
            
            string wwwRootPath = _hostEnvironment.WebRootPath;
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(wwwRootPath, @"images\tshirts");
                var extension = Path.GetExtension(file.FileName);
                if (obj.Tshirt.ImageUrl != null)
                {
                    var oldImagePath = Path.Combine(wwwRootPath, obj.Tshirt.ImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    file.CopyTo(fileStreams);
                }
                obj.Tshirt.ImageUrl = @"\images\tshirts\" + fileName + extension;
            }

            if (obj.Tshirt.Id == 0)
            {                               
               _unitOfWork.Tshirt.Add(obj.Tshirt);
                TempData["success"] = "Tshirt added successfully!";
            }
            else
            {
                _unitOfWork.Tshirt.Update(obj.Tshirt);
                TempData["success"] = "Tshirt edited successfully!";
            }
            _unitOfWork.Save();
            
            return RedirectToAction("Index");
        }
            

    
    #region API CALLS
    [HttpGet]
        public IActionResult GetAll()
        {
            var tshirtList = _unitOfWork.Tshirt.GetAll(includeProperties: "Color,Size");
            return Json(new { data = tshirtList });
        }
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = _unitOfWork.Tshirt.GetFirstOrDefault(x => x.Id == id);
            if (obj != null)
            {
                _unitOfWork.Tshirt.Remove(obj);
                _unitOfWork.Save();
                return Json(new { success = true, message = "Tshirt deleted!" });
            }
            else
            {
                return Json(new { success = false, message = "Tshirt not found!" });
            }
        }
        #endregion
    }
}
