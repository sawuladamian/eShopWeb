using eShop.DataAccess.Repository.IRepository;
using eShop.Models;
using eShop.Models.ViewModels;
using eShopWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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
                Tshirt = _unitOfWork.Tshirt.GetAll(x => x.Name.Contains(FilterName), includeProperties: "Color,Size")
            };
            return View(vm);
        }

        //public HomeVM GetByName([FromQuery]string FilterName="")
        //{
        //    HomeVM vm = new()
        //    {
        //        FilterName=FilterName,
        //        Tshirt= _unitOfWork.Tshirt.GetAll(x=>x.Name.Contains(FilterName),includeProperties:"Color,Size")
        //    };
        //    return vm;
        //}
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