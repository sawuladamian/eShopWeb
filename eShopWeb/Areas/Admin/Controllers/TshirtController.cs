using eShop.DataAccess.Repository.IRepository;
using eShop.Models;
using eShop.Models.ViewModels;
using eShop.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections;

namespace eShopWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
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
        public IActionResult Upsert(TshirtVM obj, IEnumerable<IFormFile>? file)
        {

            foreach (var item in file)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (item != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\tshirts");
                    var extension = Path.GetExtension(item.FileName);
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
                    item.CopyTo(fileStreams);
                    }
                    obj.TshirtImages = new TshirtImagesUrl(){};
                    obj.TshirtImages.ImageURL = @"\images\tshirts\" + fileName + extension;
                    //obj.TshirtImages.Color = Path.GetFileNameWithoutExtension(item.FileName);
                    obj.Tshirt.ImageUrl = @"\images\tshirts\" + fileName + extension;
                }
            
            if (obj.Tshirt.Id == 0)
            {               
                    _unitOfWork.Tshirt.Add(obj.Tshirt);
                    _unitOfWork.Save();
                    obj.TshirtImages.TshirtID = obj.Tshirt.Id;
                    _unitOfWork.TshirtImageUrl.Add(obj.TshirtImages);
                    _unitOfWork.Save();
                        obj.Tshirt.Id = 0;
                        obj.Tshirt.ImageUrl = null;
                
                TempData["success"] = "Tshirt added successfully!";
            }
            else
            {
                    
                _unitOfWork.Tshirt.Update(obj.Tshirt);
                _unitOfWork.Save();
                TempData["success"] = "Tshirt edited successfully!";
            }
        }

            return RedirectToAction("Index");
     }
            

    
    #region API CALLS
    [HttpGet]
        public IActionResult GetAll()
        {
            var tshirtList = _unitOfWork.Tshirt.GetAll(includeProperties: "Size");
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
