using eShop.DataAccess.Repository.IRepository;
using eShop.Models;
using eShop.Models.ViewModels;
using eShop.Utility;
using eShopWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace eShopWeb.Controllers
{
    [Area("Customer")]
        public class HomeController : Controller
        {
            private readonly ILogger<HomeController> _logger;
            private readonly IUnitOfWork _unitOfWork;
            
            public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
            {
                _logger = logger;
                _unitOfWork = unitOfWork;
            }

            public IActionResult Index([FromQuery] string FilterName = "")
            {

                HomeVM vm = new()
                {
                    FilterName = FilterName,
                    Tshirt = _unitOfWork.Tshirt.GetAll(includeProperties: "Color,Size"),
                 };
                
                vm.Tshirt = vm.Tshirt.GroupBy(x => x.ProductCode).First();
                if (FilterName != null)
                {
                    vm.Tshirt = _unitOfWork.Tshirt.GetAll(x => x.Name.Contains(FilterName), includeProperties: "Color,Size").GroupBy(x => x.ProductCode).Select(g => g.OrderBy(x => x.ProductCode).FirstOrDefault());
                }
                if (vm.Tshirt.Count()==0)
                {
                    TempData["error"] = "Sorry, no result found";
                }           
                return View(vm);            
            }
            public IActionResult Details(int tshirtId)
            {
                var prodCode = _unitOfWork.Tshirt.GetFirstOrDefault(x => x.Id == tshirtId, includeProperties: "Color,Size").ProductCode;
                var prodSize = _unitOfWork.Tshirt.GetFirstOrDefault(x => x.Id == tshirtId, includeProperties: "Color,Size").Size.Type;
                ShoppingCart cartObj = new()
                {
                    Tshirt = _unitOfWork.Tshirt.GetFirstOrDefault(x => x.Id == tshirtId, includeProperties: "Color,Size"),
                    Count = 1,
                    TshirtId = tshirtId,
                    ColorList = _unitOfWork.Tshirt.GetAll(x => x.ProductCode == prodCode && x.Size.Type == prodSize, includeProperties: "Color").ToList()
                };
                return View(cartObj);
            }
            [HttpPost]
            [ValidateAntiForgeryToken]
            [Authorize]
            public IActionResult Details(ShoppingCart shoppingCart)
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                shoppingCart.ApplicationUserId = claim.Value;

                ShoppingCart cartFromDb = _unitOfWork.ShoppingCart.GetFirstOrDefault(
                    u => u.ApplicationUserId == claim.Value && u.TshirtId == shoppingCart.TshirtId
                    );
                if (cartFromDb == null)
                {
                    _unitOfWork.ShoppingCart.Add(shoppingCart);
                    _unitOfWork.Save();
                    HttpContext.Session.SetInt32(SD.SessionCart,
                        _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value).ToList().Count);
                }
                else
                {
                    _unitOfWork.ShoppingCart.IncrementCount(cartFromDb, shoppingCart.Count);
                    HttpContext.Session.SetInt32(SD.SessionCart,
                        _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value).ToList().Count);
                    _unitOfWork.Save();
                }
            
                return RedirectToAction("Index");

            }
            public IActionResult Privacy()
            {
                return View();
            }

            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
            public IActionResult Error()
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
    }
}