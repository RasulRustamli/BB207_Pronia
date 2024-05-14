using BB207_Pronia.DAL;
using BB207_Pronia.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace BB207_Pronia.Areas.Admin.Controllers
{
    [Area("Admin")]
  
    public class SliderController:Controller
    {
        AppDbContext _context;
        IWebHostEnvironment _environment;

        public SliderController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        [Authorize(Roles = "Admin,Moderator")]
        public IActionResult Index()
        {

            List<Slider> sliders = _context.Sliders.ToList();
            return View(sliders);

        }
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Slider slider)
        {

            if(!slider.PhotoFile.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("PhotoFile", "Formati duzgun daxil edin");
                return View();
            }


            string filename = slider.PhotoFile.FileName;
            string path = _environment.WebRootPath + @"\Upload\Slider\" + filename;
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                slider.PhotoFile.CopyTo(stream);
                
            }


            slider.ImgUrl = filename;
            
            
            if(!ModelState.IsValid)
            {
                return View();
            }
           await _context.Sliders.AddAsync(slider);
           await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int id)
        {
            return View(_context.Sliders.Find(id));
        }
        [HttpPost]
        public async Task<IActionResult> Update(Slider newSlider)
        {
            var oldSlider = _context.Sliders.FirstOrDefault(x => x.Id == newSlider.Id);
            if(!ModelState.IsValid)
            {
                return View();
            }

            if(newSlider.PhotoFile!=null)
            {
                string filename = newSlider.PhotoFile.FileName;
                string path = _environment.WebRootPath + @"\Upload\Slider\" + filename;
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    newSlider.PhotoFile.CopyTo(stream);

                }
                FileInfo fileInfo = new FileInfo(path + oldSlider.ImgUrl);

                if(fileInfo.Exists)
                {
                    fileInfo.Delete();
                }

                oldSlider.ImgUrl = filename;
                
            }

            oldSlider.SubTitle=newSlider.SubTitle;
            oldSlider.Title=newSlider.Title;
            oldSlider.Description=newSlider.Description;
          await  _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var slider=_context.Sliders.FirstOrDefault(s => s.Id == id);
            string path = _environment.WebRootPath + @"\Upload\Slider\" + slider.ImgUrl;

            FileInfo fileInfo = new FileInfo(path);
            if(fileInfo.Exists)
            {
                fileInfo.Delete();
            }
            _context.Sliders.Remove(slider);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}
