using BB207_Pronia.DAL;
using BB207_Pronia.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BB207_Pronia.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Category> categories = _context.Categories.Include(x => x.Products).ToList();
            return View(categories);
        }
       
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            

            _context.Categories.Add(category);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {
            var category=_context.Categories.FirstOrDefault(x => x.Id == id);
            if(category== null)
            {
                return RedirectToAction("Index");
            }

            return View(category);


        }

        [HttpPost]
        public IActionResult Update(Category newCategory)
        {
            var oldCategory = _context.Categories.FirstOrDefault(x => x.Id == newCategory.Id);
            if (oldCategory == null)
            {
                return RedirectToAction("Index");
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            oldCategory.Name=newCategory.Name;
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }








        public IActionResult Delete(int? id)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Id == id);
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
