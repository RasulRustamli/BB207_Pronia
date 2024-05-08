
using BB207_Pronia.DAL;
using BB207_Pronia.Models;
using BB207_Pronia.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BB207_Pronia.Controllers
{
    public class HomeController : Controller
    {
        AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Product> products=_context.Products.Include(x=>x.ProductPhotos).Where(x=>x.ProductPhotos.Count>0).ToList();

            HomeVm homeVm = new HomeVm()
            {
                Products = products,
                Sliders = _context.Sliders.ToList()
            };
            
            return View(homeVm);
        }
        public IActionResult Detail(int? id)
        {
            return View();
        }

       
    }
}